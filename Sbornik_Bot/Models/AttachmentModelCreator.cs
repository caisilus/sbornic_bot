using System;
using VkNet.Model.Attachments;

namespace Sbornik_Bot
{
    public class WrongAttachmentTypeException : Exception
    {
        private Type goalType;
        private Type actualType;
        public WrongAttachmentTypeException() { }

        public WrongAttachmentTypeException(string message)
            : base(message) { }

        public WrongAttachmentTypeException(string message, Exception inner)
            : base(message, inner) { }

        public WrongAttachmentTypeException(Type goalType, Type actualType)
            : base($"Expected type: {goalType}, actual: {actualType}") { }

    }

    public enum AttachmentModelType
    {
        Photo,
        Document
    }
    
    public static class AttachmentModelCreator
    {
        public static IWallAttachmentModel Create(Attachment attachment)
        {
            if (PhotoModel.IsPhoto(attachment))
                return new PhotoModel(attachment);
            else{
                if (DocumentModel.IsDocument(attachment))
                    return new DocumentModel(attachment);
                else
                {
                    throw new WrongAttachmentTypeException("no type matched");
                }
            }
        }
        public static bool IsPhoto(Attachment attachment)
        {
            return PhotoModel.IsPhoto(attachment);
        }

        public static bool IsDocument(Attachment attachment)
        {
            return DocumentModel.IsDocument(attachment);
        }
    }
}