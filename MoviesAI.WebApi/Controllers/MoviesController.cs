using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAI.Domain.Entities;
using MoviesAI.Infrastructure;
using MoviesAI.WebApi.Models;
using Newtonsoft.Json;

namespace MoviesAI.WebApi.Controllers;

public class MoviesController : Controller
{
    private readonly ILogger<MoviesController> _logger;
    private readonly DataBaseContext _context;

    public MoviesController(ILogger<MoviesController> logger, DataBaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize]
    public async Task<IActionResult> RateMovie()
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
        var userEmail = User.Identity?.Name;

        if (userEmail == null)
        {
            throw new Exception("Невозможно получить Email пользователя");
        }

        HttpClient client = new HttpClient();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail)
                   ?? throw new Exception("Не было найдено ожидаемого пользователя.");

        var contentModel = new ModelRequest
        {
            Id = movieId,
            Liked = liked,
            UserId = user.Id
        };

        var content = new StringContent(JsonConvert.SerializeObject(contentModel), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://127.0.0.1:8000/api/ai-model/", content);

        response.EnsureSuccessStatusCode();

        if (liked)
        {
            user.FavoriteMovieIds.Add(movieId);
            _context.Entry(user).Property(x => x.FavoriteMovieIds).IsModified = true;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("RateMovie");
    }

    [Authorize]
    public async Task<IActionResult> Favorites(int page = 1)
    {
        var userEmail = User.Identity?.Name;

        if (userEmail == null)
        {
            throw new Exception("Невозможно получить Email пользователя");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userEmail) ??
                   throw new Exception("Не был найден ожилайемый пользователь.");

        var moviesQuery = _context.Movies.AsQueryable().Where(x => user.FavoriteMovieIds.Contains(x.Id));
        var movies = await moviesQuery.Skip((page - 1) * 10)
            .Take(10).ToArrayAsync();

        var moviesDto = new FavoriteMoviesDto
        {
            Movies = movies, PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = 10,
                TotalItems = moviesQuery.Count()
            }
        };

        return View(moviesDto);
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
        [JsonProperty("id")] public Guid Id { get; set; }
        [JsonProperty("liked")] public bool Liked { get; set; }
        [JsonProperty("user_id")] public Guid UserId { get; set; }
    }
}