using System.Security.Cryptography;
using System.Text;

namespace CouperfectServer.Application.Services;

public interface ICryptographyService
{
    public const int DefaultKeySize = 64;
    public const int DefaultIterations = 350000;

    (byte[] Hash, byte[] Salt) GenerateSaltedSHA512Hash(string rawText, int keySize = DefaultKeySize, int iterations = DefaultIterations);
    bool CompareSaltedSHA512Hash(string rawText, byte[] cypherText, byte[] salt, int keySize = DefaultKeySize, int iterations = DefaultIterations);
}

public class CryptographyService : ICryptographyService
{
    private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public bool CompareSaltedSHA512Hash(string rawText, byte[] cypherText, byte[] salt, int keySize = 64, int iterations = 350000)
    {
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(rawText, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, cypherText);
    }

    public (byte[] Hash, byte[] Salt) GenerateSaltedSHA512Hash(string rawText, int keySize = 64, int iterations = 350000)
    {
        var salt = RandomNumberGenerator.GetBytes(keySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(rawText),
            salt,
            iterations,
            hashAlgorithm,
            keySize
        );

        return (hash, salt);
    }
}
