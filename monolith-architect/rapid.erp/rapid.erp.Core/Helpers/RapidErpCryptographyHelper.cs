
namespace rapid.erp.Core.Helpers
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Encryption Decription Library. This script also used in SQL Server as copy. Please donot change anything.
    /// </summary>
    public sealed class RapidErpHelper
    {
        private static string _hashKey;
        private static string _saltKey;
        private static string GetHashedKey(string key)
        {
            byte[] hashKey;
            using (var sha2 = new SHA256Managed())
            {
                byte[] rawKey = Encoding.UTF8.GetBytes(key);
                hashKey = sha2.ComputeHash(rawKey);
                Array.Resize(ref rawKey, 32);
            }
            return Convert.ToBase64String(hashKey);
        }
        private static string GetHashedSalt(string key)
        {
            byte[] hashIv;
            using (var sha2 = new SHA256Managed())
            {
                byte[] rawIv = Encoding.UTF8.GetBytes(key);
                hashIv = sha2.ComputeHash(rawIv);
                Array.Resize(ref hashIv, 16);
            }
            return Convert.ToBase64String(hashIv);
        }

        public static string EncryptString(string plainText)
        {
            return EncryptString(plainText, RapidErpSettings.KeyPassword, RapidErpSettings.SaltPassword);
        }
        public static string DecryptString(string plainText)
        {
            return DecryptString(plainText, RapidErpSettings.KeyPassword, RapidErpSettings.SaltPassword);
        }
        public static string EncryptString(string plainText, string keyPass, string saltPass)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            if (_hashKey == null) _hashKey = GetHashedKey(keyPass);
            if (_saltKey == null) _saltKey = GetHashedSalt(saltPass);

            var ky = Convert.FromBase64String(_hashKey);
            var iv = Convert.FromBase64String(_saltKey);
            var encrypted = EncryptStringToBytes(plainText, ky, iv);
            return encrypted;
        }
        public static string DecryptString(string plainText, string keyPass, string saltPass)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            if (_hashKey == null) _hashKey = GetHashedKey(keyPass);
            if (_saltKey == null) _saltKey = GetHashedSalt(saltPass);
            var ky = Convert.FromBase64String(_hashKey);
            var iv = Convert.FromBase64String(_saltKey);
            var encrypted = DecryptStringFromBytes(plainText, ky, iv);
            return encrypted;
        }
        private static string EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            try
            {
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                byte[] encrypted;

                using (RijndaelManaged aesAlg = new RijndaelManaged())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt =
                            new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "RapidErp : Something went wrong.";
            }

        }
        private static string DecryptStringFromBytes(string cipherText, byte[] Key, byte[] IV)
        {
            try
            {
                // Check arguments.
                if (cipherText == null || cipherText.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                byte[] cipher = Convert.FromBase64String(cipherText);

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                // Create an Aes object
                // with the specified key and IV.
                using (RijndaelManaged aesAlg = new RijndaelManaged())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(cipher))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
                bool g = false;
                foreach (char c in plaintext)
                {
                    if (c > 127) g = true;
                }
                plaintext = g ? "" : plaintext;
                return plaintext;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "RapidErp : Something went wrong.";
            }

        }
    }

}