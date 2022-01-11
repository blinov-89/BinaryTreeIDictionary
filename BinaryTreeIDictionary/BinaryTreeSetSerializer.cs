using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BinaryTreeIDictionary
{
    public class BinaryTreeDictionarySerializer
    {
        private IStorage storage;

        public BinaryTreeDictionarySerializer(IStorage storage)
        {
            this.storage = storage;
        }

        public void Save<Tkey,Tvalue>(string name, BinaryTreeDictionary<Tkey,Tvalue> dictionary, bool closeStream = true) where Tkey : IComparable
        {
            var ser = new DataContractJsonSerializer(typeof(BinaryTreeDictionary<Tkey, Tvalue>));
            Stream stream = storage.GetWriteStream(name);
            ser.WriteObject(stream, dictionary);

            if (closeStream)
                stream.Close();
        }

        public BinaryTreeDictionary<Tkey, Tvalue> Load<Tkey, Tvalue>(string name, bool closeStream = true) where Tkey : IComparable
        {
            var ser = new DataContractJsonSerializer(typeof(BinaryTreeDictionary<Tkey, Tvalue>));
            Stream stream = storage.GetReadStream(name);
            var result = (BinaryTreeDictionary<Tkey, Tvalue>)ser.ReadObject(stream);

            if (closeStream)
                stream.Close();

            return result;
        }
    }
}
