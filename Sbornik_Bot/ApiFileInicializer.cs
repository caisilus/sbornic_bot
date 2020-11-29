using System.IO;
using System.Text;

namespace Sbornik_Bot
{
    public class ApiFileInicializer: IApiInicializer
    {
        private string token;
        private ulong groupId;

        public ApiFileInicializer(string path)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string line = reader.ReadLine();
                token = line;
                line = reader.ReadLine();
                groupId = ulong.Parse(line);
            }
        }
        public IBotApi GetApi()
        {
            return new VkBotApi(token, groupId);
        }
    }
}