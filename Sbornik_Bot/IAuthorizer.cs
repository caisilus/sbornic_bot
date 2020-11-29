namespace Sbornik_Bot
{
    public interface IAuthorizer
    {
        bool Authorize(long? userId);
    }
}