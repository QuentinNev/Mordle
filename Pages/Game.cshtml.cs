using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public GameModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
