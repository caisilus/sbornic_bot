using System;
using System.Linq;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public class EmptyTextHandler: IMessageConditionHandler
    {
        private ITagsService _tagsService; //for sending posts and tags to Server

        public EmptyTextHandler(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }
        
        public bool Condition(Message message)
        {
            return message?.Text == "";
        }

        public MessagesSendParams Reply(Message message)
        {
            if (!Condition(message))
            {
                return null;
            }

            if (message?.Attachments != null)
            {
                var attachments = message.Attachments;
                var wallPosts = IMessageApi.GetAttachmentsPosts(attachments);
                bool postAdded = false;
                foreach (WallPostData wallPostData in wallPosts)
                {
                    postAdded = _tagsService.AddPost(wallPostData, null); /* using tags service. status = true if 
                        post if added, and false if an arror has occured. */
                    if (postAdded) //no errors, post added (to database)
                    {
                        return IMessageApi.DefaultTextMessage(message, "пост добавлен!");
                    }
                    else //error has occured
                        return IMessageApi.DefaultTextMessage(message, "Ошибка: пост не был добавлен");
                }

                if (!postAdded) //in case foreach had zero iterations
                {
                    return IMessageApi.DefaultTextMessage(message, "Введите /help для помощи");
                }
            }
            else
            {
                return IMessageApi.DefaultTextMessage(message, "Введите /help для помощи");
            }

            throw new ApplicationException(); //never happens, added for compiler's calm
        }
    }
}