using CoreStartApp.Middleware;
using CoreStartApp.Models.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace CoreStartApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            string connection = Configuration.GetConnectionString("DefaultConnection") ?? 
                "Server=(localdb)\\mssqllocaldb;Database=BlogDb;Trusted_Connection=True;TrustServerCertificate=True;";
            // Добавляем контекст базы данных
            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(connection));
            // Добавляем сервисы в контейнер DI
            services.AddControllersWithViews();

            // регистрация сервиса репозитория для взаимодействия с базой данных
            services.AddScoped<IBlogRepository, BlogRepository>();
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
            // Поддержка статических файлов
            app.UseStaticFiles();
            // 2. Добавляем компонент, отвечающий за маршрутизацию
            app.UseRouting();
            app.UseAuthorization();
            //Добавляем компонент для логирования запросов с использованием метода Use.
            // Подключаем логирвоание с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();

            // обрабатываем ошибки HTTP
            app.UseStatusCodePages();

 
            // 3. Добавляем компонент с настройкой маршрутов
            // Сначала используем метод Use, чтобы не прерывать ковейер
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        
            // Все прочие страницы имеют отдельные обработчики
            app.Map("/about", app => About(app, env));
            app.Map("/config", app => Config(app, env));
        }
    }

}