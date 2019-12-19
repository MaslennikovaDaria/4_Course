using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4_Course_
{
    public partial class Form1 : Form
    {
        List<Factory> FactoryList = new List<Factory>();
        double Time;
        treasury Treasury1 = new treasury(0);
        List<Coord> Coords = new List<Coord>();
        
        
        public Form1()
        {
            InitializeComponent();

        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
           
            timer1.Interval = 100;
            timer1.Enabled = false;
             Time = 0;
            label1.Text = "0";

        }

        public void DrawFactory( int x, int y)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawRectangle(Pens.Purple, x, y, 7, 7);
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
            
            try
            {
                
                int numFactories = int.Parse(textBox1.Text);
                
                if (numFactories > 12 || numFactories < 5)
                    throw new System.ArgumentOutOfRangeException("Invalid number of factories");
               for(int i = 0; i<numFactories; i++)
                {
                    if ((i % 2) == 0)
                    {
                        Factory FToList = new Factory(pictureBox1.Width / 2 + (int)Math.Pow(-1, i) * pictureBox1.Width / (i + 7), pictureBox1.Height / 2 + (int)Math.Pow(-1, (i + 1)) * pictureBox1.Height / (i + 3), 20, 0);
                        FactoryList.Add(FToList);
                    }
                    // if ((i%10)==0)
                    //{
                    //    Factory FToList = new Factory(pictureBox1.Width / 2 + (int)Math.Pow(-1, i) * pictureBox1.Width / (i + 7), pictureBox1.Height / 2 + (int)Math.Pow(-1, i ) * pictureBox1.Height / (i + 4), 20, 0);
                    //    FactoryList.Add(FToList);
                    //}
                    else
                    {
                        Factory FToList = new Factory(pictureBox1.Width / 2 + (int)Math.Pow(-1, (i+1)) * pictureBox1.Width / (i + 2), pictureBox1.Height / 2 + (int)Math.Pow(-1, i) * pictureBox1.Height / (i + 4), 20, 0);
                        FactoryList.Add(FToList);
                    }
                    //FactoryList.Add(FToList);
                }
                foreach (Factory el in FactoryList)
                {

                    DrawFactory(el.x, el.y);
                }
                
            }
            catch(System.ArgumentOutOfRangeException a)
            {
                MessageBox.Show(a.Message);
            }
            catch(System.FormatException a)
            {
                MessageBox.Show(a.Message);
            }
           
            

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Factory el in FactoryList) //не работает, так как список пустой ( пикчербокс заполняется до нажатия кнопки)
            {
                e.Graphics.DrawRectangle(Pens.Purple, el.x, el.y, 7, 7);
            }
             //этот весь кусок кода( эта функция) пока что всего лишь проверка процесса рисования)

            using (var br = new SolidBrush(Color.FromArgb(10, 255, 0, 0))) // первое значение в скобках - степень заливки
            {
                e.Graphics.FillEllipse(br, new Rectangle(50, 50, 40, 40));

                e.Graphics.FillRectangle(br, 0, 0, 695, 563);
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time += timer1.Interval;
            double Days = Time / 100;
            label1.Text = Days.ToString();
            
            Step();
        }

        public void Step()
        {
            foreach(Factory el in FactoryList)
            {
                el.pollution++;
                if (el.pollution > 150 && Treasury1.money >= 30000)
                {
                    Treasury1.money = Treasury1.money - 30000;
                }
                if (el.pollution >150)
                {
                    Treasury1.money = Treasury1.money + el.pollution * 300; 
                }
                el.pollution++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
