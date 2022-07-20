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

namespace Hastane_Proje
{
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        sqlbaglanti bgldoktor = new sqlbaglanti();

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            if (MskTc.Text == "" || TxtSifre.Text == "" || checkBox1.Checked == false)
            {
                MessageBox.Show("Boş alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where Doktortc=@p1 and Doktorsifre=@p2", bgldoktor.baglanti());
                komut.Parameters.AddWithValue("@p1", MskTc.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmDoktorDetay frmdoktor = new FrmDoktorDetay();
                    frmdoktor.doktortc = MskTc.Text;
                    frmdoktor.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tc Kimlik No veya şifre hatalı lütfen tekrar deneyiniz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                bgldoktor.baglanti().Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
