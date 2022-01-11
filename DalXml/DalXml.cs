using DO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        #region singleton
        private static readonly IDal instance = new DalXml();

        public static IDal Instance { get { return instance; } }

        private DalXml()//costructor for dalObject
        {
            if (!File.Exists(xmlPath + parcelsPath))
                File.Create(xmlPath + parcelsPath);
            if (!File.Exists(xmlPath + stationsPath))
                File.Create(xmlPath + stationsPath);
            if (!File.Exists(xmlPath + CustomersPath))
                File.Create(xmlPath + CustomersPath);
            if (!File.Exists(xmlPath + DronesPath))
                File.Create(xmlPath + DronesPath);
            if (!File.Exists(xmlPath + DroneChargePath))
                File.Create(xmlPath + DroneChargePath);


        }

        #endregion

        #region config
        internal static int parcelNum = 1000000;
        internal static double ElectricityUsePerKmAvailable = 5;
        internal static double ElectricityUsePerKmLight = 7;
        internal static double ElectricityUsePerKmMedium = 8;
        internal static double ElectricityUsePerKmHeavy = 9;
        internal static double ElectricityChargePerHour = 20;
        #endregion

        private string xmlPath = @"C:\Users\yunia\source\repos\dotNet5782_4298_0184\xml\";
        private string parcelsPath = @"ParcelsXml.xml";
        private string stationsPath = @"StationsXml.xml";
        private string CustomersPath = @"CustomersXml.xml";
        private string DronesPath = @"DronesXml.xml";
        private string DroneChargePath = @"DCharge.xml";

        #region StationFuncs
        public void AddParcel(Parcel AddParcel)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);

            XElement parcelItem = new XElement("Parcel", new XElement("Id", ++parcelNum),
                                  new XElement("SenderId", AddParcel.SenderId),
                                  new XElement("ReciverId", AddParcel.ReciverId),
                                  new XElement("Weight", AddParcel.Weight),
                                  new XElement("Priority", AddParcel.Priority),
                                  new XElement("Requested", AddParcel.Requested),
                                  new XElement("Scheduled", AddParcel.Scheduled),
                                  new XElement("PickedUp", AddParcel.PickedUp),
                                  new XElement("Delivered", AddParcel.Delivered),
                                  new XElement("DroneId", AddParcel.DroneId));
            parcelElement.Add(parcelItem);

            XmlTools.SaveListToXMLElement(parcelElement, parcelsPath);
        }

        public void UpdateParcel(Parcel updateParcel)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);

            XElement parcelItem = (from par in parcelElement.Elements()
                                   where int.Parse(par.Element("Id").Value) == updateParcel.Id
                                   select par).FirstOrDefault();
            if (parcelItem != null)
            {
                parcelItem.Element("Id").Value = updateParcel.Id.ToString();
                parcelItem.Element("SenderId").Value = updateParcel.SenderId.ToString();
                parcelItem.Element("ReciverId").Value = updateParcel.ReciverId.ToString();
                parcelItem.Element("Weight").Value = updateParcel.Weight.ToString();
                parcelItem.Element("Priority").Value = updateParcel.Priority.ToString();
                parcelItem.Element("Reqested").Value = updateParcel.Requested.ToString();
                parcelItem.Element("Scheduled").Value = updateParcel.Scheduled.ToString();
                parcelItem.Element("PickedUp").Value = updateParcel.PickedUp.ToString();
                parcelItem.Element("Delivered").Value = updateParcel.Delivered.ToString();
                parcelItem.Element("DroneID").Value = updateParcel.DroneId.ToString();

                XmlTools.SaveListToXMLElement(parcelElement, parcelsPath);
            }
        }

        public Parcel GetParcel(int id)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);

            Parcel? p = (from par in parcelElement.Elements()
                         where int.Parse(par.Element("Id").Value) == id
                         select new Parcel()
                         {
                             Id = int.Parse(par.Element("Id").Value),
                             SenderId = int.Parse(par.Element("SenderId").Value),
                             ReciverId = int.Parse(par.Element("ReciverId").Value),
                             Weight = (WeightCategories)int.Parse(par.Element("Weight").Value),
                             Priority = (Priorities)int.Parse(par.Element("Priority").Value),
                             Requested = DateTime.Parse(par.Element("Reqested").Value),
                             Scheduled = DateTime.Parse(par.Element("Scheduled").Value),
                             PickedUp = DateTime.Parse(par.Element("PickedUp").Value),
                             Delivered = DateTime.Parse(par.Element("Delivered").Value),
                             DroneId = int.Parse(par.Element("DroneID").Value)

                         }).FirstOrDefault();
            if (p == null)
                throw new NotExistsException($"id: {id} not exists!!");
            return (Parcel)p;
        }

        public IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);
            return from par in parcelElement.Elements()
                   select new Parcel()
                   {
                       Id = int.Parse(par.Element("Id").Value),
                       SenderId = int.Parse(par.Element("SenderId").Value),
                       ReciverId = int.Parse(par.Element("ReciverId").Value),
                       Weight = (WeightCategories)int.Parse(par.Element("Weight").Value),
                       Priority = (Priorities)int.Parse(par.Element("Priority").Value),
                       Requested = DateTime.Parse(par.Element("Reqested").Value),
                       Scheduled = DateTime.Parse(par.Element("Scheduled").Value),
                       PickedUp = DateTime.Parse(par.Element("PickedUp").Value),
                       Delivered = DateTime.Parse(par.Element("Delivered").Value),
                       DroneId = int.Parse(par.Element("DroneID").Value)

                   };
        }

        public void DeleteParcel(Parcel deleteParcel)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);

            XElement parcelItem = (from par in parcelElement.Elements()
                                   where int.Parse(par.Element("Id").Value) == deleteParcel.Id
                                   select par).FirstOrDefault();
            if (parcelItem != null)
            {
                parcelItem.Remove();

                XmlTools.SaveListToXMLElement(parcelElement, parcelsPath);
            }
            else
                throw new NotExistsException($"id: {deleteParcel.Id} not exists!!");
        }
        #endregion

        #region StationFuncs
        public void AddStation(Station addStation)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (stations.Any(st => st.Id == addStation.Id))
            {
                throw new AlreadyExistsException($"id: {addStation.Id} already exists!!");
            }
            stations.Add(addStation);
            XmlTools.SaveListToXMLSerializer<Station>(stations, stationsPath);
        }

        public Station GetStation(int id)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (stations.Any(st => st.Id == id))
            {
                return stations.First(st => st.Id == id);
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        public IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
        }

        public void UpdateStation(Station updateStation)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            int indexOfStation = stations.IndexOf(stations.Find(st => st.Id == updateStation.Id));
            if (indexOfStation == -1)
            {
                throw new NotExistsException($"id: {updateStation.Id} not exists!!");
            }
            stations[indexOfStation] = updateStation;
        }

        public void DeleteStation(Station deleteStation)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);

            if (stations.Any(st => st.Id == st.Id))
            {
                stations.Remove(deleteStation);
                XmlTools.SaveListToXMLSerializer<Station>(stations, stationsPath);
            }
            throw new NotExistsException($"id: {deleteStation.Id} not exists!!");
        }

        #endregion

        #region CustomerFuncs
        public void AddCustomer(Customer addCustomer)
        {
            List<Customer> Customers = XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
            if (Customers.Any(st => st.Id == addCustomer.Id))
            {
                throw new AlreadyExistsException($"id: {addCustomer.Id} already exists!!");
            }
            Customers.Add(addCustomer);
            XmlTools.SaveListToXMLSerializer<Customer>(Customers, CustomersPath);
        }

        public Customer GetCustomer(int id)
        {
            List<Customer> Customers = XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
            if (Customers.Any(st => st.Id == id))
            {
                return Customers.First(st => st.Id == id);
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        public IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
        }

        public void UpdateCustomer(Customer updateCustomer)
        {
            List<Customer> Customers = XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
            int indexOfCustomer = Customers.IndexOf(Customers.Find(st => st.Id == updateCustomer.Id));
            if (indexOfCustomer == -1)
            {
                throw new NotExistsException($"id: {updateCustomer.Id} not exists!!");
            }
            Customers[indexOfCustomer] = updateCustomer;
        }

        public void DeleteCustomer(Customer deleteCustomer)
        {
            List<Customer> Customers = XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);

            if (Customers.Any(st => st.Id == st.Id))
            {
                Customers.Remove(deleteCustomer);
                XmlTools.SaveListToXMLSerializer<Customer>(Customers, CustomersPath);
            }
            throw new NotExistsException($"id: {deleteCustomer.Id} not exists!!");
        }

        #endregion

        #region DroneFuncs
        public void AddDrone(Drone addDrone)
        {
            List<Drone> Drones = XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            if (Drones.Any(st => st.Id == addDrone.Id))
            {
                throw new AlreadyExistsException($"id: {addDrone.Id} already exists!!");
            }
            Drones.Add(addDrone);
            XmlTools.SaveListToXMLSerializer(Drones, DronesPath);
        }

        public Drone GetDrone(int id)
        {
            List<Drone> Drones = XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            if (Drones.Any(st => st.Id == id))
            {
                return Drones.First(st => st.Id == id);
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        public IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
        }

        public void UpdateDrone(Drone updateDrone)
        {
            List<Drone> Drones = XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            int indexOfDrone = Drones.IndexOf(Drones.Find(st => st.Id == updateDrone.Id));
            if (indexOfDrone == -1)
            {
                throw new NotExistsException($"id: {updateDrone.Id} not exists!!");
            }
            Drones[indexOfDrone] = updateDrone;
        }

        public void DeleteDrone(Drone deleteDrone)
        {
            List<Drone> Drones = XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            if (Drones.Any(st => st.Id == st.Id))
            {
                Drones.Remove(deleteDrone);
                XmlTools.SaveListToXMLSerializer<Drone>(Drones, DronesPath);
            }
            throw new NotExistsException($"id: {deleteDrone.Id} not exists!!");
        }

        #endregion


        public void AddDroneCharge(DroneCharge addDroneCharge)
        {
            
        }

        public void DeleteDroneCharge(DroneCharge deleteDroneCharge)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            throw new NotImplementedException();
        }

        public double GetElectricityUsePerKmAvailable()
        {
            return ElectricityUsePerKmAvailable;
        }

        public double GetElectricityUsePerKmLight()
        {
            return ElectricityUsePerKmLight;
        }

        public double GetElectricityUsePerKmMedium()
        {
            return ElectricityUsePerKmMedium;
        }

        public double GetElectricityUsePerKmHeavy()
        {
            return ElectricityUsePerKmHeavy;
        }

        public double GetElectricityChargePerHour()
        {
            return ElectricityChargePerHour;
        }

    }
}
