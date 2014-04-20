namespace BSUIR.TermWork.ImageViewer.Shared
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class CryptoHelper
    {
        public static class Constants
        {
            public const int BasicLatinStartSymbolNumber = 0x0041;
            public const int BasicLatinEndSymbolNumber = 0x007A;
            public const int BasicLatinExcludedStartSymbolNumber = 0x005B;
            public const int BasicLatinExcludedEndSymbolNumber = 0x0060;
            public const int BasicLatinStartDigitSymbolNumber = 0x0030;
            public const int BasicLatinEndDigitSymbolNumber = 0x0039;
        }

        /// <exception cref="System.ArgumentOutOfRangeException">Salt length is below zero!</exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
        public static string GenerateSalt(int length)
        {
            if (length <= 0)
            {
                return string.Empty;
            }
            string resultString;
            var cryptoServiceProvider = new RNGCryptoServiceProvider();
            var saltBytes = new byte[length];
            cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            resultString = Encoding.Unicode.GetString(saltBytes);
            return resultString;
        }

        /// <exception cref="System.ArgumentException">Password or salt is null!</exception>
        /// <exception cref="System.Security.Cryptography.CryptographicException">The cryptographic service provider (CSP) cannot be acquired.</exception>
        public static string ComputePasswordHash(string password, string salt)
        {
            string resultString;
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
            {
                throw new ArgumentException("Password or salt cannot be null or empty!");
            }
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] passwordHash = SHA512.Create().ComputeHash(passwordBytes);
            string passwordHashString = Encoding.Unicode.GetString(passwordHash);
            string passwordSaltConcat = string.Concat(passwordHashString, salt);
            byte[] passwordSaltBytes = Encoding.Unicode.GetBytes(passwordSaltConcat);
            byte[] resultBytes = SHA512.Create().ComputeHash(passwordSaltBytes);
            resultString = Encoding.Unicode.GetString(resultBytes);
            return resultString;
        }

        public static string GenerateUrlCompatibleName(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("The length parameter " + "cannot be below zero!");
            }
            var seed = (uint)(Guid.NewGuid().GetHashCode() + (uint)Int32.MaxValue);

            var result = new StringBuilder(length);
            var twister = new MersenneTwister(seed);

            for (int i = 0; i < length; ++i)
            {
                result.Append(
                    (char)twister.Next(Constants.BasicLatinStartSymbolNumber, Constants.BasicLatinEndSymbolNumber));
            }

            for (int i = Constants.BasicLatinExcludedStartSymbolNumber;
                 i <= Constants.BasicLatinExcludedEndSymbolNumber;
                 ++i)
            {
                result.Replace(
                    (char)i,
                    (char)
                    twister.Next(Constants.BasicLatinStartDigitSymbolNumber, Constants.BasicLatinEndDigitSymbolNumber));
            }
            return result.ToString();
        }
    }
}
