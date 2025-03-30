namespace CoreStartApp.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
  
        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
  
        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            // Строка для публикации в лог
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
            
            // Выводим в консоль
            Console.WriteLine(logMessage);
            
            // Путь до лога
            string logFilePath = Path.Combine(_env.ContentRootPath, "Logs", "RequestLog.txt");
            
            // Используем асинхронную запись в файл
            await File.AppendAllTextAsync(logFilePath, logMessage);
            
            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}