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
    public partial class Ghiceste_Regiunea : Form
    {
        public Ghiceste_Regiunea()
        {
            InitializeComponent();
        }
        List<TextBox> list = new List<TextBox>();
        TextBox CurrentTextbox = new TextBox();
        int nota = 1;

        private void Ghiceste_Regiunea_Load(object sender, EventArgs e)
        {
        }
        private void DrawRomania()
        {
            Pen pen = new Pen(Color.Green);
            
            foreach(string file in Directory.GetFiles(Resources.hartiString))
            {
                if (file.Contains("RomaniaMare"))
                {
                    pen.Color = Color.Green;
                }
                else
                {
                    pen.Color= Color.Red;

                }
                using(StreamReader rdr = new StreamReader(file))
                {
                    Point prevpos=new Point();
                    
                    while(rdr.Peek() != -1) 
                    {
                        Graphics g = this.CreateGraphics();
                        var line = rdr.ReadLine().Split('*');
                        if(line.Length == 3)
                        {
                            TextBox textBox = new TextBox();
                            textBox.Tag = line[2];
                            textBox.Location= new Point(Int32.Parse(line[0]), Int32.Parse(line[1]));
                            list.Add(textBox);
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

        private void Start_Click(object sender, EventArgs e)
        {
            DrawRomania();

            CurrentTextbox = list[0];
            this.Controls.Add(CurrentTextbox);
            list.RemoveAt(0);
            Start.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (list.Count > 0)
            {
                if (CurrentTextbox.Text == CurrentTextbox.Tag.ToString())
                {
                    nota++;
                    label1.Text="Nota: "+nota.ToString();
                    this.Controls.Add(list[0]);
                    CurrentTextbox = list[0];
                    list.RemoveAt(0);
                }
                else
                {
                    CurrentTextbox.Text = "-"+CurrentTextbox.Text.ToString()+"-";
                    TextBox textbox = new TextBox();
                    textbox.Text = CurrentTextbox.Tag.ToString();
                    textbox.Location = new Point(CurrentTextbox.Location.X, CurrentTextbox.Location.Y + 30);

                    textbox.ReadOnly = true;
                    this.Controls.Add(textbox);
                    this.Controls.Add(list[0]);
                    CurrentTextbox = list[0];
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var diploma = new Diploma(nota);
            diploma.ShowDialog();
            this.Close();
        }
    }
}
