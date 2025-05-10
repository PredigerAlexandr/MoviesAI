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
        var response = await client.GetAsync("http://127.0.0.1:8000/api/ai-model/");
        var content = await response.Content.ReadAsStringAsync();
        var movieGuid = new Guid(content.Replace("\"", ""));
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
            Liked = liked,
            UserId = new Guid("62d777c0-3bd7-4603-96ec-305a28d10875")
        };

        var content = new StringContent(JsonConvert.SerializeObject(contentModel), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://127.0.0.1:8000/api/ai-model/", content);

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
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("liked")]
        public bool Liked { get; set; }
        [JsonProperty("user_id")]
        public Guid UserId { get; set; }
    }
}