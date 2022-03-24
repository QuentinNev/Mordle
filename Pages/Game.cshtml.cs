using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    const string GAME_KEY = "currentGame";
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<IndexModel> _logger;
    public MordleGame game;

    public GameModel(IMemoryCache memoryCache, ILogger<IndexModel> logger)
    {
        _memoryCache = memoryCache;
        _logger = logger;
    }

    string wordToFind = "Noemie";
    int maxAttempt = 6;

    public void OnGet()
    {
        game = new MordleGame(wordToFind, maxAttempt);
        _memoryCache.Set(GAME_KEY, game);
    }

    public void OnPost(string attempt)
    {
        if (_memoryCache.TryGetValue(GAME_KEY, out game) && game != null)
        {
            game.MakeAGuess(attempt);
        }
        else
        {
            game = new MordleGame(wordToFind, maxAttempt);
        }

        _memoryCache.Set(GAME_KEY, game);
    }
}
