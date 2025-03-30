namespace CoreStartApp
{
    public class Program
    {
        /// <summary>
        ///  Точка входа - метод Main
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
 
        /// <summary>
        /// Статический метод, создающий и настраивающий IHostBuilder -
        /// объект, который в свою очередь создает хост для развертывания нашего приложения
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }

}