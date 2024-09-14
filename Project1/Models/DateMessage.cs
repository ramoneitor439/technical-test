using Project1.Tools;

namespace Project1.Models;

public class DateMessage
{
    public string DateHash { get; init; }
    public string DateEncryption { get; init; }
    
    public DateMessage(DateTime date)
    {
        var dateString = date.ToString("h:mm:ss tt zz");
        
        DateHash = Encryption.ToSha256(dateString);
        DateEncryption = Encryption.ToAes(dateString);
    }
    
}