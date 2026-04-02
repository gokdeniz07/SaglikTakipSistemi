using System;
using System.Windows.Forms;
using SaglikIzSistemi.Arayuz; // Giriş formunun olduğu klasör

namespace SaglikIzSistemi
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // ANAFORM YERİNE GİRİŞ FORMU İLE BAŞLATIYORUZ
            Application.Run(new GirisFormu()); 
        }
    }
}