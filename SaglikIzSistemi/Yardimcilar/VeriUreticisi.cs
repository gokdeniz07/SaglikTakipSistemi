using System;
using SaglikIzSistemi.Cekirdek.TasarimDesenleri;
using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Yardimcilar
{
    public class VeriUretici
    {
        public static void Calistir()
        {
            // 1. Yöneticiyi (Subject) kur
            SensorYoneticisi yonetici = new SensorYoneticisi();

            // 2. Gözlemciyi (Alarm Sistemi) ekle
            AlarmSistemi alarm = new AlarmSistemi();
            yonetici.GozlemciEkle(alarm);
            yonetici.GozlemciEkle(new LogSistemi());

            // 3. Sensörleri oluştur
            NabizSensoru nabiz = new NabizSensoru("N1");
            AtesSensoru ates = new AtesSensoru("A1");
            OksijenSensoru oksijen = new OksijenSensoru("O1");

            // 4. Hastayı oluştur ve sensörleri hastaya bağla
            Hasta hasta = new Hasta("12345678901", "Ornek Hasta");
            hasta.SensorEkle(nabiz);
            hasta.SensorEkle(ates);
            hasta.SensorEkle(oksijen);

            Console.WriteLine("--- SaglikIz Sistemi Test Basliyor ---");
            Console.WriteLine($"Hasta: {hasta.AdSoyad} (TC: {hasta.TcNo})");

            // 5. Birkaç ölçüm yap ve sonuçları gör
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\nOlcum seti {i+1}:");
                foreach (Sensor sensor in hasta.Sensorler)
                {
                    yonetici.VeriGuncelle(sensor);

                    if (sensor is AtesSensoru)
                    {
                        Console.WriteLine($"{sensor.SensorAdi}: {sensor.MevcutDeger:F1}");
                    }
                    else if (sensor is OksijenSensoru)
                    {
                        Console.WriteLine($"{sensor.SensorAdi}: %{sensor.MevcutDeger}");
                    }
                    else
                    {
                        Console.WriteLine($"{sensor.SensorAdi}: {sensor.MevcutDeger}");
                    }
                }
            }
        }
    }
}