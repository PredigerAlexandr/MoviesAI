using MoviesAI.Infrastructure;
using MoviesAI.Infrastructure.Jobs;
using Quartz;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataBaseContext>();
builder.Services.AddHttpClient();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Определение задачи
    var jobKey = new JobKey("myJob");
    q.AddJob<GetMovieJob>(opts => opts.WithIdentity(jobKey));

    // Настройка триггера
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("myJob-trigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .RepeatForever()            // Повторяем бесконечно
            .WithIntervalInHours(10))); // Запуск каждые 5 секунд
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();