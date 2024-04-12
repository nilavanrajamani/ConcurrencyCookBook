using ConcurrencyCookbookWinforms._6._2;
using ConcurrencyCookbookWinforms._6._3;
using ConcurrencyCookbookWinforms._6._4;
using ConcurrencyCookbookWinforms._6._5;

namespace ConcurrencyCookbookWinforms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new TimeoutWithFallbackSubscribable());
        }
    }
}