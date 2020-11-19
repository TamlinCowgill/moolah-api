using moolah.transaction.api.Domain;

namespace moolah.transaction.api.Services
{
    public interface ITransactionPublishService
    {
        void PublishTransactionCreatedEvent(Transaction transaction);
    }
}