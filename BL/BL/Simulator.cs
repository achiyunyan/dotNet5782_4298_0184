using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using static BL.BL;


namespace BL
{
    internal class Simulator
    {
        private int DELAY = 500; // half a second
        private double SPEED = 1;// km/s
        internal Simulator(int id, Action update, Func<bool> checkStop, BL bL)
        {
            while(checkStop())
            {

            }
        }
    }
}
