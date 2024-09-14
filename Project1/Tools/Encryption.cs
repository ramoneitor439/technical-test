using System.Security.Cryptography;
using System.Text;

namespace Project1.Tools;

public static class Encryption
{
    public static string ToSha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    public static string ToAes(string input)
    {
        var aesKey = Environment.GetEnvironmentVariable("Encryption__AES_KEY") ?? throw new Exception("Missing environment variable: Encryption__AES_KEY");
        var iv = Environment.GetEnvironmentVariable("Encryption__IV") ?? throw new Exception("Missing environment variable: Encryption__IV");

        using var aes = Aes.Create();
        
        aes.Key = Encoding.UTF8.GetBytes(aesKey);
        aes.IV = Encoding.UTF8.GetBytes(iv);

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(input);
            }
        }
        return Convert.ToBase64String(ms.ToArray());
    }
}