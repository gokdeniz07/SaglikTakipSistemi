using System;

namespace SaglikIzSistemi.Modeller
{
    // KALITIM (Inheritance): NabizSensoru, Sensor sınıfından türer.
    // "Nabız sensörü bir sensördür" ilişkisi kuruldu.
    public class NabizSensoru : Sensor
    {
        private static Random rnd = new Random();

        // 'base' anahtar kelimesi ile üst sınıfın (Sensor) yapıcı metodunu çağırıyoruz.
        public NabizSensoru(string id) : base(id, "Nabız Sensörü")
        {
        }

        // ÇOK BİÇİMLİLİK (Polymorphism): Üst sınıftaki soyut metodu 'override' ile dolduruyoruz.
        public override void OlcumYap()
        {
            // Simülasyon: IoT cihazından veri geliyormuş gibi 60-120 arası değer üret.
            MevcutDeger = rnd.Next(60, 121); 
        }

        public override bool KritikMi()
        {
            // Tıbbi bir kural: Nabız 100'ün üzerindeyse sistem alarm verecek.
            return MevcutDeger > 100;
        }
    }
}

// class NabizSensoru : Sensor: "Hocam, burada Inheritance kullandım. SensorId ve SensorAdi gibi özellikleri tekrar yazmadım, mirasa kondum."

// override void OlcumYap(): "İşte bu Polymorphism. Sensor sınıfında bu metodun içi boştu (soyuttu)
// burada nabza özel (rastgele sayı üretimi) içini doldurdum."

// base(id, "Nabız Sensörü"): "Üst sınıfın constructor'ına parametre göndererek nesneyi başlattım."