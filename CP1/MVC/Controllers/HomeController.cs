// ChatGPT (GPT-5 Thinking)
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Linq;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(new TheModel());

        [HttpPost]
        public IActionResult Index(TheModel model)
        {
            if (!ModelState.IsValid)
            {
                // Validaciones fallaron: mostrar errores
                return View(model);
            }

            // Improvement: usar LINQ a nivel de Character para excluir solo espacios
            // (sin usar Replace en string).
            var charsNoSpaces = (model.Phrase ?? string.Empty)
                .Where(c => c != ' ')
                .ToList();

            // Conteos por carácter, ordenados desc por cuenta y luego por carácter
            var counts = charsNoSpaces
                .GroupBy(c => c)
                .Select(g => (ch: g.Key, count: g.Count()))
                .OrderByDescending(t => t.count)
                .ThenBy(t => t.ch)
                .ToList();

            // Mayúsculas y minúsculas sin espacios (a nivel de char)
            var lower = new string(charsNoSpaces.Select(char.ToLowerInvariant).ToArray());
            var upper = new string(charsNoSpaces.Select(char.ToUpperInvariant).ToArray());

            model.Counts = counts;
            model.LowerNoSpaces = lower;
            model.UpperNoSpaces = upper;
            model.Submitted = true;

            return View(model);
        }
    }
}
