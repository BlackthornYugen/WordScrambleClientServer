using System;
using System.Linq;

namespace WordScrambleGameJS
{
    class Program
    {
        static void Main(string[] args)
        {
            IWordScrambleGame game = new WordScrambleGame();
            Word scrambledWord = game.GetScrambledWord();
            Console.WriteLine("Can you unscramble this word?");
            Console.WriteLine("=> " + scrambledWord.scrambledWord);
            string guessedWord = Console.ReadLine();

            if (game.GuessWord(guessedWord, scrambledWord.unscrambledWord))
            {
                Console.WriteLine("Wow, You Won! ;-)");
            }
            else
            {
                Console.WriteLine("Sorry, You Loose :-(");
            }
            Console.ReadKey();
        }
    }

}
