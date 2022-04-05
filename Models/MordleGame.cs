using System.ComponentModel;

namespace Mordle;
/// <summary>
/// Represent a game of mordle, with every tries made
/// </summary>

#region ORM
public partial class MordleGame
{
    public bool win { get; set; }
    public bool lose { get; set; }
    public readonly Mordle.Data.DevDbContext _context;
    public string wordToGuess { get; set; }
    char[] _targetChars { get; set; }

    [DefaultValue(6)]
    public int maxGuesses { get; set; }

    public enum Guess { correct, misplaced, wrong }

    public Dictionary<string, Guess[]> guesses;

    public MordleGame(string wordToGuess, int maxGuesses)
    {
        this.wordToGuess = wordToGuess = wordToGuess.ToUpperInvariant();
        _targetChars = this.wordToGuess.ToCharArray();

        this.maxGuesses = maxGuesses;

        guesses = new Dictionary<string, Guess[]>();
    }
}
#endregion

#region Logic
public partial class MordleGame
{

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

    /// <summary>
    /// Checks guess and if game is finished
    /// </summary>
    /// <param name="guess"></param>
    public void MakeAGuess(string guess)
    {
        // Check if guess match lentgh
        if (guess == null || guess.Length != wordToGuess.Length)
            return;

        // Unless guess was already made
        if (!guesses.ContainsKey(guess))
            guesses.Add(guess, Compare(guess));

        // Check if player won or lost
        if (guess == wordToGuess)
            win = true;
        else if (guesses.Count >= maxGuesses)
            lose = true;
    }
}
#endregion