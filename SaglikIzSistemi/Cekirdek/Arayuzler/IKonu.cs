namespace SaglikIzSistemi.Cekirdek.Arayuzler
{
    // Hocam, bu arayüz 'Konu' (Subject) yani takip edilen nesnedir.
    // Takipçilerin listesini tutar ve bir değişiklik olduğunda onlara haber verir.
    public interface IKonu
    {
        void GozlemciEkle(IGozlemci gozlemci);
        void GozlemciCikar(IGozlemci gozlemci);
        void HaberVer();
    }
}

// Loose Coupling (Gevşek Bağlılık): "Hocam, AlarmSistemi ile Sensor sınıflarını birbirine göbekten bağlamadım.
// Arada bu arayüzleri kullandım. Böylece yarın bir gün 'SMS Gönderici' diye yeni bir modül eklemek istersem,
// mevcut kodların hiçbirini değiştirmeme gerek kalmayacak.