using System.Collections.Generic;

namespace SaglikIzSistemi.Modeller
{
    public class Hasta
    {
        // TC No sadece constructor'da set edilebilir, dışarıdan değiştirilemez.
        public string TcNo { get; }
        public string AdSoyad { get; set; }

        // KAPSÜLLEME: Listeyi private tutarak dış müdahaleye kapattık.
        private List<Sensor> _sensorler;

        // Dışarıya sadece 'Okunabilir' bir liste sunuyoruz.
        public IReadOnlyList<Sensor> Sensorler => _sensorler;

        public Hasta(string tc, string ad)
        {
            TcNo = tc;
            AdSoyad = ad;
            _sensorler = new List<Sensor>();
        }

        // Listeye veri ekleme işlemini bir metot üzerinden kontrol ediyoruz.
        public void SensorEkle(Sensor yeniSensor)
        {
            if (yeniSensor != null)
            {
                _sensorler.Add(yeniSensor);
            }
        }
    }
}