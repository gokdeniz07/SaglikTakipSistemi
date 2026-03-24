using System;

namespace SaglikIzSistemi.Modeller
{
    // 'abstract' anahtar kelimesi SOYUTLAMA (Abstraction) sağlar.
    // Hocam, bu sınıftan nesne türetilemez, sadece diğer sensörlere rehberlik eder.
    public abstract class Sensor
    {
        // KAPSÜLLEME (Encapsulation): 
        // Alanı (field) private tutup, dış dünyaya Property ile açıyoruz.
        private double _mevcutDeger;

        public string SensorId { get; set; }
        public string SensorAdi { get; set; }

        // MevcutDeger'i sadece bu sınıf ve miras alanlar (protected set) değiştirebilir.
        public double MevcutDeger 
        { 
            get => _mevcutDeger; 
            protected set => _mevcutDeger = value; 
        }

        // Yapıcı Metot (Constructor)
        protected Sensor(string id, string ad)
        {
            SensorId = id;
            SensorAdi = ad;
        }

        // ÇOK BİÇİMLİLİK (Polymorphism) hazırlığı:
        // 'abstract' metotların gövdesi olmaz, miras alan sınıflar doldurmak ZORUNDADIR.
        public abstract void OlcumYap();
        public abstract bool KritikMi();
    }
}

// Yani bu kodda sensçrün genel bi tanımı yapıldı. çünkü sensör diye
// bir cihaz alınmaz örneğin nabız sensörü alınır
//private _mevcutDeger: "Veriyi doğrudan dışarıya açmadım, kontrol bende olsun diye kapsülledim."

//abstract void OlcumYap(): "Her sensörün ölçüm tekniği farklıdır (biri sıcaklık, biri basınç ölçer), 
// o yüzden metodun içini boş bıraktım, alt sınıflar kendi mantığına göre dolduracak."