using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CoreStartApp.Models;
using CoreStartApp.Models.Db;

namespace CoreStartApp.Controllers
{
    public class HomeController : Controller
    {
        // ссылка на репозиторий
        private readonly IBlogRepository _repo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

       public IActionResult Index()
       {
           return View();
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

        public async Task <IActionResult> Authors()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }
    }
} 