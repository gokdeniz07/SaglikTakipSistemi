using System.Collections.Generic;
using SaglikIzSistemi.Cekirdek.Arayuzler;
using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Cekirdek.TasarimDesenleri
{
    // OBSERVER PATTERN: Bu sınıf 'Konu' (Subject) rolündedir.
    public class SensorYoneticisi : IKonu
    {
        // Takipçi listesi (Gözlemciler)
        private readonly List<IGozlemci> _gozlemciler = new List<IGozlemci>();
        
        // Üzerinde işlem yapılan güncel sensör
        private Sensor _aktifSensor;

        public void GozlemciEkle(IGozlemci gozlemci)
        {
            if (!_gozlemciler.Contains(gozlemci))
                _gozlemciler.Add(gozlemci);
        }

        public void GozlemciCikar(IGozlemci gozlemci)
        {
            if (_gozlemciler.Contains(gozlemci))
                _gozlemciler.Remove(gozlemci);
        }

        // Veri değiştiğinde hepsine haber ver!
        public void HaberVer()
        {
            foreach (var gozlemci in _gozlemciler)
            {
                gozlemci.Guncelle(_aktifSensor);
            }
        }

        // Sensörden yeni veri okunduğunda tetiklenecek metot
        public void VeriGuncelle(Sensor sensor)
        {
            _aktifSensor = sensor;
            _aktifSensor.OlcumYap(); // Polimorfizm: Hangi sensörse onun ölçümü çalışır.
            HaberVer(); // Herkesi uyar!
        }
    }
}