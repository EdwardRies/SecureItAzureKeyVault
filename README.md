# SecureItAzureKeyVault

Secure "IT" with Azure Key Vault Presentation Demo Application for Code PaLOUsa 2018

Key Vault brings a new level of security by moving passwords and other sensitive information out of configuration 
files and into the Key Vault. You will learn how to securely retrieve sensitive information from Azure Key Vault using an X509 Certificate using both Powershell and 
a C# console application.  

Implementation steps include:

  **Creating an Azure Active Directory Application Id**

  **Creating a self signed X509 certificate (Certificate Authority issued certificate is recommended over a self-signed certificate)**
   1. Create pvk and cer files (seperate private and public keys)
   makecert -r -pe -n "CN=www.hightechhangout.com" -a sha512 -len 4096  -b 01/01/2018 -e 12/31/2029 -sv MyTestCert.pvk  MyTestCert.cer

   2. convert pvk and cer files into pkx file (private and public key combined)
   pvk2pfx -pvk MyTestCert.pvk -spc MyTestCert.cer -pfx MyTestCert.pfx -po PasswordHere
   
  **Installing the self signed certificate on the server or workstation**

  **Assigning permissions to access the certificate**

  **Associating the certificate's public key with the Azure Application Id**

  **Creating an Azure Key Vault**

  **Granting permission to the Azure Application Id**

  **Storing secrets in the Azure Key Vault**

  **Retrieving the secret from the vault using Power Shell**

  **Retrieve the TenantId from the Azure Portal for use with Power Shell**

  **Retrieving secrets from the vault using C# Console Application**
