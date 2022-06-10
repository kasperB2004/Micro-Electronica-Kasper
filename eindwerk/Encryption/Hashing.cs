using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;

namespace eindwerk.Encryption
{
    public class Hashing
    {
        #region SaltGeneration
        static int DefaultLength = 32;
        public static byte[] GenerateSalt()
        {
            return GenerateSalt(DefaultLength);
        }
        public static byte[] GenerateSalt(int Length)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[Length];
            rng.GetNonZeroBytes(salt);
            return salt;
        }
        #endregion

        #region Hashing
        public static string hash(string password)
        {
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = 1, // higher than "Lanes" doesn't help (or hurt)
                Password = Encoding.UTF8.GetBytes(password),
                Salt = GenerateSalt(), // >= 8 bytes if not null
                HashLength = 20 // >= 4
            };
            var hash = Argon2.Hash(config);
            return hash;
        }

        public static string hash(string password,byte[] Salt)
        {
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = 1, // higher than "Lanes" doesn't help (or hurt)
                Password = Encoding.UTF8.GetBytes(password),
                Salt = Salt, // >= 8 bytes if not null
                HashLength = 20 // >= 4
            };
            var hash = Argon2.Hash(config);
            return hash;
        }
        #endregion
        #region Verify
        public static bool verify(string hash, string password)
        {
            if(Argon2.Verify(hash, password))
            {
                return true;
            }
            return false;
        }
        #endregion 

        public static string PasswordGenerator()
        {
            IEnumerable<char> characterSet =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
        "abcdefghijklmnopqrstuvwxyz" +
        "0123456789" +
        "*$-+?_&=!%{}/";
            var characterArray = characterSet.Distinct().ToArray();
            var bytes = new byte[8 * 8];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(bytes);
            var result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
