using System;

namespace SecureITAzureKeyVault
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = AppSettingsHelper.GetSetting("appSettings:connectionString");

            Console.WriteLine($"\r\n\r\n{connectionString}\r\n\r\n");
            Console.ReadLine();
        }

    }
}
