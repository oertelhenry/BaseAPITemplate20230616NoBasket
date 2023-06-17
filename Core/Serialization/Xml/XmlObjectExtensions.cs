using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace System
{
    public static class XmlObjectExtensions
    {
        public static string ToXml<TType>(this TType value, IDictionary<string, string> namespaces = null, bool format = true)
            where TType : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TType));

            var emptyNamepsaces = new XmlSerializerNamespaces();

            if (namespaces != null)
            {
                foreach (var ns in namespaces)
                {
                    emptyNamepsaces.Add(ns.Key, ns.Value);
                }
            }

            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = format,
                Encoding = Encoding.UTF8,
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
            };

            using var sw = new StringWriter();
            using XmlWriter writer = XmlWriter.Create(sw, settings);

            serializer.Serialize(writer, value, emptyNamepsaces);
            var xml = sw.ToString();
            return xml;
        }
    }
}