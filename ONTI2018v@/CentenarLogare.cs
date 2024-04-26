using ONTI2018v_.Models;
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
    public partial class CentenarLogare : Form
    {
        public CentenarLogare()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach(UserModel user in DatabaseHelper.user)
            {
                if (user.Email.Trim() == textBox1.Text && user.Password.Trim() == textBox2.Text)
                {
                    DatabaseHelper.utilizatorLogat = user;
                    this.Hide();
                    Form1 start = new Form1(true);
                    start.ShowDialog();
                    this.Close();
                    break;
                }
               
            }
           
                MessageBox.Show("Eroare la autentificare!");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (UserModel model in DatabaseHelper.user)
            {
                if (model.Email.Trim() == textBox1.Text)
                {
                    this.Hide();
                    AmUitatParola start = new AmUitatParola(model.Email);
                    start.ShowDialog();
                    this.Show();
                    break;
                }
            }
        }

        private void CentenarLogare_Load(object sender, EventArgs e)
        {
            DatabaseHelper.user.Clear();
            DatabaseHelper.GetUtilizatori();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
