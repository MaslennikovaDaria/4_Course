using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Course_
{
    class InfoOut
    {

        StreamWriter sw = new StreamWriter("output.txt");
        //Выводит информацию в начале дня 
        public void Output(string day, Cars cars, WeatherCount weather, treasury treasury, List<Factory> FactoryList)
        {
            sw.WriteLine("День: " + day);
            sw.WriteLine("Загрязнение от машин: " + Convert.ToString(cars.AllPollution));
            if (weather.BeforeWindyDayCount == 3)
                sw.WriteLine("Ветренно");
            else sw.WriteLine("Безветренно");
            if (weather.DaysBeforeRainCount == 5)
                sw.WriteLine("Дождливо");
            else sw.WriteLine("Солнечно");
            sw.WriteLine("В казне: " + Convert.ToString(treasury.money));
            int i = 0;
            foreach (Factory factory in FactoryList)
            {
                sw.WriteLine("Фабрика " + Convert.ToString(i+1) + "загрязнение " + factory.pollution);
                if (factory.amountoffilters > 0)
                    sw.WriteLine("Установлено фильтров: " + Convert.ToString(factory.amountoffilters));
                i++;
            }
            sw.WriteLine();
        }
    }
}