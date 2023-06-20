using Libary.ViewModel.Student;
using Microsoft.AspNetCore.Mvc;
using Service.AuthService;

namespace Libary.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("LoginStudent")]
		public async Task<IActionResult> LoginStudent(LoginStudentViewModel loginStudentViewModel)
		{
			if(loginStudentViewModel != null)
			{
				var loginStudent = await _authService.LoginStudent(loginStudentViewModel.email, loginStudentViewModel.password);
				if(loginStudent != null && loginStudent != string.Empty)
				{
					var newSession = HttpContext.Session;
					newSession.SetString("Student", loginStudent);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					return RedirectToAction(nameof(Login), new { error = "Error Login" });
				}
			}
			else
			{
				return RedirectToAction(nameof(Login), new { error = "Please enter email or password" });
			}
		}

		public async Task<IActionResult> Login()
		{
			return View();
		}	
	}
}
