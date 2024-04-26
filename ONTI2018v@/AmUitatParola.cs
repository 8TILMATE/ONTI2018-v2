using ONTI2018v_.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ONTI2018v_
{
    public partial class AmUitatParola : Form
    {
        private List<string> Imagini = new List<string>();
        private List<string> ToBeSelected = new List<string>();
        private List<string> ImaginiUmanoide = new List<string>();
        private List<string> ImaginiSelectate = new List<string>();
        private string adresa;
        public AmUitatParola(string email)
        {
            InitializeComponent();
            adresa = email;
        }

        private void AmUitatParola_Load(object sender, EventArgs e)
        {
            LoadImaginiUmanoide();
            foreach (string s in Directory.GetFiles(Resources.captchaString))
            {
                Imagini.Add(s);
            }
            RandomisePicture(pictureBox1);
            pictureBox1.Click += pictureBox6_Click;
            RandomisePicture(pictureBox2);
            pictureBox2.Click += pictureBox6_Click;
            RandomisePicture(pictureBox3);
            pictureBox3.Click += pictureBox6_Click;
            RandomisePicture(pictureBox4);
            pictureBox4.Click += pictureBox6_Click;
            RandomisePicture(pictureBox5);
            pictureBox5.Click += pictureBox6_Click;
            RandomisePicture(pictureBox6);
            
        }
        private void RandomisePicture(PictureBox pictureBox)
        {
            int index = 0;
            Random rand = new Random();
            index=rand.Next(0, Imagini.Count);
            pictureBox.ImageLocation = Imagini[index];
            Imagini.RemoveAt(index);
            foreach(string s in ImaginiUmanoide)
            {
                if (pictureBox.ImageLocation.Contains(s))
                {
                    ToBeSelected.Add(pictureBox.ImageLocation);
                    break;
                }
            }
        }
        private void LoadImaginiUmanoide()
        {
            using(StreamReader rdr = new StreamReader(Resources.oameniString))
            {
                while (rdr.Peek() >= 1)
                {
                    var line = rdr.ReadLine();
                    ImaginiUmanoide.Add(line.Trim());  
                }
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BorderStyle == BorderStyle.None)
            {
                pictureBox.BorderStyle = BorderStyle.Fixed3D;
                
                ImaginiSelectate.Add(pictureBox.ImageLocation);
            }
            else if(pictureBox.BorderStyle == BorderStyle.Fixed3D)
            {
                pictureBox.BorderStyle = BorderStyle.None;
                ImaginiSelectate.Remove(pictureBox.ImageLocation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool human = true;
            foreach(string s in ToBeSelected)
            {
                if (!ImaginiSelectate.Contains(s))
                {
                    human = false;
                }
            }
            if(human)
            {
                if (textBox1.Text == textBox2.Text)
                {
                    DatabaseHelper.UpdatePassword(textBox1.Text.Trim(), adresa);
                    DatabaseHelper.user.Clear();
                    DatabaseHelper.GetUtilizatori();
                }
            }
        }
    }
}
