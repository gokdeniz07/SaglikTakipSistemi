using System;

namespace SaglikIzSistemi.Modeller
{
    // KALITIM: Sensor sınıfından türer.
    public class AtesSensoru : Sensor
    {
        private static Random rnd = new Random();

        // Constructor: Üst sınıfa (base) ismi ve ID'yi gönderir.
        public AtesSensoru(string id) : base(id, "Ateş Sensörü")
        {
        }

        // ÇOK BİÇİMLİLİK: Ölçüm mantığı ateşe özel (35.5 - 41.0 arası).
        public override void OlcumYap()
        {
            // NextDouble 0.0 ile 1.0 arası üretir, biz onu 35.5 - 41.0 arasına çekiyoruz.
            MevcutDeger = 35.5 + (rnd.NextDouble() * 5.5);
            // Not: MevcutDeger, Sensor sınıfında 'protected set' olduğu için buradan değiştirebiliyoruz.
        }

        public override bool KritikMi()
        {
            // Tıbbi kural: 38.5 derece ve üstü riskli kabul edilir.
            return MevcutDeger >= 38.5;
        }
    }
}