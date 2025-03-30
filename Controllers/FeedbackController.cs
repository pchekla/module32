using Microsoft.AspNetCore.Mvc;
using CoreStartApp.Models;
using System.Diagnostics;
using CoreStartApp.Models.Db;

namespace CoreStartApp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IBlogRepository _repo;
        private const int PageSize = 10;

        public FeedbackController(IBlogRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _repo.GetFeedbacks(1, PageSize);
            return View(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] Models.Db.Feedback feedback)
        {
            if (string.IsNullOrWhiteSpace(feedback.From) || string.IsNullOrWhiteSpace(feedback.Text))
            {
                return StatusCode(400, "Пожалуйста, заполните все поля формы.");
            }

            feedback.Id = Guid.NewGuid();
            feedback.CreatedAt = DateTime.Now;

            await _repo.AddFeedback(feedback);
            
            // Получаем обновленный список отзывов
            var feedbacks = await _repo.GetFeedbacks(1, PageSize);
            return Json(new { 
                message = $"{feedback.From}, спасибо за ваш отзыв!",
                feedbacks = feedbacks
            });
        }

        // Метод для очистки отзывов
        public async Task<IActionResult> ClearFeedbacks()
        {
            var feedbacks = await _repo.GetFeedbacks(1, int.MaxValue);
            foreach (var feedback in feedbacks)
            {
                await _repo.DeleteFeedback(feedback.Id);
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}