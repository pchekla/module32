using CoreStartApp.Models.Db;

namespace CoreStartApp.Models.Db
{
    public interface ILogRepository
    {
        Task<List<Request>> GetRequests();
        Task AddRequest(Request request);
        Task DeleteRequest(Guid id);
        Task ClearRequests();
    }
} 