using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2018v_
{
    public partial class Form1 : Form
    {
        private bool isLogged = false;
        public Form1(bool isLogged1)
        {
            InitializeComponent();
            isLogged = isLogged1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InsertIntoDB();
            if (!isLogged)
            {
                button3.Hide();
                button4.Hide();
                button5.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var lectii = new VizualizareLectii();
            lectii.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CentenarLogare logare = new CentenarLogare();
            logare.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CreeazaLectie logare = new CreeazaLectie();
            logare.ShowDialog();
            this.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var logare = new Ghiceste_Regiunea();
            logare.ShowDialog();
            this.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            var logare = new GenerareTraseu();
            logare.ShowDialog();
            this.Show();

        }
    }
}
