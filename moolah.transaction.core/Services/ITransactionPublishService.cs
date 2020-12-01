namespace Moolah.Transaction.Core.Services
{
    public interface ITransactionPublishService
    {
        void PublishTransactionCreatedEvent(Domain.Transaction transaction);
    }
}