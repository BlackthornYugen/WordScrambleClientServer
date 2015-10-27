using System;
using System.Linq;

namespace WordScrambleGameJS
{
    public class WordScrambleGame : IWordScrambleGame
    {
#if DEBUG
        // Seeded random for debugging
        Random random = new Random(2015);
#else
        Random random = new Random();
#endif
        string[] words = { "kitchener", "waterloo", "toronto", "ottawa",
                    "montreal", "calgary", "edmonton", "vancouver" };

        public Word GetScrambledWord()
        {
            string selectedWord = words[random.Next(words.Length)];
            string scrambledWord = ScrambleWord(selectedWord);
            Word wordObj = new Word();
            wordObj.unscrambledWord = selectedWord;
            wordObj.scrambledWord = scrambledWord;
            return wordObj;
        }

        public bool GuessWord(string guessedWord, string unscrambledWord)
        {
            return (guessedWord.CompareTo(unscrambledWord) == 0);
        }
        

        private string ScrambleWord(string word)
        {
            char[] chars = word.ToArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int randomIndex = random.Next(0, chars.Length);
                char temp = chars[randomIndex];
                chars[randomIndex] = chars[i];
                chars[i] = temp;
            }
            return new string(chars);
        }
    }
}
