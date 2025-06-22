using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAI.Domain.Entities;
using MoviesAI.Infrastructure;
using MoviesAI.WebApi.Models;

namespace MoviesAI.WebApi.Controllers;

public class AccountController : Controller
{
    private readonly DataBaseContext _context;

    public AccountController(DataBaseContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                await Authenticate(model.Email); // аутентификация

                return RedirectToAction("RateMovie", "Movies");
            }

            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                // добавляем пользователя в бд
                _context.Users.Add(new UserEntity
                    { Name = model.Name, Surname = model.Surname, Email = model.Email, Password = model.Password });
                await _context.SaveChangesAsync();

                await Authenticate(model.Email); // аутентификация

                return RedirectToAction("RateMovie", "Movies");
            }
            else
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }

        return View(model);
    }

    private async Task Authenticate(string userName)
    {
        // создаем один claim
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
        };
        // создаем объект ClaimsIdentity
        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        // установка аутентификационных куки
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}