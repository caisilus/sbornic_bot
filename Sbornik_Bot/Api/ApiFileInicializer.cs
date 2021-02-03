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
                //assert: file contains 2 lines, no check this
                string line = reader.ReadLine();
                token = line;
                line = reader.ReadLine();
                groupId = ulong.Parse(line);
            }
        }
        public IMessageApi GetApi()
        {
            return new VkMessageApi(token, groupId);
        }
    }
}