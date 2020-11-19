namespace moolah.api.transaction.Domain
{
    public class TransactionCreatedEvent : Transaction
    {
        public EventMetaData Meta => new EventMetaData { EventType = "transaction.created" };
    }
}
