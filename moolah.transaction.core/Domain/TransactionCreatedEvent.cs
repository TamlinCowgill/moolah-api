namespace Moolah.Transaction.Core.Domain
{
    public class TransactionCreatedEvent : Transaction
    {
        public EventMetaData Meta => new EventMetaData { EventType = "transaction.created" };
    }
}
