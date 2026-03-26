using System;
using System.Windows.Forms;
using SaglikIzSistemi.Arayuz;

namespace SaglikIzSistemi
{
    static class Program
    {
        [STAThread] // Arayüz için bu şarttır
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Konsol yerine bizim AnaForm'u açıyoruz
            Application.Run(new AnaForm());
        }
    }
}