using System;
using SaglikIzSistemi.Cekirdek.Arayuzler;
using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Cekirdek.TasarimDesenleri
{
    // OBSERVER PATTERN: Bu sınıf 'Gözlemci' (Observer) rolündedir.
    public class AlarmSistemi : IGozlemci
    {
        public void Guncelle(Sensor sensor)
        {
            // Polimorfizm sayesinde her sensörün kendi 'KritikMi' mantığı çalışır.
            if (sensor.KritikMi())
            {
                // Burası ileride gerçek bir IoT projesinde siren çalabilir 
                // veya bildirim gönderebilir.
                Console.WriteLine($"!!! KRİTİK DURUM UYARISI !!!");
                Console.WriteLine($"{sensor.SensorAdi}: {sensor.MevcutDeger} değeri eşiği aştı!");
            }
        }
    }
}