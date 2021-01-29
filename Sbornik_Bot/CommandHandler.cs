using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public class CommandHandler: IMessageConditionHandler
    {
        private Dictionary<string, string> help = new Dictionary<string, string>();

        public CommandHandler()
        {
            help["/help"] = "Отображает список команд";
            help["/add_tags"] = "Добавляет слова(разделённые пробелами) из сообщения с этой командой как тэги";
            help["/tags_list"] = "Отображает список существующих тегов";
        }
        public bool Condition(Message message)
        {
            return message?.Text != null && message.Text.Length > 0 && message.Text[0] == '/';
        }

        public MessagesSendParams Reply(Message message)
        {
            if (Condition(message))
            {
                string[] words = message.Text.Split(' ');
                string replyText = "нет совпадающей команды";
                switch (words[0])
                {
                    case "/help":
                    {
                        StringBuilder sb = new StringBuilder("Список команд:\n");
                        foreach (var kv in help)
                        {
                            sb.Append(kv.Key + ": " + kv.Value + "\n");
                        }
                        replyText = sb.ToString();
                        break;
                    }

                    case "/add_tags": //placeholder. Some database shit should happen here
                    {
                        StringBuilder sb = new StringBuilder("Вы добавили тэги: ");

                        foreach (string word in words.Skip(1)) //building a reply string of tags to be added
                        {
                            sb.Append($"\"{word}\" ");
                        }

                        replyText = sb.ToString();
                        break;
                    }
                    case "/tags_list":
                        replyText = "<empty list>"; //Some database shit should happen here
                        break;

                }

                return new MessagesSendParams()
                {
                    PeerId = message.PeerId,
                    Message = replyText,
                    RandomId = new Random().Next()
                };
            }
            else 
                return null;
        }
    }
}