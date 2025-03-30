using CoreStartApp.Models.Db;

namespace CoreStartApp.Models.Db
{
    public interface IBlogRepository
    {
        Task<List<User>> GetUsers();
        Task AddUser(User user);
        Task DeleteUser(Guid id);
        
        // Методы для работы с отзывами
        Task<List<Feedback>> GetFeedbacks(int page, int pageSize);
        Task<int> GetFeedbacksCount();
        Task AddFeedback(Feedback feedback);
        Task DeleteFeedback(Guid id);
    }
} 