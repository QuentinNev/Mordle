using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using Mordle.Data;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    const string GAME_KEY = "currentGame";
    private readonly IMemoryCache _memoryCache;
    private readonly DevDbContext _context;
    private readonly ILogger<IndexModel> _logger;
    public MordleGame game = new MordleGame("default", 6);
    public string error;

    [Required(ErrorMessage = " "),
        MinLength(8, ErrorMessage = "Word is too short!"),
        MaxLength(8, ErrorMessage = "Word is too long!"),
        RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Guess can contain only letters without accent")]
    public string attempt { get; set; }

    public GameModel(IMemoryCache memoryCache, ILogger<IndexModel> logger, DevDbContext context)
    {
        _memoryCache = memoryCache;
        _logger = logger;
        _context = context;
        error = "";
        attempt = "";
    }

    int maxAttempt = 6;

    public void OnGet()
    {
        if (HttpContext.Request.Query["newGame"].ToString() == "true")
        {
            game = CreateGame(maxAttempt);
            _memoryCache.Set(GAME_KEY, game);
        }
        else
        {
            if (!_memoryCache.TryGetValue(GAME_KEY, out game))
            {
                game = CreateGame(maxAttempt);
                _memoryCache.Set(GAME_KEY, game);
            }
        }
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
                ModelState.Clear();
            }
            else
                error = "Guess is invalid : " + this.attempt;
        }
        else
        {
            game = CreateGame(maxAttempt);
        }

        _memoryCache.Set(GAME_KEY, game);
    }

    private MordleGame CreateGame(int maxAttempt)
    {
        Random rand = new Random();

        var list = _context.Words.ToList();
        string wordToGuess = list[rand.Next(0, list.Count)].word;

        return new MordleGame(wordToGuess, 6);
    }
}
