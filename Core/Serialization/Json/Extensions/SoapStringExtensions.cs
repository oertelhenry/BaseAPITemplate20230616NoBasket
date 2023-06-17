using System.Text.RegularExpressions;
using System.Xml;

namespace Core.Domain.Extensions
{
    public static class SoapStringExtensions
    {
        public static XmlNode ToSoapBodyContent(this string xml)
        {
            var ns = @"xmlns(:\w+)?=""([^""]*)""|xsi(:\w+)?=""([^""]*)""";
            var openTag = @"<(\w+:)?";
            var closTag = @"</(\w+:)?";

            xml = Regex.Replace(xml, ns, "");
            xml = Regex.Replace(xml, openTag, "<");
            xml = Regex.Replace(xml, closTag, "</");

            var doc = new XmlDocument();
            doc.LoadXml(xml);

            var body = doc.SelectSingleNode("/*[local-name()='Envelope']/*[local-name()='Body']");
            return body;
        }
    }
}