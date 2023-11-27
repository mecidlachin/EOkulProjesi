using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EOkulProjesi
{
    public partial class Frm_Ogrenci_Notlar : Form
    {
        public Frm_Ogrenci_Notlar()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OLQS08S\MSSQLSERVER02;Initial Catalog=BonusOkul;User ID=sa; Integrated Security = True");
        public string numara;
        private void Frm_Ogrenci_Notlar_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // Öğrenci Adını Tbl_Ogrenciler Tablosundan Çekme İşlemi
                SqlCommand adSorgu = new SqlCommand("SELECT OgrenciAdi FROM Tbl_Ogrenciler WHERE OgrenciId = @p1", baglanti);
                adSorgu.Parameters.AddWithValue("@p1", numara);

                SqlDataAdapter adDa = new SqlDataAdapter(adSorgu);
                DataTable adDt = new DataTable();
                adDa.Fill(adDt);

                // Öğrenci Adını Formun Başına Ekleme İşlemi
                if (adDt.Rows.Count > 0)
                {
                    string ogrenciAdi = adDt.Rows[0]["OgrenciAdi"].ToString();
                    this.Text = "Notlar : " + ogrenciAdi;
                }

                // Notları Çekme İşlemi
                SqlCommand notSorgu = new SqlCommand("SELECT DersAd, Sinav1, Sinav2, Sinav3, Proje, Ortalama, Durum FROM Tbl_Notlar INNER JOIN Tbl_Dersler ON Tbl_Notlar.DersId = Tbl_Dersler.DersId WHERE OgrenciId = @p1", baglanti);
                notSorgu.Parameters.AddWithValue("@p1", numara);

                SqlDataAdapter notDa = new SqlDataAdapter(notSorgu);
                DataTable notDt = new DataTable();
                notDa.Fill(notDt);

                dataGridView1.DataSource = notDt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                baglanti.Close();
            }
        }

    }
}

