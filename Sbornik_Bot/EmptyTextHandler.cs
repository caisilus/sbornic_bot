using System;
using System.Linq;
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
                var wallPosts = attachments.Where(attachment => attachment.Instance.GetType() == typeof(Wall))
                    .Select(attachment => (Wall) attachment.Instance);
                bool postsAdded = false;
                foreach (var wallPost in wallPosts)
                {
                    WallPostData wallPostData = new WallPostData(wallPost.Text, wallPost.Date, wallPost.Attachments);
                    bool status = _tagsService.AddPost(wallPostData, null);
                    if (status)
                    {
                        postsAdded = true;
                        return VkMessageApi.DefaultTextMessage(message, "пост добавлен!");
                    }
                    else
                        return VkMessageApi.DefaultTextMessage(message, "Ошибка: пост не был добавлен");
                }

                if (!postsAdded)
                {
                    return VkMessageApi.DefaultTextMessage(message, "Введите /help для помощи");
                }
            }
            else
            {
                return VkMessageApi.DefaultTextMessage(message, "Введите /help для помощи");
            }

            throw new ApplicationException();
        }
    }
}