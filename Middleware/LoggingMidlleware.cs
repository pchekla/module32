using CoreStartApp.Models.Db;
using Microsoft.Extensions.DependencyInjection;

namespace CoreStartApp.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly IServiceScopeFactory _serviceScopeFactory;
  
        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _env = env;
            _serviceScopeFactory = serviceScopeFactory;
        }
  
        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value ?? "localhost"}{context.Request.Path}{Environment.NewLine}";
            
            // Выводим в консоль
            Console.WriteLine(logMessage);
            
            try
            {
                // Путь до лога
                string logFilePath = Path.Combine(_env.ContentRootPath, "Logs", "RequestLog.txt");
                
                // Используем FileStream для контроля доступа к файлу
                using (var fileStream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    await streamWriter.WriteAsync(logMessage);
                }

                // Создаем scope для работы с базой данных
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var logRepo = scope.ServiceProvider.GetRequiredService<ILogRepository>();
                    
                    // Сохраняем запрос в базу данных
                    var request = new Request
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Url = $"http://{context.Request.Host.Value ?? "localhost"}{context.Request.Path}"
                    };
                    await logRepo.AddRequest(request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи лога: {ex.Message}");
            }
            
            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}