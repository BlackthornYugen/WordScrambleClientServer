using System.ServiceModel;
using WordScrambleGameJS;

namespace WordScrambleServerJS
{
    [ServiceContract]
    public interface IWordScrambleService
    {
        [OperationContract]
        Word GetScrambledWord();

        [OperationContract]
        bool GuessWord(string guessedWord, string encryptedUnscrambledWord);
    }
}
