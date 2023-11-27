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
    public partial class Frm_Ogretmen : Form
    {
        public Frm_Ogretmen()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Frm_Kulup kulup = new Frm_Kulup();
            kulup.Show();
        }

        private void btnDers_Click(object sender, EventArgs e)
        {
            Frm_Dersler fr = new Frm_Dersler();
            fr.Show();
            this.Hide();
        }

        private void btnOgrenciislemleri_Click(object sender, EventArgs e)
        {
            Frm_Ogrenci fr = new Frm_Ogrenci();
            fr.Show();
        }

        private void btnSinavNotlari_Click(object sender, EventArgs e)
        {
            Frm_SinavNotlar fr = new Frm_SinavNotlar();
            fr.Show();
        } 
    }
}
