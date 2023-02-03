using System;
using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //creating objects to manage the app
            MainForm form = new MainForm();
            Presenter presenter = new Presenter(form);

            Application.Run(form);
        }
    }
}
