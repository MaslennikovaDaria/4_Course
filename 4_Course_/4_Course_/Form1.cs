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

        List<int> Xs = new List<int>();
        List<int> Ys = new List<int>();
        
        
        public Form1()
        {
            InitializeComponent();

        }

        public void AddCoord(int amount)
        {

            Random random = new Random();
            for (int i = 0; i < amount; i++)
            {
                int a = random.Next(0, 650);
                int b = random.Next(0, 500);
                Xs.Add(a);
                Ys.Add(b);
            }
            //for(int i = 0; i< amount; i++)
            //{
            //    Xs.Add((i+1) * 10);
            //    Ys.Add((i + 2) * 10 + 2);
                
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            timer1.Interval = 100;
            timer1.Enabled = false;
             Time = 0;
            label1.Text = "0";

        }

       

        private void DrawFactory( int x, int y)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawRectangle(Pens.Purple, x, y, 7, 7);
        }

        private void DrawPollutionRad ( int x, int y, int pollution) //загрязнение
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            if (pollution > 0 && pollution < 85)
            {
                red = 34;
                green = 139;
                blue = 34;
            }
            if (pollution >= 85 && pollution < 170)
            {
                green = 255;
                red = 255;
            }
            else { red = 255; }
            Graphics gr = pictureBox1.CreateGraphics();
            //using (var br = new SolidBrush(Color.FromArgb(pollution, 255, 0, 0))) // первое значение в скобках - степень заливки
            using (var br = new SolidBrush(Color.FromArgb(255, red, green, blue)))
            {
                gr.FillEllipse(br, new Rectangle(x, y, 20, 20));

                //e.Graphics.FillRectangle(br, 0, 0, 695, 563);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
            try
            {
                
                int numFactories = int.Parse(textBox1.Text);
                AddCoord(numFactories);
                if (numFactories > 12 || numFactories < 5)
                    throw new System.ArgumentOutOfRangeException("Invalid number of factories");
               for(int i = 0; i<numFactories; i++)
                {
                    Factory FToList = new Factory(Xs.ElementAt(i), Ys.ElementAt(i),  0);

                    //Factory FToList = new Factory(pictureBox1.Width / 2 + (int)Math.Pow(-1, i) * pictureBox1.Width / (i + 7), pictureBox1.Height / 2 + (int)Math.Pow(-1, (i + 1)) * pictureBox1.Height / (i + 3), 20, 0);
                    FactoryList.Add(FToList);
                   
                }
                foreach (Factory el in FactoryList)
                {

                    DrawFactory(el.x, el.y);
                    //DrawPollutionRad(el.x, el.y, el.pollution);
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

        private void pictureBox1_Paint(object sender, PaintEventArgs e) //НЕ НУЖНО!
        {
            //foreach (Factory el in FactoryList) //не работает, так как список пустой ( пикчербокс заполняется до нажатия кнопки)
            //{
            //    e.Graphics.DrawRectangle(Pens.Purple, el.x, el.y, 7, 7);
            //}
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

        private void DrawNewStep()
        {
            // Clear screen with teal background.
            Graphics gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
            foreach (Factory el in FactoryList)
            {
                DrawPollutionRad(el.x, el.y, el.pollution);
                DrawFactory(el.x, el.y);
            }
        }

        
        public void Step()
        {
            
            foreach (Factory el in FactoryList)
            {
                
                //DrawFactory(el.x, el.y);
                //DrawPollutionRad(el.x, el.y, el.pollution);
                // el.pollution++;
                
                if (el.pollution > 170 && Treasury1.money >= 30000)
                {
                    Treasury1.money = Treasury1.money - 30000;
                    el.pollution = el.pollution*93/100;
                }
                if (el.pollution >170)
                {
                    Treasury1.money = Treasury1.money + el.pollution * 300; 
                }
              
                el.pollution++;
               
            }
            
                DrawNewStep();
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
