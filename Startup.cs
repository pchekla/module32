namespace CoreStartApp
{
    public class Startup
    {
        /// <summary>
        ///  Обработчик для страницы About
        /// </summary>
        private static void About(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{env.ApplicationName} - ASP.Net Core tutorial project");
            });
        }
    
        private static void Config(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}");
            }); 
        }
        // Метод вызывается средой ASP.NET.
        // Используйте его для подключения сервисов приложения
        // Документация:  https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавляем сервисы в контейнер DI
            services.AddControllers();
        }

        // Метод вызывается средой ASP.NET.
        // Используйте его для настройки конвейера запросов
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Создаем директорию для логов, если она не существует
            string logsPath = Path.Combine(env.ContentRootPath, "Logs");
            if (!Directory.Exists(logsPath))
            {
                Directory.CreateDirectory(logsPath);
            }

            // Настраиваем конвейер обработки HTTP-запросов
            // Проверяем, не запущен ли проект в среде разработки
            if (env.IsDevelopment() || env.IsStaging())
            {
                // 1. Добавляем компонент, отвечающий за диагностику ошибок
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
        
            // 2. Добавляем компонент, отвечающий за маршрутизацию
            app.UseRouting();
        
            //Добавляем компонент для логирования запросов с использованием метода Use.
            app.Use(async (context, next) =>
            {
                // Строка для публикации в лог
                string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}{Environment.NewLine}";
                
                // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
                string logFilePath = Path.Combine(env.ContentRootPath, "Logs", "RequestLog.txt");
                
                // Используем асинхронную запись в файл
                await File.AppendAllTextAsync(logFilePath, logMessage);
                
                await next.Invoke();
            });
 
            // 3. Добавляем компонент с настройкой маршрутов
            // Сначала используем метод Use, чтобы не прерывать ковейер
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
                });
            });
        
            // Все прочие страницы имеют отдельные обработчики
            app.Map("/about", app => About(app, env));
            app.Map("/config", app => Config(app, env));
        
            // Завершим вызовом метода Run
            // Обработчик для ошибки "страница не найдена"
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Page not found");
            });

        }
    }

}