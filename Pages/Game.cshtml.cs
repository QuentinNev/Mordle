using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    const string GAME_KEY = "currentGame";
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<IndexModel> _logger;
    public MordleGame game = new MordleGame("default", 6);
    public string error;

    [Required(ErrorMessage = " "),
        RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Guess can contain only letters without accent")]
    public string attempt { get; set; }

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

    public void OnPost(string attempt = "")
    {

        Regex regex = new Regex("^[A-Z]+$");

        if (attempt != null && _memoryCache.TryGetValue(GAME_KEY, out game) && game != null)
        {
            attempt = attempt.ToUpperInvariant();
            if (regex.IsMatch(attempt))
            {
                game.MakeAGuess(attempt);
            }
            else
                error = "Guess is invalid : " + this.attempt;
        }
        else
        {
            game = new MordleGame(wordToFind, maxAttempt);
        }

        _memoryCache.Set(GAME_KEY, game);
    }
}
