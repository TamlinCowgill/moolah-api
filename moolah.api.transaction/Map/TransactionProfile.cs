using AutoMapper;
using moolah.api.transaction.Domain;

namespace moolah.api.transaction.Map
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionCreatedEvent>();
        }
    }
}
