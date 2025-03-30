using Microsoft.EntityFrameworkCore;

namespace CoreStartApp.Models.Db
{
    public class BlogRepository : IBlogRepository
    {
        // ссылка на контекст
        private readonly BlogContext _context;
  
        // Метод-конструктор для инициализации
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }
  
        public async Task AddUser(User user)
        {
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);
      
            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Feedback>> GetFeedbacks(int page, int pageSize)
        {
            return await _context.Feedbacks
                .OrderByDescending(f => f.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetFeedbacksCount()
        {
            return await _context.Feedbacks.CountAsync();
        }

        public async Task AddFeedback(Feedback feedback)
        {
            var entry = _context.Entry(feedback);
            if (entry.State == EntityState.Detached)
                await _context.Feedbacks.AddAsync(feedback);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedback(Guid id)
        {
            var feedback = _context.Feedbacks.FirstOrDefault(f => f.Id == id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
} 