using System.Collections.Generic;
using System.Linq;

namespace Sbornik_Bot
{
    public class DictTagsService: ITagsService
    {
        private HashSet<string> _tags;
        
        private Dictionary<int, List<string>> _idsToTags;
        private Dictionary<int, WallPostData> _idsToWallPostData;
        
        private int _index;

        public DictTagsService()
        {
            _tags = new HashSet<string>();
            _idsToTags = new Dictionary<int, List<string>>();
            _idsToWallPostData = new Dictionary<int, WallPostData>();
        }
        public string[] GetTagsList()
        {
            return _tags?.ToArray();
        }

        public bool AddTags(string[] tags)
        {
            foreach (var tag in tags)
            {
                _tags.Add(tag);
            }
            return true;
        }

        public bool AddTag(string tag)
        {
            return _tags.Add(tag);
        }

        public bool AddPost(WallPostData wallPostData, string[] tags)
        {
            _idsToWallPostData[_index] = wallPostData;
            _idsToTags[_index] = new List<string>();
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    if (_tags.Contains(tag))
                    {
                        _idsToTags[_index].Add(tag);
                    }
                }   
            }
            _index++;
            return true;
        }
        
        public bool AddPost(WallPostData wallPostData, string[] tags, out int id)
        {
            _idsToWallPostData[_index] = wallPostData;
            _idsToTags[_index] = new List<string>();
            var photoes = wallPostData.Attachments.Where(att => PhotoModel.IsPhoto(att))
                .Select(att => new PhotoModel(att));
            foreach (var photo in photoes)
            {
                photo.PrintUri();
            }
            id = _index; //id of added post
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    if (_tags.Contains(tag))
                    {
                        _idsToTags[_index].Add(tag);
                    }
                }   
            }
            _index++;
            return true;
        }
    }
}