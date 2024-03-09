using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            //Thread client1Thread = new Thread(() =>
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new Form1());
            //});
            //client1Thread.Start();

            //// Sleep for a moment to ensure the first client has started
            //Thread.Sleep(1000);

            //// Create and run the second client
            //Thread client2Thread = new Thread(() =>
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new Form1());
            //});
            //client2Thread.Start();
            //Thread.Sleep(1000);

            //// Create and run the second client
            //Thread client3Thread = new Thread(() =>
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new Form1());
            //});
            //client3Thread.Start();

            //// Continue as needed for more clients

            //// Keep the main thread alive
            //Application.Run();
        }
    }
}
