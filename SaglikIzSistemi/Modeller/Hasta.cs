using System.Collections.Generic;
using System.Linq; // Silme işlemi için FirstOrDefault kullanacağız

namespace SaglikIzSistemi.Modeller
{
    public class Hasta
    {
        public string TcNo { get; }
        public string AdSoyad { get; set; }

        private List<Sensor> _sensorler;

        public IReadOnlyList<Sensor> Sensorler => _sensorler;

        public Hasta(string tc, string ad)
        {
            TcNo = tc;
            AdSoyad = ad;
            _sensorler = new List<Sensor>();
        }

        public void SensorEkle(Sensor yeniSensor)
        {
            if (yeniSensor != null)
            {
                _sensorler.Add(yeniSensor);
            }
        }

        // SİLME: ID üzerinden listeden sensör çıkartma yeteneği ekledik.
        public void SensorSil(string sensorId)
        {
            // ID'si eşleşen ilk sensörü buluyoruz
            var silinecek = _sensorler.FirstOrDefault(s => s.SensorId == sensorId);
            
            if (silinecek != null)
            {
                _sensorler.Remove(silinecek);
            }
        }
    }
}