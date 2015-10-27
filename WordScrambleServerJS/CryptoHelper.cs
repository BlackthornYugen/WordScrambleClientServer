using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WordScrambleServerJS
{
    public class CryptoHelper
    {
        public const int KEY_SIZE = 256;
        public const int BLOCK_SIZE = 128;

        public static string AES_Encrypt(string textToBeEncrypted, string passwordText, byte[] salt = null)
        {
            return Convert.ToBase64String(AES_Encrypt(
                textToBeEncrypted == null ? null : Encoding.UTF8.GetBytes(textToBeEncrypted),
                passwordText      == null ? null : Encoding.UTF8.GetBytes(passwordText     ),
                salt));
        }

        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes, byte[] salt = null)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            if (salt == null)
            {
                Console.Error.WriteLine("WARNING: AES Encrypt used without salt. Default salt used.");
                salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = KEY_SIZE;
                    AES.BlockSize = BLOCK_SIZE;

                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                    AES.Key = key.GetBytes(KEY_SIZE / 8);
                    AES.IV = key.GetBytes(BLOCK_SIZE / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static string AES_Decrypt(string textToBeDecrypted, string passwordText, byte[] salt = null)
        {
            return Encoding.UTF8.GetString(AES_Decrypt(
                textToBeDecrypted == null ? null : Convert.FromBase64String(textToBeDecrypted),
                passwordText      == null ? null : Encoding.UTF8.GetBytes(passwordText     ),
                salt));
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes, byte[] salt = null)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            if (salt == null)
            {
                Console.Error.WriteLine("WARNING: AES Decrypt used without salt. Default salt used.");
                salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            }

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = KEY_SIZE;
                    AES.BlockSize = BLOCK_SIZE;

                    var key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                    AES.Key = key.GetBytes(KEY_SIZE / 8);
                    AES.IV = key.GetBytes(BLOCK_SIZE / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}