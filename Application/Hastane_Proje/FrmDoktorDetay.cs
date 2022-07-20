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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public string doktortc;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = doktortc;

            // ad soyad çekme
            SqlCommand komut = new SqlCommand("Select Doktorad,Doktorsoyad From Tbl_Doktorlar where Doktortc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", doktortc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[0].ToString() + " " + dr[1].ToString();
            }
            bgl.baglanti().Close();

            // doktorun aktif randevuları
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where Randevudoktor='" + LblAdsoyad.Text + "' and Randevudurum=1", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorGuncelle frmdoktorguncelle = new FrmDoktorGuncelle();
            frmdoktorguncelle.doktortc = LblTc.Text;
            frmdoktorguncelle.Show();
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // şikayetleri richtextboxa çekme
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
