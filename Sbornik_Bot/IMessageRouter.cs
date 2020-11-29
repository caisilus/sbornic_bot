using System.Collections.Generic;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public delegate MessagesSendParams MessageHandler(Message message); //Takes message, does smth and returns reply message or null

    public delegate void MessageSenderDelegate(MessagesSendParams sendParams); //Sends message (for IBotApi.SendMessage())
    
    public interface IMessageRouter
    {
        void RouteMessage(Message message);
        event MessageSenderDelegate SendReplyMessage;
    }
}