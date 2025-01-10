using Microsoft.Extensions.Configuration;

namespace Azure_AI_Demo
{
    internal static class Program
    {
        public static IConfiguration Configuration { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets<frmMain>()
                .Build();
            Application.Run(new frmMain());
        }
    }
}