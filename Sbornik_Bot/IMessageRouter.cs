using System.Collections.Generic;
using VkNet.Model;

namespace Sbornik_Bot
{
    public delegate Message MessageHandler(Message message); //Takes message, does smth and returns reply message or null

    public delegate void MessageSenderDelegate(Message message); //Sends message (for IBotApi.SendMessage())
    
    public interface IMessageRouter
    {
        void RouteMessage(Message message);
        event MessageSenderDelegate SendReplyMessage;
    }
}