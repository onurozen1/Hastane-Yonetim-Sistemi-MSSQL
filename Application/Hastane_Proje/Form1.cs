using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void captcha()
        {
            string[] harf = { "a", "b", "c", "d", "e", "f" };
            char[] sembol = { '+', '-', '*', '/', '$', '#' };
            Random rnd = new Random();
            int s1, s2, s3, s4;
            s1 = rnd.Next(0, 5);
            s2 = rnd.Next(0, harf.Length);
            s3 = rnd.Next(0, 5);
            s4 = rnd.Next(0, sembol.Length);
            label2.Text = s1.ToString() + harf[s2].ToString() + s3.ToString() + sembol[s4].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label2.Text == textBox1.Text)
            {
                MessageBox.Show("Tebrikler,onaylandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
            }
            else
            {
                textBox1.Text = "";
                MessageBox.Show("Tekrar deneyiniz.", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                captcha();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            captcha();
        }
    }
}
