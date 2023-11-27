using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EOkulProjesi
{
    public partial class Frm_Kulup : Form
    {
        public Frm_Kulup()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OLQS08S\MSSQLSERVER02;Initial Catalog=BonusOkul;User ID=sa; Integrated Security = True");

        void liste()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Kulupler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Frm_Kulup_Load(object sender, EventArgs e)
        {
            liste();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            liste();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKulupAd.Text))
            {
                MessageBox.Show("Kulüp Adı boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Kulüp adı boşsa, işlemi sonlandır
            }

            baglanti.Open();

            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Kulupler (KulupAd) VALUES (@p1)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtKulupAd.Text);

            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Kulüp Listeye Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            liste();
            txtKulupId.Clear();
            txtKulupAd.Clear();
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Frm_Ogretmen ogretmen = new Frm_Ogretmen();
            ogretmen.Show();
            this.Hide();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Silver;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKulupId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKulupAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKulupId.Text))
            {
                MessageBox.Show("Lütfen Silmek İstediğiniz kulübü seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Kulüp seçilmediyse, işlemi sonlandır
            }

            DialogResult result = MessageBox.Show("Seçili kulüp silinecek. Emin misiniz?", "Kulüp Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                baglanti.Open();

                SqlCommand komut = new SqlCommand("DELETE FROM Tbl_Kulupler WHERE KulupId = @p1", baglanti);
                komut.Parameters.AddWithValue("@p1", txtKulupId.Text);

                komut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Kulüp Silme İşlemi Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                liste();
                txtKulupId.Clear();
                txtKulupAd.Clear();

            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                // Mevcut kulüp adını alır
                SqlCommand selectKomut = new SqlCommand("SELECT KulupAd FROM Tbl_Kulupler WHERE KulupId = @p1", baglanti);
                selectKomut.Parameters.AddWithValue("@p1", txtKulupId.Text);

                string eskiKulupAd = selectKomut.ExecuteScalar()?.ToString();

                baglanti.Close();

                // Eğer değişiklik yoksa uyarı verir
                if (eskiKulupAd == txtKulupAd.Text)
                {
                    MessageBox.Show("Hiçbir değişiklik yapılmadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Değişiklik varsa, güncelleme işlemini gerçekleştirir
                baglanti.Open();

                SqlCommand updateKomut = new SqlCommand("UPDATE Tbl_Kulupler SET KulupAd = @p1 WHERE KulupId = @p2", baglanti);
                updateKomut.Parameters.AddWithValue("@p1", txtKulupAd.Text);
                updateKomut.Parameters.AddWithValue("@p2", txtKulupId.Text);

                updateKomut.ExecuteNonQuery();

                baglanti.Close();

                MessageBox.Show("Kulüp Güncelleme İşlemi Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                liste();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglanti.State == ConnectionState.Open)
                    baglanti.Close();
            }
            txtKulupId.Clear();
            txtKulupAd.Clear();
        }


    }
}
