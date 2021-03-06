using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moolah.Account.Core.Services;
using moolah.common.Services;

namespace Moolah.Account.Api
{
    public class Startup
    {
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
            services.AddAWSService<Amazon.DynamoDBv2.IAmazonDynamoDB>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IItemEventService, ItemEventService>();
            services.AddSwaggerGen();
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

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                c.RoutePrefix = "api/help/accounts";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
