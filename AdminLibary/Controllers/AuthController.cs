using AdminLibary.ViewModel.LoginViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.AuthService;

namespace AdminLibary.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var loginUser = await _authService.LoginUser(loginViewModel.Username, loginViewModel.Password);
                if (!string.IsNullOrEmpty(loginUser))
                {
                    var session = HttpContext.Session;
                    session.SetString("USER", loginUser);
                    return RedirectToAction(nameof(Index),"Home");
                    
                }else {
                    return RedirectToAction(nameof(Index), new { error = "Please enter username or password" });
                }
            }
            else
            {
                return RedirectToAction(nameof(Index), new { error = "Please enter username or password" });
            }
        }
        public IActionResult Index(string error)
        {
            if(error != null)
            {
                ViewBag.Error = error;
            }
            return View();
        }
    }
}
