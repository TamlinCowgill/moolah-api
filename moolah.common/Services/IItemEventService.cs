using moolah.common.Domain;

namespace moolah.common.Services
{
    public interface IItemEventService
    {
        ItemEvent RegisterEvent(string itemEventId, string body);
        bool CommitEvent(ItemEvent itemEvent);
        bool RollbackEvent(ItemEvent itemEvent);
    }
}