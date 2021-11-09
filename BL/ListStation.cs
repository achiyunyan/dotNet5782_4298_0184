using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ListStation
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int FreeChargeSlots { set; get; }
        public int BusyChargeSlots { set; get; }
        public override string ToString()
        {
            return $" Station Id:       {Id}\n" +
                   $" Name:             {Name}\n" +
                   $" Free Charge slots: {FreeChargeSlots}\n" +
                   $" Busy Charge slots: {BusyChargeSlots}\n";
                  
        }
    }
}
