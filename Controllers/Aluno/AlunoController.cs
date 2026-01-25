using Microsoft.AspNetCore.Mvc;

namespace EduManager.Controllers.Aluno
{
    public class AlunoController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}