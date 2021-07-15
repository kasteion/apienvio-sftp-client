using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;


namespace apienvio_sync
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static int Main(string[] args)
        {
            try
            {
                MainAsync(args);
                return 0;
            } catch
            {
                return 1;
            }
        }
        static void MainAsync(string[] args)
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                serviceProvider.GetService<App>().Run();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Building Configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            // Add app
            serviceCollection.AddTransient<App>();
        }
    }
}
