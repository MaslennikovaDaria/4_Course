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
        WeatherCount Weather = new WeatherCount();
        List<int> Xs = new List<int>();
        List<int> Ys = new List<int>();
        InfoOut infoOut=new InfoOut();
        Cars cars;
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

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Interval = 100;
            timer1.Enabled = false;
            Time = 0;
            label1.Text = "0";

        }

        private double AddingPollution( int amountoffilters) //метод, рассчитывающий прирост загрязнения завода за шаг в зависимости от количества фильтров
        {
            if (amountoffilters == 0)
                return 1;
            else 
                return (Math.Pow((93/100) , amountoffilters));
        }

        private void DrawFactory(int x, int y)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            gr.DrawRectangle(Pens.Black, x, y, 10, 10);
        }

        private void DrawPollutionRad(int x, int y, double pollution, double carspol) //загрязнение
        {
            
            int red = 0;
            int green = 0;
            int blue = 0;
            if (pollution + carspol >= 0 && pollution + carspol < 51) // должен быть зеленый( 0, 255, 0)
            {
                //red = 34;
                green = 255;
                //blue = 34;
            }
            if (pollution + carspol >= 51 && pollution + carspol < 102)
            {
                green = 255;
                red = 255;
            }
            if (pollution + carspol >= 102 && pollution + carspol < 153)
            {
                green = 127;
                red = 255;
            }
            if (pollution + carspol >= 153 && pollution + carspol < 204)
            {

                red = 255;
            }
            if (pollution + carspol >= 204)
            {
                red = 139;
                green = 34;
                blue = 82;
            }
            Graphics gr = pictureBox1.CreateGraphics();
            //using (var br = new SolidBrush(Color.FromArgb(pollution, 255, 0, 0))) // первое значение в скобках - степень заливки
            using (var br = new SolidBrush(Color.FromArgb(255, red, green, blue)))
            {
                gr.FillEllipse(br, new Rectangle(x-35, y-35, 80, 80));

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
                for (int i = 0; i < numFactories; i++)
                {
                    
                    Factory FToList = new Factory(Xs.ElementAt(i), Ys.ElementAt(i), 0);

                    //Factory FToList = new Factory(pictureBox1.Width / 2 + (int)Math.Pow(-1, i) * pictureBox1.Width / (i + 7), pictureBox1.Height / 2 + (int)Math.Pow(-1, (i + 1)) * pictureBox1.Height / (i + 3), 20, 0);
                    FactoryList.Add(FToList);

                }
                foreach (Factory el in FactoryList)
                {

                    DrawFactory(el.x, el.y);
                    //DrawPollutionRad(el.x, el.y, el.pollution);
                }

            }
            catch (System.ArgumentOutOfRangeException a)
            {
                MessageBox.Show(a.Message);
            }
            catch (System.FormatException a)
            {
                MessageBox.Show(a.Message);
            }


            try
            {
               
                int numCars = int.Parse(textBox2.Text)*1000;
                if (numCars > 90000 || numCars < 30000)
                    throw new System.ArgumentOutOfRangeException("Invalid number of cars");
                 cars = new Cars(numCars);
                
            }
            catch (System.ArgumentOutOfRangeException a)
            {
                MessageBox.Show(a.Message);
            }
            catch (System.FormatException a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) //НЕ НУЖНО!
        {
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Time += timer1.Interval;
            double Days = Time / 100;
            label1.Text = Days.ToString();
            infoOut.Output(Days.ToString(),cars,Weather,Treasury1,FactoryList);
            Step();
        }


        public void CarsPollutionColour(double pollution)
        {
            Graphics gr = pictureBox1.CreateGraphics();
            int red = 0;
            int green = 0;
            int blue = 0;
            if (pollution >= 0 && pollution < 51) // должен быть зеленый( 0, 255, 0)
            {
                //red = 34;
                green = 255;
                //blue = 34;
            }
            if (pollution >= 51 && pollution < 102)
            {
                green = 255;
                red = 255;
            }
            if (pollution >= 102 && pollution < 153)
            {
                green = 127;
                red = 255;
            }
            if (pollution >= 153 && pollution < 204)
            {

                red = 255;
            }
            if (pollution >= 204)
            {
                red = 139;
                green = 34;
                blue = 82;
            }
            using (var br = new SolidBrush(Color.FromArgb(255, red, green, blue)))
            {
                gr.FillRectangle(br, new Rectangle(0, 0, 695, 563));
            }

        }
        public void Step()
        {
            int k = 0;
            // Graphics gr = pictureBox1.CreateGraphics();
            //gr.Clear(Color.White);
            cars.AllPollution = cars.WeatherCarsConnection(Weather.DaysBeforeRainCount, Weather.BeforeWindyDayCount, cars.AllPollution);
            CarsPollutionColour(cars.AllPollution);
           
            if (Weather.BeforeWindyDayCount ==3)
            {
                Weather.BeforeWindyDayCount = 0;
            }
            if (Weather.DaysBeforeRainCount ==5)
            {
                Weather.DaysBeforeRainCount = 0;
            }
            foreach (Factory el in FactoryList)
            {
                k++;
                if (el.isworking)
                {

                    DrawPollutionRad(el.x, el.y, el.pollution, cars.AllPollution);
                    DrawFactory(el.x, el.y);
                    if (el.pollution > 204)//Если загрязнение превышает каждый день допустимую величину увеличиваем количество дней
                        el.days++;
                    else el.days = 0;
                    if (el.pollution > 170 && Treasury1.money >= 30000)
                    {
                        Treasury1.money = Treasury1.money - 30000;
                        if (el.filterday == 0)
                        {  
                            el.filterday = 7;
                            //el.isthereafilter = true;
                        }
                        el.filterday--;
                    }
                    if (el.pollution > 170)
                    {
                        Treasury1.money = Treasury1.money + el.pollution * 100;
                    }

                    
                    //DrawPollutionRad(el.x, el.y, el.pollution);
                    //DrawFactory(el.x, el.y);
                    if (el.days == 7)
                    {
                        el.isworking = false;
                        el.daysoutofservice = 9;
                    }
                    
                        el.pollution = el.pollution+AddingPollution(el.amountoffilters);
                }
                else el.daysoutofservice--;
                if (el.daysoutofservice == -1)
                {
                    el.isworking = true;
                    el.pollution = 0;
                    el.daysoutofservice = 0;
                }

                
            }

            Weather.BeforeWindyDayCount++;
            Weather.BeforeWindyDayCount++;
            if (cars.AllPollution > 200)
                cars.specialmode = true;
            if (cars.AllPollution < 50)
                cars.specialmode = false;
            cars.AllPollution = cars.AllPollution + cars.CountingAddingCarsPollution(cars.AmountOfCars, cars.specialmode);
                   //DrawNewStep();
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