using agristeam.web.Models.Entities;
using agristeam.web.Models;
using agristeam.web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace agristeam.web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new UserCreateModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    Email = model.Email.Trim(),
                    Phone = model.Phone.Trim(),
                    Username = model.Username.Trim()
                };

                var passwordHasher = new PasswordHasher<User>();
                user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

                await _userService.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}
