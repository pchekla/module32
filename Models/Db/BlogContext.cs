using Microsoft.EntityFrameworkCore;

namespace CoreStartApp.Models.Db
{
    /// <summary>
    /// Класс контекста, предоставляющий доступ к сущностям базы данных
    /// </summary>
    public class BlogContext : DbContext
    {
        /// <summary>
        /// Ссылка на таблицу Users
        /// </summary>
        public DbSet<User> Users { get; set; }
  
        /// <summary>
        /// Ссылка на таблицу UserPosts
        /// </summary>
        public DbSet<UserPost> Posts { get; set; }
 
        /// <summary>
        /// Ссылка на таблицу Feedbacks
        /// </summary>
        public DbSet<Feedback> Feedbacks { get; set; }

        /// <summary>
        /// Логика взаимодействия с таблицами в БД
        /// </summary>
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
                   Database.EnsureCreated();
        }
    }
} 