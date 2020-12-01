using AutoMapper;
using Moolah.Transaction.Core.Domain;

namespace Moolah.Transaction.Core.Map
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Domain.Transaction, TransactionCreatedEvent>();
        }
    }
}
