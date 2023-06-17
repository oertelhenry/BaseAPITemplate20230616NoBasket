using System;
using System.Security.Cryptography.X509Certificates;

namespace Core.Security
{
    public static class CertificateHandler
    {
        public static X509Certificate2 GetCertificate(string name)
        {
            X509Store userCaStore = new X509Store(SecuritySettingsAccessor.Current.CertificateStoreName, SecuritySettingsAccessor.Current.CertificateStoreLocation);
            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificatesInStore = userCaStore.Certificates;

                // Changed to search for invalid certs too.
                X509Certificate2Collection findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, name, false);
                X509Certificate2 clientCertificate = null;

                if (findResult.Count == 1)
                {
                    clientCertificate = findResult[0];
                }
                else
                {
                    throw new ArgumentException($"Unable to locate the correct client certificate. Found {findResult.Count} certificates with the name {name}. The search location used was {SecuritySettingsAccessor.Current.CertificateStoreLocation}");
                }

                return clientCertificate;
            }
            finally
            {
                userCaStore.Close();
            }
        }
    }
}