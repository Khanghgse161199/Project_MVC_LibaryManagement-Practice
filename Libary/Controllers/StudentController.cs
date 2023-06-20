using Libary.ViewModel.Student;
using Microsoft.AspNetCore.Mvc;
using Service.StudentServices;

namespace Libary.Controllers
{
	public class StudentController : Controller
	{
		private readonly IStudentService _studentService;
		public StudentController(IStudentService studentService)
		{
			_studentService = studentService;
		}

		[HttpPost("CreateStudent")]
		public async Task<IActionResult> CreateStudent(CreateStudentViewModel createStudentViewModel)
		{
			if(createStudentViewModel != null)
			{
				var isCreate = await _studentService.CreateStudentAsync(createStudentViewModel.username, createStudentViewModel.password, createStudentViewModel.email);
				if (isCreate)
				{
					return RedirectToAction("Login", "Auth", new { success = "Create student success" });
				}
				else
				{
					return RedirectToAction(nameof(Index), new
					{
						error = "Error in signup"
					});
				}
			}else return RedirectToAction(nameof(Index), new { error = "Error in signup because null-element" });
		}

		public async Task<IActionResult> Index()
		{
			return View();
		}
	}
}
