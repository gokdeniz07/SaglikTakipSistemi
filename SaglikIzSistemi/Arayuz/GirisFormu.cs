using System;
using System.Windows.Forms;
using System.Drawing;
using SaglikIzSistemi.Modeller;

namespace SaglikIzSistemi.Arayuz
{
    public class GirisFormu : Form
    {
        public GirisFormu()
        {
            this.Text = "Med-Track Pro | Giriş";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblSoru = new Label() { Text = "Sisteme Giriş Yapın", Location = new Point(20, 20), Size = new Size(300, 30), Font = new Font("Arial", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };

            Button btnDoktor = new Button() { Text = "DOKTOR GİRİŞİ", Location = new Point(75, 70), Size = new Size(200, 45), BackColor = Color.AliceBlue };
            btnDoktor.Click += (s, e) => AnaFormuAc(new Doktor("Dr. Gökdeniz Demir"));

            Button btnHasta = new Button() { Text = "HASTA GİRİŞİ", Location = new Point(75, 130), Size = new Size(200, 45), BackColor = Color.MistyRose };
            btnHasta.Click += (s, e) => AnaFormuAc(new HastaKullanici("Hasta Gökdeniz"));

            this.Controls.AddRange(new Control[] { lblSoru, btnDoktor, btnHasta });
        }

        private void AnaFormuAc(Kullanici user)
        {
            // Giriş yapan kişiyi AnaForm'a gönderiyoruz (Dependency Injection mantığı)
            AnaForm ana = new AnaForm(user);
            this.Hide(); 
            ana.ShowDialog();
            this.Close(); 
        }
    }
}