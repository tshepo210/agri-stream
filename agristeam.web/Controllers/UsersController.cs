using agristeam.web.Models.Entities;
using agristeam.web.Services;
using Microsoft.AspNetCore.Mvc;

namespace agristeam.web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        /*public UsersController(IUserService userService)
        {
            _userService = userService;
        }*/

        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(user.RowKey))
                    user.RowKey = System.Guid.NewGuid().ToString();

                await _userService.CreateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
