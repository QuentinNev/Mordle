using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Mordle.Data;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    /// <summary>
    /// KEY to store current game
    /// </summary>
    const string GAME_KEY = "currentGame";
    /// <summary>
    /// Used to store a game to keep the data persistent
    /// </summary>
    private readonly IMemoryCache _memoryCache;
    /// <summary>
    /// Used to retrieve list of word to get a random one
    /// </summary>
    private readonly DevDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    
    /// <summary>
    /// Game model used to construct the view
    /// </summary>
    public MordleGame game = new MordleGame("default", 6);

    /// <summary>
    /// Last word entered by player.
    /// DataAnnotations serves as validation client and server side
    /// </summary>
    [Required(ErrorMessage = " "),
        MinLength(8, ErrorMessage = "Word is too short!"),
        MaxLength(8, ErrorMessage = "Word is too long!"),
        RegularExpression("^[a-zA-Z]+$", 
            ErrorMessage = "Guess can contain only letters without accent")]
    public string guess { get; set; }

    /// <summary>
    /// Constructor of razor page
    /// </summary>
    public GameModel(IMemoryCache memoryCache, ILogger<IndexModel> logger, DevDbContext context)
    {
        _memoryCache = memoryCache;
        _logger = logger;
        _context = context;
        guess = "";
    }

    int maxGuesses = 6;

    /// <summary>
    /// Called when a GET request is made on route /Game
    /// Check if we wanted to restart game, else create a new on if there's none running
    /// </summary>
    public void OnGet()
    {
        if (HttpContext.Request.Query["newGame"].ToString() == "true")
        {
            game = CreateGame(maxGuesses);
            _memoryCache.Set(GAME_KEY, game);
        }
        else
        {
            if (!_memoryCache.TryGetValue(GAME_KEY, out game))
            {
                game = CreateGame(maxGuesses);
                _memoryCache.Set(GAME_KEY, game);
            }
        }
    }

    /// <summary>
    /// Called when a POST request is made on route /Game
    /// </summary>
    /// <param name="guess">Word player entered which has been validated client side</param>
    public void OnPost(string guess = "")
    {
        Regex regex = new Regex("^[A-Z]+$");

        if (guess != null && _memoryCache.TryGetValue(GAME_KEY, out game) && game != null)
        {
            guess = guess.ToUpperInvariant();
            if (regex.IsMatch(guess))
            {
                game.MakeAGuess(guess);

                // Clear form fields, else it'll show last guess
                ModelState.Clear();
            }
        }
        else
        {
            game = CreateGame(maxGuesses);
        }

        _memoryCache.Set(GAME_KEY, game);
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    /// <param name="maxGuesses">Maximum words player can try before losing the game</param>
    /// <returns></returns>
    private MordleGame CreateGame(int maxGuesses)
    {
        Random rand = new Random();

        var list = _context.Words.ToList();                         // Get word list
        string wordToGuess = list[rand.Next(0, list.Count)].word;   // Pick a random one

        return new MordleGame(wordToGuess, 6);
    }
}
