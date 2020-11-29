namespace Sbornik_Bot
{
    public class DefaultAuthorizer : IAuthorizer
    {
        public bool Authorize(long? userId)
        {
            return true;
        }
    }
}