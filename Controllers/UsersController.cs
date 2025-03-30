using Microsoft.AspNetCore.Mvc;
using CoreStartApp.Models.Db;

namespace CoreStartApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBlogRepository _repo;
      
        public UsersController(IBlogRepository repo)
        {
            _repo = repo;
        }
      
        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();
            return View(authors);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            newUser.JoinDate = DateTime.Now;
            newUser.Id = Guid.NewGuid();
            await _repo.AddUser(newUser);
            return View(newUser);
        }

        // Временный метод для очистки БД
        public async Task<IActionResult> ClearUsers()
        {
            var users = await _repo.GetUsers();
            foreach (var user in users)
            {
                await _repo.DeleteUser(user.Id);
            }
            return RedirectToAction("Index");
        }
    }
}