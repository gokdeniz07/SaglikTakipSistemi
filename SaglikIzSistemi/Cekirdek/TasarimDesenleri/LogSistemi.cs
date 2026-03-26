using System;
using System.IO; // Dosya işlemleri için bu kütüphane şart!
using SaglikIzSistemi.Cekirdek.Arayuzler;
using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Cekirdek.TasarimDesenleri
{
    // OBSERVER PATTERN: Bu sınıf 'Gözlemci' rolündedir ve verileri dosyaya arşivler.
    public class LogSistemi : IGozlemci
    {
        private string _dosyaYolu = "saglik_kayitlari.txt";

        public void Guncelle(Sensor sensor)
        {
            // BASİT MANTIK: Bir satır yazı hazırla ve dosyaya ekle.
            string satir = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {sensor.SensorAdi} ({sensor.SensorId}) | Değer: {sensor.MevcutDeger:F1}";

            try 
            {
                // File.AppendAllLines: Dosya varsa sonuna ekler, yoksa yeni oluşturur.
                // Bu yöntem "Aç -> Yaz -> Kapat" işlemini otomatik yapar.
                File.AppendAllText(_dosyaYolu, satir + Environment.NewLine);
                
                // Konsola da bilgi verelim (Terminalde görmek için)
                Console.WriteLine($"[ARŞİVLENDİ] {sensor.SensorAdi} verisi dosyaya kaydedildi.");
            }
            catch (Exception ex)
            {
                // HATA YÖNETİMİ: Dosya o an başka bir program tarafından kullanılıyorsa 
                // programın çökmesini engeller, bize haber verir.
                Console.WriteLine("Dosya yazma hatası: " + ex.Message);
            }
        }
    }
}