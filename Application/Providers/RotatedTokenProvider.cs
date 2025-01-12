using System.Security.Cryptography;
using System.Text;
using RegisterCard.Application.Common.Interfaces;

namespace RegisterCard.Application.Providers;
internal class RotatedTokenProvider : ITokenGenerator
{
    /// <summary>
    ///  gera um token a partir de uma rotação dos últimos 4 dígitos do cartão
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

    //private int[] RotateArray(int[] array, int rotations)
    //{
    //    rotations %= array.Length;
    //    return array[^rotations..].Concat(array[..^rotations]).ToArray();
    //}

    /// <summary>
    /// Função para rotacionar o array
    /// </summary>
    /// <param name="array"></param>
    /// <param name="rotations"></param>
    /// <returns></returns>
    private int[] RotateArray(int[] array, int rotations)
    {
        // Garante que o número de rotações seja dentro do limite do tamanho do array
        rotations = rotations % array.Length;

        if (rotations == 0)
            return array;

        int[] rotatedArray = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            int newIndex = (i + rotations) % array.Length;  // Calcula o novo índice
            rotatedArray[newIndex] = array[i];
        }

        return rotatedArray;
    }

    /// <summary>
    /// Gerar um GUID determinístico a partir de uma string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private byte[] GenerateGuidFromString(string input)
    {
        using (var sha256 = SHA256.Create()) //hash de 32 bytes
        {
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            // pega os primeiros 16 bytes do SHA256 para gerar um Guid (16 bytes)
            return hashBytes.Take(16).ToArray();
        }
    }
}