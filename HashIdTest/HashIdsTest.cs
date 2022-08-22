using HashidsNet;
using HashIdTest.Helpers;

namespace HashIdTest;

public static class HashIdsTest
{
    public static void Run()
    {
        Cons.WriteTitle("Test Hash Ids", '-', ConsoleColor.Yellow);

        const string salt = "2TAkKrVVOEaYASsnckHYBg";
        var hashids = new Hashids(salt, 11);

        var encodedIds = new List<string>
        {
            hashids.Encode(1),
            hashids.Encode(21),
            hashids.Encode(485),
            hashids.Encode(3040),
            hashids.Encode(51234)
        };

        foreach (var item in encodedIds)
        {
            Cons.Write(item, 0, ConsoleColor.Green);
            Cons.Write("=", 16, ConsoleColor.Gray);
            Cons.WriteLine(hashids.Decode(item).First(), 1, 
                ConsoleColor.DarkBlue, Cons.PositionType.FromCurrentPosition);
        }        
    }
}