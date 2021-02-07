using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public interface IMessageConditionHandler
    {
        bool Condition(Message message);
        MessagesSendParams Reply(Message message);
    }
}