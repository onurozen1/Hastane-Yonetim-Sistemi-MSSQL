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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();

        public string sekretertc;

        void temizle()
        {
            CmbBrans.Text = "";
            CmbDoktor.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
        }

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = sekretertc;

            // ad soyad çekme
            SqlCommand komut = new SqlCommand("Select Sekreteradsoyad From Tbl_Sekreterler where Sekretertc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            // branşları datagride aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // doktorları datagride aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (Doktorad + ' ' + Doktorsoyad) as 'Doktorlar',Doktorbrans From Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // branşları comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("Select Bransad From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (CmbBrans.Text == "" || CmbDoktor.Text == "" || MskTarih.MaskFull == false || MskSaat.MaskFull == false) 
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (Randevubrans,Randevudoktor,Randevutarih,Randevusaat) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
                komutkaydet.Parameters.AddWithValue("@p1", CmbBrans.Text);
                komutkaydet.Parameters.AddWithValue("@p2", CmbDoktor.Text);
                komutkaydet.Parameters.AddWithValue("@p3", MskTarih.Text);
                komutkaydet.Parameters.AddWithValue("@p4", MskSaat.Text);
                komutkaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Randevu oluşturuldu .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        // Branş seçtikten sonra combobox'a o branşdaki doktorları ekledik
        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("Select (Doktorad+' '+Doktorsoyad) From Tbl_Doktorlar where Doktorbrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }

        private void BtnOluştur_Click(object sender, EventArgs e)
        {
            if (RchDuyuru.Text == "")
            {
                MessageBox.Show("Lütfen metin giriniz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru oluşturuldu .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RchDuyuru.Text = "";
            }
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPanel frm = new FrmDoktorPanel();
            frm.Show();
        }

        private void BtnBranşPanel_Click(object sender, EventArgs e)
        {
            FrmBransPanel frm = new FrmBransPanel();
            frm.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListe frm = new FrmRandevuListe();
            frm.Show();
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }
    }
}
