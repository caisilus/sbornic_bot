namespace Sbornik_Bot
{
    public interface ITagsService
    {
        string[] GetTagsList();
        bool AddTags(string[] tags);
        bool AddTag(string tag);
        bool AddPost(WallPostData wallPostData, string[] tags); //string[] tags - nullable. When no tags to add, null is used
        bool AddPost(WallPostData wallPostData, string[] tags, out int id); //id of added post, used to identify added post
    }
}