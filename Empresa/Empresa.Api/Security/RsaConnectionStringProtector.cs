using System.Security.Cryptography;
using System.Text;

namespace Empresa.Api.Security;

public static class RsaConnectionStringProtector
{
    private static readonly string privateKey = File.ReadAllText("/keys/private.pem");
    public static readonly string publicKeyPem = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqmK94N1yU0dkDCI9xtd4QhC2LHfOZNmkU5EMZFiMW7TVuBHOi/rFlPDptUrfSWkxWj+7gyFcgbOUufhlS/xcCu/Pkj4sbYgiafeTNRJlEXPLkAbdglaqEBLSzfQ7jzsRdNZtqVdyN48y7363TE9coUsB/poShfwbaxb9gg668evToFqRwyUTH+w+IuW8WVDwAu2cw3shj7QwFYJgH6op5+6Q7Ei8tvHCx9eKPr7JTsduSWnA8rkE7C/uGrmLo3Xnacltuw4KpnpRGzDAXT/fuHoy+U3sAVhaM5cniPfMhIhudFjJmXcukxqxA9/Y+hZojPUhEKq+EsqQviTUQZvh1QIDAQAB";
    public static string Encrypt(string plainText, string publicKeyPem)
    {
        publicKeyPem = publicKeyPem.Replace("\\n", "\n").Replace("\\r", "\r"); // Corrige quebras de linha
        using var rsa = RSA.Create();
        rsa.ImportFromPem(publicKeyPem);

        var data = Encoding.UTF8.GetBytes(plainText);
        var encrypted = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);

        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(string cipherTextBase64, string privateKeyPem)
    {
        privateKeyPem = privateKeyPem.Replace("\\n", "\n").Replace("\\r", "\r"); // Corrige quebras de linha

        using var rsa = RSA.Create();
        rsa.ImportFromPem(privateKeyPem);

        var cipherBytes = Convert.FromBase64String(cipherTextBase64);
        var decrypted = rsa.Decrypt(cipherBytes, RSAEncryptionPadding.OaepSHA256);

        return Encoding.UTF8.GetString(decrypted);
    }

    public static (string PublicKeyPem, string PrivateKeyPem) GeneratePemKeys(int keySize = 2048)
    {
        using var rsa = RSA.Create(keySize);

        // Formatos compatíveis com ImportFromPem
        var privateKeyPem = rsa.ExportPkcs8PrivateKeyPem();          // -----BEGIN PRIVATE KEY-----
        var publicKeyPem = rsa.ExportSubjectPublicKeyInfoPem();     // -----BEGIN PUBLIC KEY-----

        return (publicKeyPem, privateKeyPem);
    }
}