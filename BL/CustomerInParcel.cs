using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
   public class CustomerInParcel 
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public override string ToString()
        {
            return $" Customer Id:      {Id}\n" +
                   $" Name:             {Name}\n";
        }
    }
}
