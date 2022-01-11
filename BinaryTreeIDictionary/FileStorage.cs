using System.IO;

namespace BinaryTreeIDictionary
{

    public class FileStorage : IStorage
    {
        public Stream GetReadStream(string name)
        {
            return new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public Stream GetWriteStream(string name)
        {
            return new FileStream(name, FileMode.Create, FileAccess.Write, FileShare.None);
            ;
        }
    }
}
