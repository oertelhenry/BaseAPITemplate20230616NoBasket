using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;

namespace Core.Serialization
{
    public static class Cryptography
    {
        private static string EnvelopeNamespace => "http://schemas.xmlsoap.org/soap/envelope/";

        public static string SignSoapXml(XDocument document, X509Certificate2 cert)
        {
            var xmlDoc = new XmlDocument();
            using (var xmlReader = document.CreateReader())
            {
                xmlDoc.Load(xmlReader);
            }

            XmlNamespaceManager ns = new XmlNamespaceManager(xmlDoc.NameTable);
            ns.AddNamespace("soapenv", EnvelopeNamespace);

            if (!(xmlDoc.DocumentElement.SelectSingleNode(@"//soapenv:Body", ns) is XmlElement body))
            {
                throw new ArgumentException("No body tag found");
            }

            body.SetAttribute("id", "Body");
            SignedXml signedXml = new SignedXml(xmlDoc);

            KeyInfo keyInfo = new KeyInfo();
            signedXml.SigningKey = cert.PrivateKey;
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data();
            keyInfoData.AddIssuerSerial(cert.Issuer, cert.GetSerialNumberString());
            keyInfoData.AddCertificate(cert);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            Reference reference = new Reference
            {
                Uri = "#Body"
            };

            reference.AddTransform(new XmlDsigExcC14NTransform());
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            XmlElement signedElement = signedXml.GetXml();
            signedElement.Prefix = "ds";

            if (xmlDoc.DocumentElement.SelectSingleNode("//soapenv:Header", ns) is XmlElement soapHeader)
            {
                soapHeader.AppendChild(signedElement);
            }

            return xmlDoc.OuterXml;
        }
    }
}