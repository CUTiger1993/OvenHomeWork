using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp5
{

    public delegate void OverHeatHandler();
    public class Oven
    {
        public event OverHeatHandler OverHeat;

        private bool _ovenIsWorking = false;
        public int MaxOvenTemperature
        {
            get;
            set;
        }


        public int DefaultTemperature
        {
            get
            {
                return 37;
            }
           
        }

        public int PresentTemperature
        {
            get;
            set;
        }

        public Oven(int value)
        {
            MaxOvenTemperature = value;
        }

        public void OvenStart()
        {

            PresentTemperature = DefaultTemperature;
            _ovenIsWorking = true;
            while((PresentTemperature < MaxOvenTemperature) && _ovenIsWorking)
            {
                Thread.Sleep(1000);//Thread sealed class
                PresentTemperature++;
                Console.WriteLine(PresentTemperature);                                                         
            }
            if(PresentTemperature == MaxOvenTemperature)
            {
                OverHeat();
            }

        }

        public void OvenStop()
        {
            _ovenIsWorking = false;
            Console.WriteLine("Oven has stopped!");
            while ((PresentTemperature >= DefaultTemperature) && !_ovenIsWorking)
            {
                Thread.Sleep(1000);//Thread sealed class
                PresentTemperature--;
                Console.WriteLine(PresentTemperature);
            }
        }


    }

    public class OverHeatSolving
    {
        private Oven oven1;
        public OverHeatSolving(Oven oven)
        {
            oven.OverHeat += _overHeatSolving;
            oven1 = oven;
        }
        private void _overHeatSolving()
        {
            Console.WriteLine("The oven is overheat, please stop the oven immediately!");
            oven1.OvenStop();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Oven oven = new Oven(40);
            OverHeatSolving overheatsolving = new OverHeatSolving(oven);
            oven.OvenStart();
            
            


            
        }
    }
}
