using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_Course_
{
    public class Cars
    {
        public int AmountOfCars; 
        public bool specialmode = false;
        public double AllPollution = 0;

        
        public Cars (int Amount)
        {
            AmountOfCars = Amount;
        }

        public double CountingAddingCarsPollution(int AmountOfCars, bool SpecialMode)
        {
            double Add;
            Add = AmountOfCars/20000; //
            if (specialmode == true)
                Add = Add * 0.5;  //если ввели спец режим, машин вдвое меньше
            else
                Add = Add * 0.75; // т.к. в сутки условно ходит 75% машин
            return Add;
        }
        public double WeatherCarsConnection(int Rain, int Wind, double CarPollution)
        {
            if (Rain == 5)
                CarPollution = CarPollution * 0.9;
            if (Wind == 3)
                CarPollution = CarPollution * 0.9;
            return CarPollution;

        }
    }
}
