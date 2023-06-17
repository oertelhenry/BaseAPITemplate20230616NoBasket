using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System
{
    public static class XmlStringExtensions
    {
        public static TType ParseXml<TType>(this string value)
            where TType : class
        {
            using var stream = value.Trim().ToStream();
            using var reader = XmlReader.Create(stream, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Document });
            return new XmlSerializer(typeof(TType)).Deserialize(reader) as TType;
        }

        public static Stream ToStream(this string input)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            writer.Write(input);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}