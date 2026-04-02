namespace SaglikIzSistemi.Modeller
{
    // Kalıtımın babası: Tüm kullanıcılar buradan türer.
    public abstract class Kullanici
    {
        public string AdSoyad { get; set; }
        public string Rol { get; protected set; } // Sadece alt sınıflar rolü belirleyebilir.

        public Kullanici(string ad)
        {
            AdSoyad = ad;
        }
    }

    // DOKTOR SINIFI: Kullanıcıdan miras alır.
    public class Doktor : Kullanici
    {
        public Doktor(string ad) : base(ad)
        {
            Rol = "Doktor"; // Tam yetki
        }
    }

    // HASTA SINIFI: Kullanıcıdan miras alır.
    public class HastaKullanici : Kullanici
    {
        public HastaKullanici(string ad) : base(ad)
        {
            Rol = "Hasta"; // Kısıtlı yetki
        }
    }
}