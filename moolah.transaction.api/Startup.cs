using Amazon.DynamoDBv2.DataModel;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using moolah.transaction.api.Services;

namespace moolah.transaction.api
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            services.AddControllers().AddNewtonsoftJson();
            // Add S3 and DynamoDB to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>();
            services.AddAWSService<Amazon.SQS.IAmazonSQS>();
            services.AddAWSService<Amazon.SimpleNotificationService.IAmazonSimpleNotificationService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddAutoMapper(typeof(Startup));
            
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddSingleton<ITransactionService, TransactionService>();
            services.AddSingleton<ITransactionPublishService, TransactionPublishService>();
            services.AddSingleton<IMapper, Mapper>();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
