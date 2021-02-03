namespace Sbornik_Bot
{
    public interface ITagsService
    {
        string[] GetTagsList();
        bool AddTags(string[] tags);
        bool AddPost(WallPostData wallPostData, string[] tags); //string[] tags - nullable. When no tags to add, null is used
    }
}