using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Memez.Models;
using Memez.Data;

namespace Memez.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MemezContext _context;

        public HomeController(ILogger<HomeController> logger, MemezContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int page = 0)
        {
            page *= 9;
            var memezContext = _context.Meme.OrderByDescending(c => c.Timestamp).Skip(page).Take(9);
            return View(memezContext.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}