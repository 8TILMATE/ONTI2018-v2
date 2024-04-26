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
    public partial class GenerareTraseu : Form
    {
        List<Label> list = new List<Label>();
        private int indexDesen = 0;
        private Point prevPos = new Point();
        public GenerareTraseu()
        {
            InitializeComponent();
        }

        private async void GenerareTraseu_Load(object sender, EventArgs e)
        {
            await Task.Delay(100);
            DrawRomania();
        }
        private void DrawRomania()
        {
            Pen pen = new Pen(Color.Green);

            foreach (string file in Directory.GetFiles(Resources.hartiString))
            {
                if (file.Contains("RomaniaMare"))
                {
                    pen.Color = Color.Green;
                }
                else
                {
                    pen.Color = Color.Red;

                }
                using (StreamReader rdr = new StreamReader(file))
                {
                    Point prevpos = new Point();

                    while (rdr.Peek() != -1)
                    {
                        Graphics g = this.CreateGraphics();
                        var line = rdr.ReadLine().Split('*');
                        if (line.Length == 3)
                        {
                            Label label = new Label();
                            label.Tag = line[2];
                            label.Text = line[2];
                            label.Location = new Point(Int32.Parse(line[0]), Int32.Parse(line[1]));
                            this.Controls.Add(label);
                            listBox1.Items.Add(label.Text);
                            list.Add(label);
                        }
                        if (prevpos.X == 0 && prevpos.Y == 0)
                        {
                            g.DrawLine(pen, new Point(Int32.Parse(line[0]), Int32.Parse(line[1])), new Point(Int32.Parse(line[0]), Int32.Parse(line[1])));
                        }
                        else
                        {
                            g.DrawLine(pen, prevpos, new Point(Int32.Parse(line[0]), Int32.Parse(line[1])));

                        }
                        g.Dispose();
                        prevpos = new Point(Int32.Parse(line[0]), Int32.Parse(line[1]));
                    }
                }
            }
        }
        private void DrawLines()
        {
            Label punctdeplecare = new Label();
            foreach(Label label in list)
            {
                if (label.Text ==listBox1.SelectedItem)
                {
                    punctdeplecare = label;
                    break;
                }
            }
            list.Remove(punctdeplecare);
            list.Add(punctdeplecare);
            indexDesen = list.Count - 1;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawLines();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Color.Purple, 1);
            if (indexDesen >= 0)
            {
                if(prevPos.X==0&&prevPos.Y==0) 
                {
                    prevPos = list[indexDesen].Location;
                }
                g.DrawLine(pen, prevPos, list[indexDesen].Location);
                prevPos = list[indexDesen].Location;
                indexDesen--;
                
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
