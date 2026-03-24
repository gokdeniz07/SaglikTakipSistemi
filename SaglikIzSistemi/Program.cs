using System;
using SaglikIzSistemi.Yardimcilar;

namespace SaglikIzSistemi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Bizim yazdığımız test motorunu çağırıyoruz
            VeriUretici.Calistir();

            Console.WriteLine("\nTest bitti. Kapatmak icin bir tusa bas...");
            Console.ReadKey();
        }
    }
}