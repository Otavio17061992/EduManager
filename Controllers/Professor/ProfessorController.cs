using Microsoft.AspNetCore.Mvc;

namespace EduManager.Controllers.Professor
{
    public class ProfessorController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}