using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using System.Xml.Linq;

namespace Core.Serialization.Soap
{
    public static class Cryptography
    {
        public static string SignXml(XDocument document, X509Certificate2 cert)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = document.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }

            var signedXml = new SignedXml(xmlDocument)
            {
                SigningKey = cert.PrivateKey
            };

            // Create a reference to be signed.
            Reference reference = new Reference
            {
                Uri = ""
            };

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env =
               new XmlDsigEnvelopedSignatureTransform(true);
            reference.AddTransform(env);

            //canonicalize
            XmlDsigC14NTransform c14t = new XmlDsigC14NTransform();
            reference.AddTransform(c14t);

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data keyInfoData = new KeyInfoX509Data(cert);
            KeyInfoName kin = new KeyInfoName
            {
                Value = "Public key of certificate"
            };
            RSACryptoServiceProvider rsaprovider = (RSACryptoServiceProvider)cert.PublicKey.Key;
            RSAKeyValue rkv = new RSAKeyValue(rsaprovider);
            keyInfo.AddClause(kin);
            keyInfo.AddClause(rkv);
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            xmlDocument.DocumentElement.AppendChild(
                xmlDocument.ImportNode(xmlDigitalSignature, true));

            return xmlDocument.OuterXml;
        }
    }
}