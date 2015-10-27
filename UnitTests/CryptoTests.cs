using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordScrambleServerJS;

namespace UnitTests
{
    [TestClass]
    public class CryptoTests
    {
        [TestMethod]
        public void ShortMessage()
        {
            TestRunner("Short", "abcd1234", null);
        }
        [TestMethod]
        public void ShortWithSalt()
        {
            TestRunner("Short", "abcd1234", new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
        }
        [TestMethod]
        public void LongMessage()
        {
            TestRunner("Hello WorldHello WorldHello WorldHello WorldHello WorldHello WorldHello WorldHello WorldHello WorldHello World", "abcd1234", null);
        }

        public void TestRunner(string text, string password, byte[] salt)
        {
            var encrypted = CryptoHelper.AES_Encrypt(text, password, salt);
            System.Console.WriteLine(encrypted);

            var decrypted = CryptoHelper.AES_Decrypt(encrypted, password, salt);
            System.Console.WriteLine(decrypted);
            Assert.AreEqual(text, decrypted, "Decrypted text did not match original text.");
        }
    }
}
