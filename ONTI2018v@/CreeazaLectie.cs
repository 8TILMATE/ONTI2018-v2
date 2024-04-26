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
    public partial class CreeazaLectie : Form
    {
        public CreeazaLectie()
        {
            InitializeComponent();
        }
        int countr = 1;
        int countc = 1;
        int lastadded = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent));
            tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowStyles.Count - 1].Height = 100 / countr;
            countr++;
            lastadded = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowStyles.Count - 1].Height += 10;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowStyles.Count - 1].Height -= 10;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.RowStyles.RemoveAt(tableLayoutPanel1.RowStyles.Count - 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
           
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.ShowDialog();
                var adresa =dialog.FileName;
                PictureBox pictureBox = new PictureBox();
                //pictureBox.Size = new Size((int)tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnStyles.Count - 1].Width,  (int)tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowStyles.Count - 1].Height);
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.ImageLocation = adresa;
                tableLayoutPanel1.Controls.Add(pictureBox, tableLayoutPanel1.ColumnStyles.Count - 1, tableLayoutPanel1.RowStyles.Count - 1);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnStyles.Count - 1].Width += 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent));
            tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnStyles.Count - 1].Width = 100 / countc;
            lastadded = 0;
            countc++;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnStyles.Count - 1].Width -= 10;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.ColumnStyles.RemoveAt(tableLayoutPanel1.ColumnStyles.Count - 1);
        }

        private void button10_Click(object sender, EventArgs e)
        {

            TextBox textBox = new TextBox();
            textBox.Size = new Size((int)tableLayoutPanel1.ColumnStyles[tableLayoutPanel1.ColumnStyles.Count - 1].Width, (int)tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowStyles.Count - 1].Height);
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            
            tableLayoutPanel1.Controls.Add(textBox, tableLayoutPanel1.ColumnStyles.Count - 1, tableLayoutPanel1.RowStyles.Count - 1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Rectangle rect = tableLayoutPanel1.DisplayRectangle;
            Bitmap bitmap = new Bitmap(rect.Width,rect.Height);
            tableLayoutPanel1.DrawToBitmap(bitmap,rect);
            bitmap.Save(Properties.Resources.continutLectiiString + textBox1.Text + ".bmp");
            DatabaseHelper.InsertLectie(textBox3.Text, textBox2.Text, textBox1.Text);
            DatabaseHelper.lectii.Clear();
            DatabaseHelper.GetLectii();
        }
    }
}
