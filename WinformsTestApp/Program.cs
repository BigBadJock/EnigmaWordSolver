using EnigmaWordSolver;
using EnigmaWordSolver.Contracts;
using EnigmaWordSolver.miscellaneous;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WinformsTestApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services
                .AddSingleton<IConfiguration>(configuration)
                .AddScoped<IWordListLoader, WordListLoader>()
                .AddScoped<IEnigmaWord, EnigmaWord>()
                .AddScoped<Form1>();
        }
    }
}