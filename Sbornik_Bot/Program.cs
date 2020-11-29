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
            while (true)
            {
                IEnumerable<Message> new_messages = botApi.NewMessages();
                
            }
        }
    }
}