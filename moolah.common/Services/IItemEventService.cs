using Moolah.Common.Domain;

namespace Moolah.Common.Services
{
    public interface IItemEventService
    {
        ItemEvent RegisterEvent(string itemEventId, string body);
        bool CommitEvent(ItemEvent itemEvent);
        bool RollbackEvent(ItemEvent itemEvent);
    }
}