using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Memez.Models;
using Memez.Data;
using Microsoft.EntityFrameworkCore;

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
            int memesPerPage = 9;
            page *= memesPerPage;
            var memezContext = _context.Memes.OrderByDescending(m => m.Timestamp)
                .Skip(page)
                .Take(memesPerPage)
                .Include(m => m.MemezUser);
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