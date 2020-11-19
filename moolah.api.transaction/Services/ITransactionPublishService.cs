using moolah.api.transaction.Domain;

namespace moolah.api.transaction.Services
{
    public interface ITransactionPublishService
    {
        void PublishTransactionCreatedEvent(Transaction transaction);
    }
}