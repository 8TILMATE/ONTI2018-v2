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
    public partial class Diploma : Form
    {
        int punctaj;
        public Diploma(int nota)
        {
            InitializeComponent();
            punctaj = nota;
        }

        private void Diploma_Load(object sender, EventArgs e)
        {
            if (punctaj == 10)
            {
                label1.Text += " premiul 1";
            }
            else if (punctaj == 9)
            {
                label1.Text += " premiul 2";
            }
            else if (punctaj == 8)
            {
                label1.Text += " premiul 3";
            }
            else if(punctaj<=7 && punctaj >= 5)
            {
                label1.Text += " mentiune";
            }
            else
            {
                label1.Text += " diploma de participare";

            }
        }
    }
}
