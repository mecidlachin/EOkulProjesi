using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOkulProjesi
{
    public partial class Frm_Giris : Form
    {
        public Frm_Giris()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
                // Öğrenci numarası boşsa uyarı ver
                if (string.IsNullOrWhiteSpace(txtOgrenciNumara.Text))
                {
                    MessageBox.Show("Lütfen bir öğrenci numarası giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Öğrenci numarası girilmişse Frm_Ogretmen formuna geçiş yap
                Frm_Ogrenci_Notlar fr = new Frm_Ogrenci_Notlar();
                fr.numara = txtOgrenciNumara.Text;
                fr.Show();
                this.Hide();
            }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Frm_Ogretmen fr = new Frm_Ogretmen();
            fr.Show();
            this.Hide();
        }
    }
    }

