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
                            replyText = tags.Aggregate((s1, s2) => s1 +" "+ s2);
                        } 
                        break;
                    }
                    case "/set_tags":
                    {
                        var attachments = message.Attachments;
                        if (attachments == null)
                        {
                            replyText = "Ошибка: нет прикреплённого поста!";
                            break;
                        }

                        WallPostData wallPost;
                        try
                        {
                            wallPost = IMessageApi.GetAttachmentsPosts(attachments); //Getting post from message
                        }
                        catch (InvalidOperationException ioe)
                        {
                            replyText = "Ошибка: нет прикреплённого поста!";
                            break;
                        }
                        catch (ArgumentNullException ane)
                        {
                            replyText = "Ошбика: нет прикреплённого поста!";
                            break;
                        }
                        catch (ApplicationException ae)
                        {
                            replyText = "Ошибка: больше 2 постов в сообщении!!!";
                            break;
                        }

                        //Building a string
                        StringBuilder stringBuilder = new StringBuilder();
                        var tags = words.Skip(1).ToArray();
                        foreach (var tag in tags)
                        {
                            if (_tagsService.AddTag(tag))
                            {
                                stringBuilder.Append($"Новый тэг {tag} добавлен.\n");
                            }
                        }
                        if (tags.Length == 0)
                            tags = null;

                        var status = _tagsService.AddPost(wallPost, tags, out int id); //Adding post and setting tags!
                        if (status)
                        {
                            stringBuilder.Append($"Пост (id: {id}) добавлен.\nТэги установлены.");
                            replyText = stringBuilder.ToString();
                        }
                        else
                        {
                            replyText = "Произошла ошибка! Пост или тэги не были добавлены.";
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