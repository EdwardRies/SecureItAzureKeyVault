namespace SecureITAzureKeyVault
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    public static class AppSettingsHelper
    {
        private const string VaultString = "Vault:";
        private static readonly Lazy<IConfigurationRoot> configuration = new Lazy<IConfigurationRoot>(
            () => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build());

        public static string GetSetting(string key)
        {
            var value = $"{configuration.Value[key]}";
            return (!KeyVaultHelper.IsReady() || !value.StartsWith(VaultString)) ? value : GetVaultValue(value);
        }

        private static string GetVaultValue(string value)
        {
            value = value.Substring(6, value.Length - 6); // Remove Vault:
            var openingBrace = value.IndexOf("{", StringComparison.Ordinal); // Find Opening Brace
            var closingBrace = value.IndexOf("}", StringComparison.Ordinal); // Find Closing Brace
            var hasBraces = closingBrace > openingBrace && openingBrace >= 0; // Key Has Braces
            var vaultKey = hasBraces 
                ? value.Substring(openingBrace + 1, closingBrace - openingBrace - 1) 
                : value; // Key Vault Key

            var vaultValue = KeyVaultHelper.GetSecret(vaultKey);

            return hasBraces ? value.Replace($"{{{vaultKey}}}", vaultValue) : vaultValue;
        }

    }
}
