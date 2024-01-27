using System.Security.Cryptography;
using System.Text;

namespace WordsApp.Authentication;

/// <summary>
/// Creates GUID based on hash of string
/// </summary>
internal static class IdHasher
{
    public static Guid StringToGUID(string value)
    {
        var data = MD5.HashData(Encoding.Default.GetBytes(value));
        return new Guid(data);
    }
}