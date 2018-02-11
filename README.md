# SecureItAzureKeyVault

Secure "IT" with Azure Key Vault Presentation Demo Application for Code PaLOUsa 2018

Key Vault brings a new level of security by moving passwords and other sensitive information out of configuration 
files and into the Key Vault. You will learn how to securely retrieve sensitive information from Azure Key Vault using an X509 Certificate using both Powershell and 
a C# console application.  

Implementation steps include:

  **Creating an Azure Active Directory Application Id**

  **Creating a self signed X509 certificate (Certificate Authority issued certificate is recommended over a self-signed certificate)**

  *Installing the self signed certificate on the server or workstation*

  *Assigning permissions to access the certificate*

  Associating the certificate's public key with the Azure Application Id

  Creating an Azure Key Vault

  Granting permission to the Azure Application Id

  Storing secrets in the Azure Key Vault  

  Retrieving the secret from the vault using Power Shell

  Retrieve the TenantId from the Azure Portal for use with Power Shell

  Retrieving secrets from the vault using C# Console Application
