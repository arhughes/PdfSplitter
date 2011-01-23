using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PdfSplitter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (arguments.Length == 1)
                Application.Run(new SplitterWindow(arguments[0]));
            else
                Application.Run(new SplitterWindow());
        }
    }
}
