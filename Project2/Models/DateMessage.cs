using Project2.Tools;

namespace Project2.Models;

public record DateMessage
{
    public string DateHash { get; init; } = string.Empty;
    public string DateEncryption { get; init; } = string.Empty;
    
    public bool IsCorrupted()
    {
        var date = Encryption.DecryptAes(DateEncryption);
        return !Encryption.CheckHash(date, DateHash);
    }

    public string GetDate() => Encryption.DecryptAes(DateEncryption);
}