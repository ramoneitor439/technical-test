using System.Security.Cryptography;
using System.Text;

namespace Project2.Tools;

public static class Encryption
{
    public static string DecryptAes(string input)
    {
        var aesKey = Environment.GetEnvironmentVariable("Encryption__AES_KEY") ?? throw new Exception("Missing environment variable: Encryption__AES_KEY");
        var iv = Environment.GetEnvironmentVariable("Encryption__IV") ?? throw new Exception("Missing environment variable: Encryption__IV");

        using var aes = Aes.Create();
        
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(input));
        using var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }

    public static bool CheckHash(string input, string hash)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        var inputHash = builder.ToString();
        return inputHash == hash;
    }
}