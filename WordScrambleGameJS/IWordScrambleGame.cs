namespace WordScrambleGameJS
{
    public interface IWordScrambleGame
    {
        /**
		* Randonly selects a word, scrambles it, and retuns the original
		* (unscrambled) and the scrambed words as a single Word object
		**/
        Word GetScrambledWord();

        /**
		* Returns true if the guessed word correctly matches the
		* unscrambled word or false otherwise
		**/
        bool GuessWord(string guessedWord, string unscrambledWord);
    }
}
