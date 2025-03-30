using Microsoft.EntityFrameworkCore;

namespace CoreStartApp.Models.Db
{
    public class LogRepository : ILogRepository
    {
        private readonly DbContextOptions<BlogContext> _contextOptions;

        public LogRepository(DbContextOptions<BlogContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public async Task<List<Request>> GetRequests()
        {
            using (var context = new BlogContext(_contextOptions))
            {
                return await context.Requests
                    .OrderByDescending(r => r.Date)
                    .ToListAsync();
            }
        }

        public async Task AddRequest(Request request)
        {
            using (var context = new BlogContext(_contextOptions))
            {
                var entry = context.Entry(request);
                if (entry.State == EntityState.Detached)
                    await context.Requests.AddAsync(request);

                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteRequest(Guid id)
        {
            using (var context = new BlogContext(_contextOptions))
            {
                var request = await context.Requests.FindAsync(id);
                if (request != null)
                {
                    context.Requests.Remove(request);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task ClearRequests()
        {
            using (var context = new BlogContext(_contextOptions))
            {
                context.Requests.RemoveRange(await context.Requests.ToListAsync());
                await context.SaveChangesAsync();
            }
        }
    }
} 