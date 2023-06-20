using AdminLibary.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Service.UserServices;

namespace AdminLibary.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> disable(string id)
        {
            var isDisable = await _userService.DisableEnableAsync(id);
            if (isDisable)
            {
                return RedirectToAction("Index", new { success = "Disable user success" });
            }
            else
            {
                return RedirectToAction("Index", new { error = "Disable user error" });
            }
        }

        public async Task<IActionResult> enable(string id)
        {
            var isDisable = await _userService.DisableEnableAsync(id);
            if (isDisable)
            {
                return RedirectToAction("Index", new { success = "Enable user success" });
            }
            else
            {
                return RedirectToAction("Index", new { error = "Enable user error" });
            }
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                bool created = await _userService.CreateUserAsync(createUserViewModel.UserName, createUserViewModel.Password, createUserViewModel.Fullname);
                if (created)
                {
                    return RedirectToAction("Create", new { success = "create user success" });
                }
                else
                {
                    return RedirectToAction("Create", new { error = "username or password is duplicate" });
                }
            }
            else
            {
                return RedirectToAction("Create", new { error = "please loign first" });
            }
        }

        public ActionResult Create(string error, string success)
        {
            if(error != null)
            {
                ViewBag.Error = error;
            }
            if(success != null)
            {
                ViewBag.Success = success;
            }
            return View();
        }

        public async Task<IActionResult> Index(string error, string success)
        {
            var users = await _userService.GetAll();
            ListUserViewModel listUserViewModel = new ListUserViewModel();
            
            foreach (var user in users) {
                listUserViewModel.Users.Add(new UserViewModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    Fullname = user.Fullname,
                    IsActive = user.IsActive,
                });
            }
            if (error != null)
            {
                ViewBag.Error = error;
            }
            if (success != null)
            {
                ViewBag.Success = success;
            }
            return View(listUserViewModel);
        }
    }
}
