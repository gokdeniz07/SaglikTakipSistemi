using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Cekirdek.Arayuzler
{
    // Hocam, bu arayüz 'Gözlemci' (Observer) deseninin temelidir.
    // Bu arayüzü uygulayan her sınıf, sensör verisi güncellendiğinde 'Guncelle' metodunu çalıştırır.
    public interface IGozlemci
    {
        void Guncelle(Sensor sensor);
    }
}