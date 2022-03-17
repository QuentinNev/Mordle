using System.ComponentModel;

namespace Mordle;
/// <summary>
/// Represent a game of mordle, with every tries made
/// </summary>
public class MordleGame
{
    public string wordToGuess { get; set; }

    [DefaultValue(6)]
    public int maxAttempt { get; set; }

    public enum Guess { Correct, WrongPlace, NotPresent }

    Dictionary<string, Guess[]> _guesses;

    public MordleGame(string wordToGuess, int maxAttempt)
    {
        this.wordToGuess = wordToGuess;
        this.maxAttempt = maxAttempt;

        _guesses = new Dictionary<string, Guess[]>();
    }
}