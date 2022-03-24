using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Mordle.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<TrucModel> _logger;

    public IndexModel(ILogger<TrucModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
