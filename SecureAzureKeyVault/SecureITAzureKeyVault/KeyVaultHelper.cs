namespace SecureITAzureKeyVault
{
    using Microsoft.Azure.KeyVault;
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System;
    using System.Threading.Tasks;

    public static class KeyVaultHelper
    {
        private static readonly string aadClientId;
        private static readonly string keyVaultUri;
        private static readonly string certificateThumbprint;

        static KeyVaultHelper()
        {
            aadClientId = AppSettingsHelper.GetSetting("AppSettings:AzureKeyVault:AadClientId");
            keyVaultUri = AppSettingsHelper.GetSetting("AppSettings:AzureKeyVault:KeyVaultUri");
            certificateThumbprint = AppSettingsHelper.GetSetting("AppSettings:AzureKeyVault:CertificateThumbprint");
            LoggerCallbackHandler.UseDefaultLogging = false;
        }

        public static bool IsReady()
        {
            return !string.IsNullOrEmpty(aadClientId)
                   && !string.IsNullOrEmpty(keyVaultUri)
                   && !string.IsNullOrEmpty(certificateThumbprint);
        }

        public static string GetSecret(string key)
        {
            if (!IsReady())
            {
                throw new InvalidOperationException("Missing or Invalid Key Vault Credentials");
            }

            // Prevents Dead Locks in Web Applications
            return Task.Run(async () =>
            {
                using (var keyVaultClient = new KeyVaultClient(GetTokenByCertificate))
                {
                    return await keyVaultClient.GetSecretAsync(keyVaultUri, key);
                }
            }).Result.Value;
        }

        private static async Task<string> GetTokenByCertificate(string authority, string resource, string scope)
        {
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var authenticationResultTask = await context.AcquireTokenAsync(resource, CreateAssertionCertificate());

            if (authenticationResultTask == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT Authentication Token");
            }

            return authenticationResultTask.AccessToken;
        }

        private static ClientAssertionCertificate CreateAssertionCertificate()
        {
            var certificate = CertificateHelper.FindCertificateByThumbprint(certificateThumbprint);

            return new ClientAssertionCertificate(aadClientId, certificate);
        }

    }
}
