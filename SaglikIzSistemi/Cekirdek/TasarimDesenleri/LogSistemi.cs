using System;
using SaglikIzSistemi.Cekirdek.Arayuzler;
using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Cekirdek.TasarimDesenleri
{
    // OBSERVER PATTERN: Bu sınıf log amaçlı ikinci gözlemcidir.
    public class LogSistemi : IGozlemci
    {
        public void Guncelle(Sensor sensor)
        {
            Console.WriteLine($"[LOG] {sensor.SensorAdi} guncellendi");
        }
    }
}
