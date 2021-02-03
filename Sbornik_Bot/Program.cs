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
            IMessageApi botApi = inicializer.GetApi();
            
            IAuthorizer authorizer = new DefaultAuthorizer();
            ITagsService tagsService = new DictTagsService();
            
            while (true)
            {
                IEnumerable<Message> new_messages = botApi.NewMessages();
                IMessageConditionHandler comandConditionHandler = new CommandHandler(tagsService);
                IMessageConditionHandler emptyTextHandler = new EmptyTextHandler(tagsService);
                foreach (Message message in new_messages)
                {
                    if (comandConditionHandler.Condition(message))
                    {
                        botApi.SendMessage(comandConditionHandler.Reply(message));
                    }

                    if (emptyTextHandler.Condition(message))
                    {
                        botApi.SendMessage(emptyTextHandler.Reply(message));
                    }
                }
            }
        }
    }
}