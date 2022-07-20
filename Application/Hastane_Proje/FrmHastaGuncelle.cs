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
    public partial class FrmHastaGuncelle : Form
    {
        public FrmHastaGuncelle()
        {
            InitializeComponent();
        }

        sqlbaglanti bglkayıt = new sqlbaglanti();

        public string tcno;

        private void label7_Click(object sender, EventArgs e)
        {

        }

        // hasta bilgilerini güncelleme
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Merhaba" + " " + TxtAd.Text + " " + "bilgileriniz güncellensin mi ?", "Hasta Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                SqlCommand komut1 = new SqlCommand("Update Tbl_Hastalar set Hastaad=@p1,Hastasoyad=@p2,Hastatel=@p3,Hastacinsiyet=@p4,Hastasifre=@p5 where Hastatc=@p6 ", bglkayıt.baglanti());
                komut1.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut1.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut1.Parameters.AddWithValue("@p3", MskTel.Text);
                komut1.Parameters.AddWithValue("@p4", label8.Text);
                komut1.Parameters.AddWithValue("@p5", TxtSifre.Text);
                komut1.Parameters.AddWithValue("@p6", MskTc.Text);
                komut1.ExecuteNonQuery();
                bglkayıt.baglanti().Close();
            }
        }

        private void FrmHastaGuncelle_Load(object sender, EventArgs e)
        {
            MskTc.Text = tcno;

            //bilgileri çekme
            SqlCommand komut = new SqlCommand("Select Hastaad,Hastasoyad,Hastatel,Hastasifre,Hastacinsiyet From Tbl_Hastalar where Hastatc=@p1", bglkayıt.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[0].ToString();
                TxtSoyad.Text = dr[1].ToString();
                MskTel.Text = dr[2].ToString();
                TxtSifre.Text = dr[3].ToString();
                label8.Text = dr[4].ToString();
            }
            bglkayıt.baglanti().Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            else if (label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }
    }
}
