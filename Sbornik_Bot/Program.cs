using System;
using System.Collections.Generic;
using VkNet.Model;

namespace Sbornik_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            IApiInicializer initializer = 
                new ApiFileInicializer(@"C:\C#_pet_projects\Sbornik_Bot\Sbornik_Bot\files\ini.txt");
            IMessageApi botApi = initializer.GetApi();
            
            //IAuthorizer authorizer = new DefaultAuthorizer(); Add authorization!
            ITagsService tagsService = new DictTagsService();
            
            while (true)
            {
                IEnumerable<Message> newMessages = botApi.NewMessages();
                IMessageConditionHandler commandConditionHandler = new CommandHandler(tagsService);
                IMessageConditionHandler emptyTextHandler = new EmptyTextHandler(tagsService);
                foreach (Message message in newMessages)
                {
                    if (commandConditionHandler.Condition(message))
                    {
                        botApi.SendMessage(commandConditionHandler.Reply(message));
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