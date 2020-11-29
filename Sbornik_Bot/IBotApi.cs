using System.Collections.Generic;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public interface IBotApi
    {
        IEnumerable<Message> NewMessages();
        void SendMessage(MessagesSendParams sendParams);
    }
}