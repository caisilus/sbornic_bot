using VkNet.Model.Attachments;

namespace Sbornik_Bot
{
    public interface IWallAttachmentModel
    {
        public string Filename { get; }
        public AttachmentModelType AttachmentType { get; }

        //public static bool IsTypeOf(Attachment attachment);
    }
}