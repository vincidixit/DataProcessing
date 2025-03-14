using System.Xml.Serialization;
using System.Xml;

namespace XmlHandler
{
    public static class Utility
    {
        public static void ObjectToXMLFile<T>(T obj, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            using (var xmlWriter = XmlWriter.Create(fileStream))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(xmlWriter, obj);
            }
        }

        public static string ObjectToXMLString<T>(T obj)
        {
            using (var stringWriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static string ObjectToXMLString<T>(T obj, XmlWriterSettings xmlWriterSettings)
        {
            // utf-16
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter, xmlWriterSettings))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
                return stringWriter.ToString();
            }
        }

        public static Stream ObjectToXMLStream<T>(T obj)
        {
            // utf-8
            var stream = new MemoryStream();

            using (var writer = XmlWriter.Create(stream))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
            }

            stream.Position = 0;
            return stream;
        }

    }
}
