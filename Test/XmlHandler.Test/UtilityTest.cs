using FluentAssertions;
using System.Text;

namespace XmlHandler.Test
{
    public class UtilityTest
    {
        [Fact]
        public void ObjectToXMLString_Test()
        {
            var user = new User { Name = "John", Location = "USA" };
            var xmlString = Utility.ObjectToXMLString(user);

            xmlString.Should().NotBeNullOrWhiteSpace();
            xmlString.Should().Contain("John");
            xmlString.Should().Contain("USA");
        }


        [Fact]
        public void ObjectToXMLString_Settings_Test()
        {
            var user = new User { Name = "John", Location = "USA" };
            var xmlString = Utility.ObjectToXMLString(user, new System.Xml.XmlWriterSettings { OmitXmlDeclaration = true });

            xmlString.Should().NotBeNullOrWhiteSpace();
            xmlString.Should().NotContain("<?xml version=\"1.0\" encoding=\"utf-16\"?>");
            xmlString.Should().Contain("John");
            xmlString.Should().Contain("USA");
        }


        [Fact]
        public void ObjectToXMLStream_Test()
        {
            var user = new User { Name = "John", Location = "USA" };

            var stream = Utility.ObjectToXMLStream(user);

            stream.Should().BeReadable();
            stream.Length.Should().BeGreaterThan(0);

            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);

            string xmlString = Encoding.UTF8.GetString(buffer);
            xmlString.Should().NotBeNullOrWhiteSpace();
            xmlString.Should().Contain("John");
            xmlString.Should().Contain("USA");
        }

        [Fact]
        public void ObjectToXMLFile_Test()
        {
            var user = new User { Name = "John", Location = "USA" };

            var filePath = Path.GetTempFileName();

            Utility.ObjectToXMLFile(user, filePath);

            File.Exists(filePath).Should().BeTrue();

            var xmlString = File.ReadAllText(filePath);
            xmlString.Should().NotBeNullOrWhiteSpace();
            xmlString.Should().Contain("John");
            xmlString.Should().Contain("USA");
        }


    }
}