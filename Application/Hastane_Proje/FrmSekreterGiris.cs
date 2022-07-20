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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        sqlbaglanti bglsekreter = new sqlbaglanti();

        private void BtnGiris_Click(object sender, EventArgs e)
        {
           if(MskTc.Text == "" || TxtSifre.Text == "" || checkBox1.Checked == false)
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komut = new SqlCommand("Select * From Tbl_Sekreterler where Sekretertc=@p1 and Sekretersifre=@p2", bglsekreter.baglanti());
                komut.Parameters.AddWithValue("@p1", MskTc.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmSekreterDetay frm = new FrmSekreterDetay();
                    frm.sekretertc = MskTc.Text;
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik No veya Şifre hatalı lütfen tekrar deneyiniz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                bglsekreter.baglanti().Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form1 frmcaptcha = new Form1();
            frmcaptcha.Show();
        }

        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
    }
}
