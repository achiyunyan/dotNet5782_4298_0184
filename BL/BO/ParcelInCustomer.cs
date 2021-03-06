using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ParcelInCustomer
    {
        public int Id { set; get; }
        public WeightCategory WeightCategory { set; get; }
        public Priority Priority { set; get; }
        public ParcelState State { set; get; }
        public CustomerInParcel Customer { set; get; }
        public override string ToString()
        {
            return $"    Parcel Id:        {Id}\n" +
                   $"    Weight Category:  {WeightCategory}\n" +
                   $"    Priority:         {Priority}\n" +
                   $"    State:            {State}\n" +
                   $"    Customer:\n" + Customer.ToString();
        }
    }
}
