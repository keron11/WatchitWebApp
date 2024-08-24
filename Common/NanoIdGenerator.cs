using System;
using System.Security.Cryptography;

public static class NanoIdGenerator
{
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-";
    private const int Size = 14;

    public static string Generate()
    {
        var bytes = new byte[Size];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }

        var id = new char[Size];
        for (var i = 0; i < Size; i++)
        {
            id[i] = Alphabet[bytes[i] % Alphabet.Length];
        }

        return new string(id);
    }
}