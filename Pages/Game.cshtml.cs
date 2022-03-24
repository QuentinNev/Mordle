using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    const string GAME_KEY = "currentGame";
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<IndexModel> _logger;
    public MordleGame game;
    public string error;

    public GameModel(IMemoryCache memoryCache, ILogger<IndexModel> logger)
    {
        _memoryCache = memoryCache;
        _logger = logger;
        error = "";
    }

    string wordToFind = "NOEMIE";
    int maxAttempt = 6;

    public void OnGet()
    {
        game = new MordleGame(wordToFind, maxAttempt);
        _memoryCache.Set(GAME_KEY, game);
    }

    public void OnPost(string attempt)
    {
        attempt = attempt.ToUpperInvariant();
        Regex regex = new Regex("^[A-Z]+$");

        if (_memoryCache.TryGetValue(GAME_KEY, out game) && game != null)
        {
            if (regex.IsMatch(attempt))
            {
                game.MakeAGuess(attempt);
            }
            else
                error = "Guess is invalid";
        }
        else
        {
            game = new MordleGame(wordToFind, maxAttempt);
        }

        _memoryCache.Set(GAME_KEY, game);
    }
}
