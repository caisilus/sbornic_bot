using System;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Sbornik_Bot
{
    public class PlainMessageHandler: IMessageHandler
    {
        private string _help;

        public PlainMessageHandler(string help = "help: !help")
        {
            _help = help;
        }
        public MessagesSendParams HandleMessage(Message message)
        {
            string text = message.Text;
            if (!String.IsNullOrEmpty(text))
            {
                return new MessagesSendParams
                {
                    PeerId = message.PeerId,
                    Message = _help,
                    RandomId = new DateTime().Millisecond
                };
            }
            else
            {
                var attachments = message.Attachments;
                foreach (var attachment in attachments)
                {
                    Console.WriteLine(attachment.Type);
                }
                return null;
            }
        }
    }
}