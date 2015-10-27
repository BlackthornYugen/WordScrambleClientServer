using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class WordScrambleServiceTests
    {
        [TestMethod]
        public void UnitTest1()
        {
            var wordScrambleServer = new WordScrambleServerJS.WordScrambleService();
            wordScrambleServer.GetScrambledWord();
            var scrambledWord = wordScrambleServer.GetScrambledWord();
            System.Console.WriteLine(scrambledWord.scrambledWord);
            System.Console.WriteLine(scrambledWord.unscrambledWord);
            Assert.IsInstanceOfType(scrambledWord, typeof(WordScrambleGameJS.Word));
            Assert.IsNotNull(scrambledWord);
        }
    }
}
