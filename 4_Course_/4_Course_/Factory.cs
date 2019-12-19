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
        public int radius;

        public int pollution;

        public Factory(int X, int Y, int R, int POLLUTION)
        {
            x = X;
            y = Y;
            radius = R;

            pollution = POLLUTION;
        }
    }
}
