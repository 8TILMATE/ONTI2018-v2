using ONTI2018v_.Models;
using ONTI2018v_.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2018v_
{
    public partial class VizualizareLectii : Form
    {
        public static List<string> Lectii = new List<string>();
        public VizualizareLectii()
        {
            InitializeComponent();
        }

        private void VizualizareLectii_Load(object sender, EventArgs e)
        {
            DatabaseHelper.GetLectii();
            DatabaseHelper.GetUtilizatori();
            foreach(LectiiModel lectie in DatabaseHelper.lectii)
            {
                listBox1.Items.Add(lectie.NumeImagine);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = Resources.continutLectiiString + listBox1.SelectedItem.ToString().Trim() + ".bmp";
            foreach(UserModel user in DatabaseHelper.user)
            {
                foreach(LectiiModel model in DatabaseHelper.lectii)
                {
                    if(model.NumeImagine.Trim()== listBox1.SelectedItem.ToString().Trim())
                    {
                        if (model.IdUser == user.IdUtilizator)
                        {
                            textBox1.Text = user.Name + '\n' + user.Email + '\n' + model.Regiune + '\n' + model.DataCreare.ToString();
                        }
                    }
                }
            }
        }
    }
}
