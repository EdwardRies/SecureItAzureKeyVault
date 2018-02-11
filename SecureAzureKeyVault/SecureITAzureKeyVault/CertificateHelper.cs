namespace SecureITAzureKeyVault
{
    using System.Security.Cryptography.X509Certificates;

    public static class CertificateHelper
    {
        public static X509Certificate2 FindCertificateByThumbprint(string certificateThumbPrint)
        {
            return FindCertificate(StoreName.My, StoreLocation.CurrentUser, certificateThumbPrint)
                   ?? FindCertificate(StoreName.My, StoreLocation.LocalMachine, certificateThumbPrint);
        }

        private static X509Certificate2 FindCertificate(StoreName storeName, StoreLocation storeLocation, string certificateThumbPrint)
        {
            using (var store = new X509Store(storeName, storeLocation))
            {
                try
                {
                    store.Open(OpenFlags.ReadOnly);

                    var certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbPrint, false);

                    return certificateCollection.Count == 0 ? null : certificateCollection[0];
                }
                finally
                {
                    store.Close();
                }
            }
        }

    }
}
