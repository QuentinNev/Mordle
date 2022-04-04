using System.ComponentModel;

namespace Mordle;
/// <summary>
/// Represent a game of mordle, with every tries made
/// </summary>
public class MordleGame
{
    public readonly Mordle.Data.DevDbContext _context;
    public string wordToGuess { get; set; }
    char[] _targetChars { get; set; }

    [DefaultValue(6)]
    public int maxAttempt { get; set; }

    public enum Guess { correct, misplaced, wrong }

    public Dictionary<string, Guess[]> guesses;

    public MordleGame(string wordToGuess, int maxAttempt)
    {

        this.wordToGuess = wordToGuess = wordToGuess.ToUpperInvariant();
        _targetChars = this.wordToGuess.ToCharArray();

        this.maxAttempt = maxAttempt;

        guesses = new Dictionary<string, Guess[]>();
    }

    /// <summary>
    /// Compares every character and return an array indicating how letters matched the guess
    /// </summary>
    /// <param name="guess"></param>
    /// <returns></returns>
    private Guess[] Compare(string guess)
    {
        Guess[] result = new Guess[guess.Length];

        char[] guessChars = guess.ToCharArray();

        for (int i = 0; i < guessChars.Length; i++)
        {
            // Check if char is present
            if (_targetChars.Contains(guess[i]))
            {
                // Correct answer
                if (guess[i] == _targetChars[i])
                {
                    result[i] = Guess.correct;
                }
                else
                {
                    result[i] = Guess.misplaced;
                }
            }
            else
            {
                result[i] = Guess.wrong;
            }
        }

        return result;
    }

    public bool MakeAGuess(string guess)
    {
        // Check if guess match lentgh
        if (guess == null || guess.Length != wordToGuess.Length)
            return false;

        if (!guesses.ContainsKey(guess))
            guesses.Add(guess, Compare(guess));

        return (wordToGuess == guess);
    }
}