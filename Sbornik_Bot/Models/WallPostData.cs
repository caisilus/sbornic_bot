using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using VkNet.Model.Attachments;

namespace Sbornik_Bot
{
    public class WallPostData
    {
        private string _text;
        private DateTime? _date;
        private ReadOnlyCollection<Attachment> _attachments;

        public WallPostData(string text, DateTime? date, ReadOnlyCollection<Attachment> attachments)
        {
            _text = text;
            _date = date;
            _attachments = attachments;
        }

        public ReadOnlyCollection<Attachment> Attachments
        {
            get
            {
                return _attachments;
            }
        }
        
        public override bool Equals(object obj)
        {
            WallPostData wallPostObj = obj as WallPostData;
            if (wallPostObj is null)
                return false;
            return wallPostObj._attachments.Zip(this._attachments).All(pair => pair.First == pair.Second);
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode() ^ _date.GetHashCode() ^ _attachments.GetHashCode();
        }

        public static bool operator==(WallPostData wpd1, WallPostData wpd2)
        {
            return wpd1?.Equals(wpd2) ?? false;
        }
        
        public static bool operator!=(WallPostData wpd1, WallPostData wpd2)
        {
            return !(wpd1 == wpd2);
        }
    }
}