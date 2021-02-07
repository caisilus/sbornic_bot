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
        private ITagsService _tagsService;

        public CommandHandler(ITagsService tagsService)
        {
            _tagsService = tagsService;
            help["/help"] = "Отображает список команд";
            help["/add_tags"] = "Добавляет слова(разделённые пробелами) из сообщения с этой командой как тэги";
            help["/tags_list"] = "Отображает список существующих тегов";
            help["/set_tags"] = "Устанавливает теги для поста";
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

                    case "/add_tags":
                    {
                        string[] tags = words.Skip(1).ToArray();
                        if (_tagsService.AddTags(tags)) //using tag service to add tags
                        {
                            StringBuilder sb = new StringBuilder("Вы добавили тэги: ");
                            foreach (string tag in tags) //building a reply string of tags to be added
                            {
                                sb.Append($"\"{tag}\" ");
                            }
                            replyText = sb.ToString();   
                        }
                        else
                        {
                            replyText = "Error!";
                        }
                        break;
                    }
                    case "/tags_list":
                    {
                        var tags = _tagsService.GetTagsList();
                        if (tags == null || tags.Length == 0)
                            replyText = "<empty list>";
                        else
                        {
                            replyText = String.Concat(tags);
                        } 
                        break;
                    }
                }

                return IMessageApi.DefaultTextMessage(message, replyText);
            }
            else 
                return null;
        }
    }
}