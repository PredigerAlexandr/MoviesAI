using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MoviesAI.Infrastructure;
using MoviesAI.WebApi.Models;
using Newtonsoft.Json;

namespace MoviesAI.WebApi.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataBaseContext _context;

    public HomeController(ILogger<HomeController> logger, DataBaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        HttpClient client = new HttpClient();
        var response = await client.GetAsync("");
        var content = await response.Content.ReadAsStringAsync();
        var movieGuid = new Guid(content);
        var movie = _context.Movies.FirstOrDefault(x => x.Id == movieGuid);

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessResponse(Guid movieId, bool liked)
    {
        HttpClient client = new HttpClient();

        var contentModel = new ModelRequest
        {
            Id = movieId,
            Liked = liked
        };

        var content = new StringContent(JsonConvert.SerializeObject(contentModel), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("", content);

        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private class ModelRequest
    {
        public Guid Id { get; set; }
        public bool Liked { get; set; }
    }
}