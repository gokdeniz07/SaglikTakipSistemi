using System;

namespace SaglikIzSistemi.Modeller
{
    public class OksijenSensoru : Sensor
    {
        private static Random rnd = new Random();

        public OksijenSensoru(string id) : base(id, "Oksijen Sensörü (SpO2)")
        {
        }

        public override void OlcumYap()
        {
            // Sağlıklı değer %90-%100 arasıdır.
            MevcutDeger = rnd.Next(85, 101); 
        }

        public override bool KritikMi()
        {
            // Oksijen seviyesi %90'ın altına düşerse kritiktir.
            return MevcutDeger < 90;
        }
    }
}