using System.Text.Json;
using MoviesAI.Domain;
using MoviesAI.Domain.Entities;
using MoviesAI.Infrastructure.Models.Dto;
using Quartz;
using Shared;

namespace MoviesAI.Infrastructure.Jobs;

public class GetMovieJob : IJob
{
    private readonly HttpClient _httpClient;

    private readonly DataBaseContext _context;

    public GetMovieJob(DataBaseContext context,
        IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        string apiKey = "ваш_api_key"; // Замените на ваш API Key
        var page = 1;
        var isEnd = false;
        // Установка заголовка авторизации
        _httpClient.DefaultRequestHeaders.Add("X-API-KEY", "GJC155V-N81MCQS-KFGNN9Y-PVVXCVK");

        // string urlTEst =
        //     $"https://api.kinopoisk.dev/v1.4/movie?page={page}&limit=200&selectFields=createdAt&selectFields=id&notNullFields=createdAt&selectFields=name&selectFields=genres&selectFields=countries&selectFields=year&selectFields=persons&sortField=createdAt&sortType=1";
        // // Отправка GET-запроса
        // HttpResponseMessage responseTest = await _httpClient.GetAsync(urlTEst);
        // // Проверка на успешный статус ответа
        //
        // // Чтение ответа как строки
        // string responseBodyTest = await responseTest.Content.ReadAsStringAsync();
        //
        // var resultTest = JsonSerializer.Deserialize<KinoPoiskResponse>(responseBodyTest);
        //
        // var maxPages = resultTest.Pages;
        //
        // var job = new JobResultEntity
        // {
        //     Id = Guid.NewGuid(),
        //     Started = DateTime.UtcNow,
        //     Status = StatusJob.Process
        // };
        // _context.JobResults.Add(job);
        // await _context.SaveChangesAsync();


        try
        {
            do
            {
                string url =
                    $"https://api.kinopoisk.dev/v1.4/movie?page={page}&limit=200&lists=top500&selectFields=createdAt&selectFields=id&selectFields=name&selectFields=genres&selectFields=countries&selectFields=year&selectFields=persons&sortField=createdAt&sortType=1&selectFields=poster";
                // Отправка GET-запроса
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                // Проверка на успешный статус ответа

                // Чтение ответа как строки
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody); // Вывод ответа на консоль

                var result = JsonSerializer.Deserialize<KinoPoiskResponse>(responseBody);


                // Получение объектов из списка docs
                List<Doc> docs = result.Docs;

                var newMovies = docs.Where(x => x is { Year: not null, Name: not null, Poster: not null });

                foreach (var newMovie in newMovies)
                {
                    var movieEntity = new MovieEntity()
                    {
                        Id = new Guid(),
                        ExternalId = newMovie.Id,
                        Title = newMovie.Name,
                        CreatedCountries = newMovie.Countries.Select(x => x.Name).ToList(),
                        Genres = newMovie.Genres.Select(x => x.Name).ToList(),
                        Actors = newMovie.Persons?.Select(x => x.Name).Take(3).ToList() ?? new List<string>(),
                        CreatedYear = newMovie.Year.Value,
                        ImageUrl = newMovie.Poster.Url
                    };
                    _context.Movies.Add(movieEntity);
                }

                _context.SaveChanges();
                page++;
                if (page > result.Pages)
                {
                    isEnd = true;
                }
            } while (!isEnd);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Ошибка при отправке запроса: {e.Message}");
        }

        Console.WriteLine("Hello");
    }
}