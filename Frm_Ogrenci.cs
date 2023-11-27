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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EOkulProjesi
{
    public partial class Frm_Ogrenci : Form
    {
        public Frm_Ogrenci()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OLQS08S\MSSQLSERVER02;Initial Catalog=BonusOkul;User ID=sa; Integrated Security = True");

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();



        private void Frm_Ogrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Tbl_Kulupler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember = "KulupAd";
            comboBox1.ValueMember = "KulupId";
            comboBox1.DataSource = dt;
            baglanti.Close();
        }

        string c = "";
        private void btnEkle_Click(object sender, EventArgs e)
        {
            ds.OgrenciEkle(txtAd.Text, txtSoyad.Text, byte.Parse(comboBox1.SelectedValue.ToString()), c);
            MessageBox.Show("Öğrenci Başarıyla Eklendi");
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // txtId.Text = comboBox1.SelectedValue.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(txtId.Text));
            MessageBox.Show("Öğrenci Başarıyla Silinmiştir");
        }
        string cins = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seçilen satırdaki cinsiyet bilgisini alınır
            string cinsiyet = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

            // Radyo butonları temizler
            rdbKiz.Checked = false;
            rdbErkek.Checked = false;

            // Diğer bilgileri TextBox ve ComboBox'lara atayınır
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            // Cinsiyet bilgisine göre radyo butonlarını kontrol edilir
            if (cinsiyet == "Kiz")
            {
                rdbKiz.Checked = true;
            }
            else if (cinsiyet == "Erkek")
            {
                rdbErkek.Checked = true;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(txtAd.Text, txtSoyad.Text, byte.Parse(comboBox1.SelectedValue.ToString()), c, int.Parse(txtId.Text));
            MessageBox.Show("Kaydınız Başarıyla Güncellenmiştir");
        }

        private void rdbKiz_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbKiz.Checked == true)
            {
                c = "Kız";
            }
        }

        private void rdbErkek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbErkek.Checked == true)
            {
                c = "Erkek";
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(txtAra.Text);
            // Arama Yapıldıktan sonra txtAra nın içeriğini temizler
            txtAra.Text = string.Empty;
        }
    }
}
