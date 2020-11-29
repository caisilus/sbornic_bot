using System;
using System.Collections.Generic;
using VkNet.Model;

namespace Sbornik_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            IApiInicializer inicializer = 
                new ApiFileInicializer(@"C:\C#_pet_projects\Sbornik_Bot\Sbornik_Bot\files\ini.txt");
            IBotApi botApi = inicializer.GetApi();
            
            IAuthorizer authorizer = new DefaultAuthorizer();
            MessageRouter router = new MessageRouter(authorizer);
            router.SendReplyMessage += botApi.SendMessage;
            PlainMessageHandler plainMessageHandler = new PlainMessageHandler();
            router.PlainMessage += plainMessageHandler.HandleMessage;
            
            while (true)
            {
                IEnumerable<Message> new_messages = botApi.NewMessages();
                foreach (Message message in new_messages)
                {
                    router.RouteMessage(message);
                }
            }
        }
    }
}