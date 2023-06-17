using System.Collections.Generic;
using System.Xml.Linq;

namespace Core.Domain.Extensions
{
    public static class SoapObjectExtensions
    {
        public static XDocument ToSoapEnvelope<TType>(this TType body, XElement header = null, IDictionary<string, string> namespaces = null)
            where TType : class
        {
            var xml = body.ToXml(namespaces, false);

            XDocument soapEnvelopeDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace xsd = "http://tasima/common/ws/schema/";
            XNamespace sch = xsd;

            var content = new List<object>
            {
                new XAttribute(XNamespace.Xmlns + "soapenv", soap.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "sch", sch.NamespaceName)
            };

            if (namespaces != null)
            {
                foreach (var ns in namespaces)
                {
                    XNamespace xns = ns.Value;
                    content.Add(new XAttribute(XNamespace.Xmlns + ns.Key, xns.NamespaceName));
                }
            }

            if (header != null)
            {
                content.Add(header);
            }
            else
            {
                content.Add(new XElement(soap + "Header"));
            }

            content.Add(new XElement(soap + "Body", XDocument.Parse(xml).Root));
            XElement envelope = new XElement(soap + "Envelope", content.ToArray());

            soapEnvelopeDocument.Add(envelope);

            return soapEnvelopeDocument;
        }

        public static XElement ToSoapHeader<TType>(this TType body)
                    where TType : class
        {
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";

            var xml = body.ToXml();

            var header = new XElement(
                soap + "Header",
                new XAttribute(XNamespace.Xmlns + "xsd", xsd.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi.NamespaceName),
                 XDocument.Parse(xml).Root
                );

            return header;
        }
    }
}