using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public interface IMessageHandler
    {
        MessagesSendParams HandleMessage(Message message);
    }
}