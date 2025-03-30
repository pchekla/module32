using Microsoft.AspNetCore.Mvc;
using CoreStartApp.Models.Db;

namespace CoreStartApp.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogRepository _repo;

        public LogController(ILogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetRequests();
            return View(requests);
        }

        public async Task<IActionResult> ClearLogs()
        {
            await _repo.ClearRequests();
            return RedirectToAction("Index");
        }
    }
} 