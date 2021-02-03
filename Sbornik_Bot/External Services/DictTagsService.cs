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

        public bool AddPost(WallPostData wallPostData, string[] tags)
        {
            _idsToWallPostData[_index] = wallPostData;
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