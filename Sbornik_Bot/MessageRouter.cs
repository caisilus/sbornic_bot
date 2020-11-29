using System;
using VkNet.Model;

namespace Sbornik_Bot
{
    public class MessageRouter: IMessageRouter
    {
        private IAuthorizer _authorizer; //for authorization

        public event MessageHandler PlainMessage; //event for messages without commands
        public event MessageHandler CommandMessage; //event for messages with commands
        
        public event MessageSenderDelegate SendReplyMessage; //event for sending messages using api
        
        public MessageRouter(IAuthorizer authorizer)
        {
            _authorizer = authorizer;
        }

        public void RouteMessage(Message message)
        {
            var userId = message.UserId;
            if (_authorizer.Authorize(userId)) //User access check (admins only allowed)
            {
                string text = message.Text;
                if (String.IsNullOrEmpty(text)) //Empty string cannot be a command
                {
                    var reply = PlainMessage?.Invoke(message); //if event is not null calls an event
                    if (!(reply is null))
                    {
                        SendReplyMessage?.Invoke(reply); //if event is not null calls an event
                    }
                }
                else
                {
                    if (text[0] == '!') //ToDo: command symbol
                    {
                        var reply = CommandMessage?.Invoke(message); //if event is not null calls an event
                        if (!(reply is null))
                        {
                            SendReplyMessage?.Invoke(reply); //if event is not null calls an event
                        }
                    }
                }
            }
        }
    }
}