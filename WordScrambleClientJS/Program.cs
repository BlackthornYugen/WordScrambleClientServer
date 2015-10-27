using System;

namespace WordScrambleClientJS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
#if DEBUG
                var gameService = new WordScrambleService.WordScrambleServiceClient(
                    binding: new System.ServiceModel.BasicHttpBinding(), 
                    remoteAddress: new System.ServiceModel.EndpointAddress("http://localhost:2170/WordScrambleService.svc"));
#else
                var gameService = new WordScrambleService.WordScrambleServiceClient();
#endif
                var scrambledWord = gameService.GetScrambledWord();
                Console.WriteLine("Can you unscramble this word?");
                Console.WriteLine("=> " + scrambledWord.scrambledWord);
                string guessedWord = Console.ReadLine();

                if (gameService.GuessWord(guessedWord, scrambledWord.unscrambledWord))
                {
                    Console.WriteLine("Wow, You Won! ;-)");
                }
                else
                {
                    Console.WriteLine("Sorry, You Loose :-(");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.GetBaseException().Message);
            }

            try
            {
                Console.ReadKey();
            }
            catch (InvalidOperationException)
            {
                // Don't throw exception when we can't connect to TTY (pipes)
            }
        }
    }
}
