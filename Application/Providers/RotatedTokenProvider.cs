using System.Security.Cryptography;
using System.Text;
using RegisterCard.Application.Common.Interfaces;

namespace RegisterCard.Application.Providers;
public class RotatedTokenProvider : ITokenGenerator
{
    /// <summary>
    /// Generates a token from a rotation from the last 4 digits of a card
    /// </summary>
    /// <param name="cardNumber"></param>
    /// <param name="cvv"></param>
    /// <returns></returns>
    public Guid GenerateToken(string cardNumber, string cvv)
    {
        var last4Digits = cardNumber[^4..];
        var rotations = int.Parse(cvv);

        var array = last4Digits.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        var rotatedArray = RotateArray(array, rotations);

        var rotatedString = string.Join("", rotatedArray);
        var token = new Guid(GenerateGuidFromString(rotatedString));

        return token;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="rotations"></param>
    /// <returns></returns>
    private int[] RotateArray(int[] array, int rotations)
    {
        rotations = rotations % array.Length;

        if (rotations == 0)
            return array;

        int[] rotatedArray = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            int newIndex = (i + rotations) % array.Length;
            rotatedArray[newIndex] = array[i];
        }

        return rotatedArray;
    }

    /// <summary>
    /// Generates a deterministic guid from a string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private byte[] GenerateGuidFromString(string input)
    {
        using (var sha256 = SHA256.Create()) //hash de 32 bytes
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            //takes the first 16 bytes of SHA256 to generate a Guid (16 bytes)
            return hashBytes.Take(16).ToArray();
        }
    }
}