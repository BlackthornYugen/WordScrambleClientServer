using System.Collections.Generic;
using System.ServiceModel.Channels;
using WordScrambleGameJS;

namespace WordScrambleServerJS
{
    public class WordScrambleService : IWordScrambleService
    {
        private static Dictionary<string, byte[]> salt = new Dictionary<string, byte[]>();
        private const string password = "Super Secret Password"; // TODO: Don't store password in source code
        
        public WordScrambleService()
        {
            if (!salt.ContainsKey(GetClientIP()))
            {
                salt.Add(GetClientIP(), null);
            }
            if (salt[GetClientIP()] == null)
            {
#if DEBUG
                // Fixed salt for debugging
                salt[GetClientIP()] = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
#else

                // Production uses a randomized salt
                salt[GetClientIP()] = new byte[8];                
                new System.Random().NextBytes(salt[GetClientIP()]);
#endif
            }
        }

        public Word GetScrambledWord()
        {
            var game = new WordScrambleGame();
            var word = game.GetScrambledWord();
            word.unscrambledWord = Encrypt(word.unscrambledWord);
            return word;
        }

        public bool GuessWord(string guessedWord, string encryptedUnscrambledWord)
        {
            return (guessedWord.Trim().CompareTo(Decrypt(encryptedUnscrambledWord)) == 0);
        }

        private string Encrypt(string someText)
        {
            System.Console.WriteLine(salt.ToString());
            return CryptoHelper.AES_Encrypt(someText, password, salt[GetClientIP()]);
        }

        private string Decrypt(string unscrambledWord)
        {
            return CryptoHelper.AES_Decrypt(unscrambledWord, password, salt[GetClientIP()]);
        }

        private string GetClientIP()
        {
            var context = System.ServiceModel.OperationContext.Current;
            var prop = context.IncomingMessageProperties;
            var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint.Address;
        }
    }
}
