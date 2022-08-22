using System.Buffers.Text;
using System.Runtime.InteropServices;
using HashIdTest.Helpers;

namespace HashIdTest;

public static class GuidIdsTest
{
    public static void Run()
    {
        Cons.WriteTitle("Test Guid Ids", '-', ConsoleColor.Yellow);

        void WriteData(string label, object value)
        {
            Cons.Write(label, 0, ConsoleColor.Green);
            Cons.Write("=", 16, ConsoleColor.Gray);
            Cons.WriteLine(value, 1, ConsoleColor.DarkBlue, Cons.PositionType.FromCurrentPosition);
        }

        var guidId = Guid.NewGuid();
        WriteData("Guid", guidId);

        var idStr = guidId.ToStringId();
        WriteData("Id String", idStr);

        var id = idStr.ToGuidFromIdString();
        WriteData("Back To Guid", id);
    }
}

public static class GuidIdExtensions
{
    private const char EqualsChar = '=';
    private const char Hyphen = '-';
    private const char Underscore = '_';
    private const char Slash = '/';
    private const byte SlashByte = (byte)Slash;
    private const char Plus = '+';
    private const byte PlusByte = (byte)Plus;

    public static string ToStringId(this Guid id)
    {
        Span<byte> idBytes = stackalloc byte[16];
        Span<byte> base64Bytes = stackalloc byte[24];
        MemoryMarshal.TryWrite(idBytes, ref id);
        Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);
		
        Span<char> finalChars = stackalloc char[22];
        for (var i = 0; i < 22; i++)
        {
            finalChars[i] = base64Bytes[i] switch
            {
                SlashByte => Hyphen,
                PlusByte => Underscore,
                _ => (char)base64Bytes[i]
            };
        }
		
        return new string(finalChars);
    }

    public static Guid ToGuidFromIdString(this string id)
    {
        Span<char> base64Chars = stackalloc char[24];
        for (var i = 0; i < 22; i++)
        {
            base64Chars[i] = id[i] switch
            {
                Hyphen => Slash,
                Underscore => Plus,
                _ => id[i]
            };
        }
        base64Chars[22] = EqualsChar;
        base64Chars[23] = EqualsChar;

        Span<byte> idBytes = stackalloc byte[16];
        Convert.TryFromBase64Chars(base64Chars, idBytes, out _);
        return new Guid(idBytes);
    }
}