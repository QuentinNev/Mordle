using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace Mordle.Pages;

public class GameModel : PageModel
{
    public string attempt = "";
    string wordToFind = "Noemie";
    private readonly ILogger<IndexModel> _logger;

    public GameModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public void OnPost(string attempt)
    {
        // SUCCESS
        if (attempt == wordToFind)
        {

        }
        // Compare characters
        else
        {

        }
    }
}
