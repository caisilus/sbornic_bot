using System;
using System.Net;
using System.Linq;
using VkNet.Model.Attachments;

namespace Sbornik_Bot
{
    public class PhotoModel: IWallAttachmentModel
    {
        //private string filename;
        private string uri;
        public string Filename { get; }
        public AttachmentModelType AttachmentType { get; }

        public PhotoModel(Attachment attachment)
        {
            if (!IsPhoto(attachment))
            {
                throw new WrongAttachmentTypeException(typeof(Photo),attachment.Type);
            }
            
            AttachmentType = AttachmentModelType.Photo;
            var photo = (Photo)attachment.Instance;
            uri = photo.Sizes.OrderBy(size => size.Height).Last().Url.AbsoluteUri;
            
            //it's awful
            var ext = uri.Split('/').Last().Split('.').Skip(1).First().Split('?').First();
            Console.WriteLine(ext);
            var filename = $@"..\..\..\files\attachments\{System.Guid.NewGuid()}.{ext}";
            Console.WriteLine(filename);
            using (var client = new WebClient())
            {
                client.DownloadFile(uri,filename);
            }

            Filename = filename;
        }

        public static bool IsPhoto(Attachment attachment)
        {
            return attachment.Instance.GetType() == typeof(Photo);
        }
        
        public void PrintUri()
        {
            Console.WriteLine(uri);
        }
    }
}