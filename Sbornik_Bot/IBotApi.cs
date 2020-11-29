using System.Collections.Generic;
using VkNet;
using VkNet.Model;

namespace Sbornik_Bot
{
    public interface IBotApi
    {
        IEnumerable<Message> NewMessages();
        void SendMessage(Message message);
    }
}