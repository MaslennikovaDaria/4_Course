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

        public int days=0;//Кол-во дней превышающих предел загрязнений
        public int daysoutofservice = 0;//Сколько дней фабрика будет не работать
        public bool isworking = true;
        public int pollution;
        public int filterday=7;
        public Factory(int X, int Y,  int POLLUTION)
        {
            x = X;
            y = Y;
            

            pollution = POLLUTION;
        }

        void ChangePollution ()
        {

        }
    }
}
