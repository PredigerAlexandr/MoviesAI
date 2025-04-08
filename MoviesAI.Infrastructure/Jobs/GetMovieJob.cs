using System.Text.Json;
using MoviesAI.Infrastructure.Models.Dto;
using Quartz;

namespace MoviesAI.Infrastructure.Jobs;

public class GetMovieJob : IJob
{
    private static readonly HttpClient client = new HttpClient();

    public async Task Execute(IJobExecutionContext context)
    {
        string apiKey = "ваш_api_key"; // Замените на ваш API Key
        string url = "https://api.kinopoisk.dev/v1.4/movie?page=1&limit=1&selectFields=createdAt&selectFields=id&notNullFields=createdAt&sortField=createdAt&sortType=1"; // Замените на URL вашего API

        // Установка заголовка авторизации
        client.DefaultRequestHeaders.Add("X-API-KEY", "GJC155V-N81MCQS-KFGNN9Y-PVVXCVK");

        try
        {
            // Отправка GET-запроса
            HttpResponseMessage response = await client.GetAsync(url);
            // Проверка на успешный статус ответа

            // Чтение ответа как строки
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody); // Вывод ответа на консоль
            
            var result = JsonSerializer.Deserialize<KinoPoiskResponse>(responseBody);

            // Получение объектов из списка docs
            List<Doc> docs = result.Docs;

            // Получение параметров page и pages
            int page = result.Page;
            int pages = result.Pages;

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Ошибка при отправке запроса: {e.Message}");
        }

        Console.WriteLine("Hello");
    }
}