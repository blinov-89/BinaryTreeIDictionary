using System.IO;

namespace BinaryTreeIDictionary
{
    public interface IStorage
    {
        Stream GetReadStream(string name);
        Stream GetWriteStream(string name);
    }
}
