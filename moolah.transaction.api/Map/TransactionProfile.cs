using AutoMapper;
using moolah.transaction.api.Domain;

namespace moolah.transaction.api.Map
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionCreatedEvent>();
        }
    }
}
