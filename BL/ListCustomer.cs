using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class ListCustomer
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Phone { set; get; }
        public int SentSuppliedParcels { set; get; }
        public int SentNotSuppliedParcels { set; get; }
        public int ReceivedParcels { set; get; }
        public int OnTheWayParcels { set; get; }
        public override string ToString()
        {
            return $" Customer Id:      {Id}\n" +
                   $" Name:             {Name}\n" +
                   $" Phone number:     {Phone}\n" +
                   $" Number of sent and supplied parcels: {SentSuppliedParcels}\n" +
                   $" Number of sent but not supplied parcels: {SentNotSuppliedParcels}\n" +
                   $" Number of received parcels: {ReceivedParcels}\n" +
                   $" Number of parcels on the way: {OnTheWayParcels}\n";
        }
    }
}
