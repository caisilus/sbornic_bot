using VkNet.Model.Attachments;
using System.Net;

namespace Sbornik_Bot
{
    public class DocumentModel: IWallAttachmentModel
    {
        private string uri;
        public string Filename { get; }
        public AttachmentModelType AttachmentType { get; }
        public DocumentModel(Attachment attachment)
        {
            if (!IsDocument(attachment))
            {
                throw new WrongAttachmentTypeException(typeof(Document),attachment.Type);
            }
            else
            {
                AttachmentType = AttachmentModelType.Document;
                var document = (Document) attachment.Instance;
                var ext = document.Ext;
                var uri = document.Uri;
                var filename = $@"..\..\..\files\attachments\{System.Guid.NewGuid()}.{ext}";
                using (var client = new WebClient())
                {
                    client.DownloadFile(uri,filename);
                }

                Filename = filename;
            }
        }
        public static bool IsDocument(Attachment attachment)
        {
            return attachment.Instance.GetType() == typeof(Document);
        }
    }
}