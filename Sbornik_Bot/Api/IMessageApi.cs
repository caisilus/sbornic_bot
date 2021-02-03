using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public interface IMessageApi
    {
        IEnumerable<Message> NewMessages();
        void SendMessage(MessagesSendParams sendParams);
        
        //STATIC FUNCTIONS SIMPLIFYING WORK WITH API
        
        /// <summary>
        /// used as default MessageSendParams Constructor when only Text is valuable
        /// </summary>
        /// <param name="message"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MessagesSendParams DefaultTextMessage(Message message, string text) => new MessagesSendParams()
        {
            PeerId = message.PeerId,
            Message = text,
            RandomId = new Random().Next()
        };

        /// <summary>
        /// from attachment collection makes collection of WallPostData
        /// </summary>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public static IEnumerable<WallPostData> GetAttachmentsPosts(ReadOnlyCollection<Attachment> attachments)
        {
            var wallPosts = attachments.Where(attachment => attachment.Instance.GetType() == typeof(Wall))
                .Select(attachment => (Wall) attachment.Instance) //IEnumerable of Wall posts among Attachments
                .Select(wallPost => new WallPostData(wallPost.Text, wallPost.Date, wallPost.Attachments)); //IEnumerable<WallPostData>
            return wallPosts;
        }
    }
}