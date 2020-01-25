using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Course_
{
    public class Factory
    {
        public int x;
        public int y;

        public int days = 0;//Кол-во дней превышающих предел загрязнений
        public int daysoutofservice = 0;//Сколько дней фабрика будет не работать
        public bool isworking = true;
        public double pollution;
        public int filterday = 7;
        public int amountoffilters = 0;
        public Factory(int X, int Y, double POLLUTION)
        {
            x = X;
            y = Y;


            pollution = POLLUTION;
        }

       public double AddingPollution()
        {
            if (amountoffilters == 0) //если нет фильтров
                return 1;
            else //если есть фильтры
                return (Math.Pow((0.93), amountoffilters));
        }
    }
}