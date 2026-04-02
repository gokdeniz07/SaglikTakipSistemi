using System;
using System.Windows.Forms;
using System.Drawing;
using SaglikIzSistemi.Modeller;
using SaglikIzSistemi.Cekirdek.TasarimDesenleri;

namespace SaglikIzSistemi.Arayuz
{
    public class AnaForm : Form
    {
        // 1. DEĞİŞKENLER: Her şeyi tek sefer tanımlıyoruz.
        private Hasta _hasta;
        private SensorYoneticisi _yonetici;
        private Kullanici _girisYapanKullanici;
        private System.Windows.Forms.Timer _otomatikOlcumTimer;
        private ListView lsvSensorler;
        private Button btnEkle;
        private Button btnSil;
        private Button btnOlcumYap;
        private ComboBox cmbSensorTipi;
        private Label lblHastaBilgi;
        private Panel pnlOzet;
        private Label lblToplamSensor;
        private Label lblKritikSayisi;
        private Label lblSistemDurumu;

        // 2. CONSTRUCTOR (Yapıcı Metot)
        public AnaForm(Kullanici user) 
        {
            _girisYapanKullanici = user; 
            
            this.Text = $"Sağlıkİz - {_girisYapanKullanici.Rol} Paneli"; 
            this.Size = new Size(480, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            _hasta = new Hasta("12345678901", "Gökdeniz Demir");
            _yonetici = new SensorYoneticisi();
            
            _yonetici.GozlemciEkle(new AlarmSistemi());
            _yonetici.GozlemciEkle(new LogSistemi());

            BilesenleriHazirla();

            // --- YETKİ VE OTOMASYON KONTROLÜ ---
            if (_girisYapanKullanici.Rol == "Hasta")
            {
                // 1. Hasta için varsayılan sensörleri biz ekliyoruz
                _hasta.SensorEkle(new NabizSensoru("H-01"));
                _hasta.SensorEkle(new AtesSensoru("H-02"));
                _hasta.SensorEkle(new OksijenSensoru("H-03"));
                ListeyiGuncelle();

                // 2. Müdahaleyi engelle (Butonları kapat)
                btnEkle.Enabled = false; 
                btnSil.Enabled = false;  
                cmbSensorTipi.Enabled = false;
                btnOlcumYap.Visible = false; // Manuel ölçüm butonunu tamamen gizleyebiliriz

                // 3. 5 Saniyede bir otomatik ölçüm yapacak Timer'ı kur
                _otomatikOlcumTimer = new System.Windows.Forms.Timer();
                _otomatikOlcumTimer.Interval = 5000; // 5 saniye
                _otomatikOlcumTimer.Tick += (s, e) => BtnOlcumYap_Click(null, null); // Butona basılmış gibi yap
                _otomatikOlcumTimer.Start();

                lblSistemDurumu.Text = "Mod: Otomatik Canlı İzleme";
                lblSistemDurumu.ForeColor = Color.Blue;
            }
            else if (_girisYapanKullanici.Rol == "Doktor")
            {
                btnEkle.Enabled = true; 
                btnSil.Enabled = true;  
                cmbSensorTipi.Enabled = true;
                btnOlcumYap.Visible = true;
                lblSistemDurumu.Text = "Mod: Yönetici (Manuel)";
                lblSistemDurumu.ForeColor = Color.DarkGreen;
            }

            IstatistikGuncelle();
        }

        private void BilesenleriHazirla()
        {
            // 1. Hasta Bilgi Etiketi
            lblHastaBilgi = new Label() { 
                Text = $"Hasta: {_hasta.AdSoyad} (TC: {_hasta.TcNo})", 
                Location = new Point(20, 20), 
                Size = new Size(400, 20), 
                Font = new Font("Arial", 10, FontStyle.Bold) 
            };
            this.Controls.Add(lblHastaBilgi);

            pnlOzet = new Panel() { 
                Location = new Point(20, 350), 
                Size = new Size(420, 80), 
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.WhiteSmoke
            };

            // 2. Sensör Tipi Seçimi
            cmbSensorTipi = new ComboBox() { Location = new Point(20, 50), Width = 150 };
            cmbSensorTipi.Items.AddRange(new string[] { "Nabız Sensörü", "Ateş Sensörü", "Oksijen Sensörü" });
            cmbSensorTipi.SelectedIndex = 0;
            lblToplamSensor = new Label() { Text = "Toplam Sensör: 0", Location = new Point(10, 10), Size = new Size(150, 20) };
            lblKritikSayisi = new Label() { Text = "Kritik Durum: 0", Location = new Point(10, 30), Size = new Size(150, 20) };
            lblSistemDurumu = new Label() { Text = "Sistem Durumu: Bekleniyor", Location = new Point(10, 50), Size = new Size(250, 20), Font = new Font("Arial", 9, FontStyle.Bold) };

            pnlOzet.Controls.AddRange(new Control[] { lblToplamSensor, lblKritikSayisi, lblSistemDurumu });
            this.Controls.Add(pnlOzet);

            // 3. Ekle Butonu
            btnEkle = new Button() { Text = "Sensör Ekle", Location = new Point(180, 48), Width = 100 };
            btnEkle.Click += BtnEkle_Click;

            // 4. SENSÖR LİSTESİ
            lsvSensorler = new ListView() 
            { 
                Location = new Point(20, 90), 
                Size = new Size(420, 200),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            
            lsvSensorler.Columns.Add("Sensör Tipi", 150);
            lsvSensorler.Columns.Add("ID", 80);
            lsvSensorler.Columns.Add("Durum / Değer", 150);

            // 5. Sil Butonu
            btnSil = new Button() { Text = "Seçili Sensörü Sil", Location = new Point(20, 300), Width = 150, BackColor = Color.LightCoral };
            btnSil.Click += BtnSil_Click;

            // 6. Ölçüm Yap Butonu
            btnOlcumYap = new Button() { Text = "Tüm Sensörlerden Veri Oku", Location = new Point(200, 300), Width = 240, BackColor = Color.LightGreen };
            btnOlcumYap.Click += BtnOlcumYap_Click;

            this.Controls.AddRange(new Control[] { cmbSensorTipi, btnEkle, lsvSensorler, btnSil, btnOlcumYap });
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            string id = "S-" + (Guid.NewGuid().ToString().Substring(0, 4));
            Sensor yeni;
            string secim = cmbSensorTipi.SelectedItem.ToString();
            
            if (secim == "Nabız Sensörü") yeni = new NabizSensoru(id);
            else if (secim == "Ateş Sensörü") yeni = new AtesSensoru(id);
            else yeni = new OksijenSensoru(id);

            _hasta.SensorEkle(yeni);
            ListeyiGuncelle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            try 
            {
                if (lsvSensorler.SelectedItems.Count > 0)
                {
                    string id = lsvSensorler.SelectedItems[0].SubItems[1].Text;
                    _hasta.SensorSil(id);
                    ListeyiGuncelle();
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz sensörü seçin usta!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOlcumYap_Click(object sender, EventArgs e)
        {
            lsvSensorler.Items.Clear();
            foreach (var sensor in _hasta.Sensorler)
            {
                _yonetici.VeriGuncelle(sensor);
                ListViewItem satir = new ListViewItem(sensor.SensorAdi);
                satir.SubItems.Add(sensor.SensorId);
                satir.SubItems.Add(sensor.MevcutDeger.ToString("F1"));

                if (sensor.KritikMi())
                {
                    satir.BackColor = Color.Red;
                    satir.ForeColor = Color.White;
                }
                else
                {
                    satir.BackColor = Color.LightGreen;
                }
                lsvSensorler.Items.Add(satir);
            }
            IstatistikGuncelle();
        }

        private void ListeyiGuncelle()
        {
            lsvSensorler.Items.Clear();
            foreach (var s in _hasta.Sensorler)
            {
                ListViewItem satir = new ListViewItem(s.SensorAdi);
                satir.SubItems.Add(s.SensorId);
                satir.SubItems.Add("Bekliyor...");
                lsvSensorler.Items.Add(satir);
            }
            IstatistikGuncelle();
        }

        private void IstatistikGuncelle()
        {
            int toplam = _hasta.Sensorler.Count;
            int kritik = 0;
            foreach (var s in _hasta.Sensorler)
            {
                if (s.KritikMi()) kritik++;
            }

            lblToplamSensor.Text = $"Toplam Sensör: {toplam}";
            lblKritikSayisi.Text = $"Kritik Durum: {kritik}";

            if (kritik > 0)
            {
                lblSistemDurumu.Text = "SİSTEM DURUMU: ACİL MÜDAHALE!";
                lblSistemDurumu.ForeColor = Color.Red;
            }
            else if (toplam > 0)
            {
                lblSistemDurumu.Text = "SİSTEM DURUMU: HER ŞEY YOLUNDA";
                lblSistemDurumu.ForeColor = Color.Green;
            }
        }
    }
}