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
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace Dal
{
    internal sealed class DalXml : IDal
    {
        #region singleton
        private static readonly IDal instance = new DalXml();

        public static IDal Instance { get { return instance; } }

        private DalXml()//costructor for dalObject
        {
            Config();
        }

        #endregion

        #region config
        internal static int parcelNum;
        internal static double ElectricityUsePerKmAvailable;
        internal static double ElectricityUsePerKmLight;
        internal static double ElectricityUsePerKmMedium;
        internal static double ElectricityUsePerKmHeavy;
        internal static double ElectricityChargePerSec;
        private void Config()
        {
            #region files Create
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
            #endregion
            if (File.Exists(xmlPath + ConfigPath))
            {
                XElement Electricity = XmlTools.LoadListFromXMLElement(ConfigPath).Element("dalXmlData");
                ElectricityUsePerKmAvailable = double.Parse(Electricity.Element("ElectricityUsePerKmAvailable").Value);
                ElectricityUsePerKmLight = double.Parse(Electricity.Element("ElectricityUsePerKmLight").Value);
                ElectricityUsePerKmMedium = double.Parse(Electricity.Element("ElectricityUsePerKmMedium").Value);
                ElectricityUsePerKmHeavy = double.Parse(Electricity.Element("ElectricityUsePerKmHeavy").Value);
                ElectricityChargePerSec = double.Parse(Electricity.Element("ElectricityChargePerSec").Value);
                parcelNum = int.Parse(Electricity.Element("parcelNum").Value);
            }
        }
        #endregion

        private string xmlPath = @"C:\Users\yunia\source\repos\dotNet5782_4298_0184\xml\";
        private string parcelsPath = @"ParcelsXml.xml";
        private string stationsPath = @"StationsXml.xml";
        private string CustomersPath = @"CustomersXml.xml";
        private string DronesPath = @"DronesXml.xml";
        private string DroneChargePath = @"DCharge.xml";
        private string ConfigPath = @"dal-config.Xml";

        #region ParcelFuncs
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel AddParcel)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);

            XElement parcelItem = new XElement("Parcel", new XElement("Id", parcelNum++),
                                  new XElement("SenderId", AddParcel.SenderId),
                                  new XElement("ReciverId", AddParcel.ReciverId),
                                  new XElement("Weight", (int)AddParcel.Weight),
                                  new XElement("Priority", (int)AddParcel.Priority),
                                  new XElement("Requested", AddParcel.Requested),
                                  new XElement("Scheduled", AddParcel.Scheduled),
                                  new XElement("PickedUp", AddParcel.PickedUp),
                                  new XElement("Delivered", AddParcel.Delivered),
                                  new XElement("DroneId", AddParcel.DroneId));
            parcelElement.Add(parcelItem);

            XmlTools.SaveListToXMLElement(parcelElement, parcelsPath);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
                parcelItem.Element("Weight").Value = ((int)updateParcel.Weight).ToString();
                parcelItem.Element("Priority").Value = ((int)updateParcel.Priority).ToString();
                parcelItem.Element("Requested").Value = updateParcel.Requested.ToString();
                parcelItem.Element("Scheduled").Value = updateParcel.Scheduled.ToString();
                parcelItem.Element("PickedUp").Value = updateParcel.PickedUp.ToString();
                parcelItem.Element("Delivered").Value = updateParcel.Delivered.ToString();
                parcelItem.Element("DroneId").Value = updateParcel.DroneId.ToString();

                XmlTools.SaveListToXMLElement(parcelElement, parcelsPath);
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);

            Parcel p = (from par in parcelElement.Elements()
                        where int.Parse(par.Element("Id").Value) == id
                        select new Parcel()
                        {
                            Id = int.Parse(par.Element("Id").Value),
                            SenderId = int.Parse(par.Element("SenderId").Value),
                            ReciverId = int.Parse(par.Element("ReciverId").Value),
                            Weight = (WeightCategories)int.Parse(par.Element("Weight").Value),
                            Requested = par.Element("Requested").Value == "" ? null : DateTime.Parse(par.Element("Requested").Value),
                            Scheduled = par.Element("Scheduled").Value == "" ? null : DateTime.Parse(par.Element("Scheduled").Value),
                            PickedUp = par.Element("PickedUp").Value == "" ? null : DateTime.Parse(par.Element("PickedUp").Value),
                            Delivered = par.Element("Delivered").Value == "" ? null : DateTime.Parse(par.Element("Delivered").Value),
                            DroneId = int.Parse(par.Element("DroneId").Value)

                        }).FirstOrDefault();
            if (p.Id == 0)//so it is default
                throw new NotExistsException($"id: {id} not exists!!");
            return p;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null)
        {
            XElement parcelElement = XmlTools.LoadListFromXMLElement(parcelsPath);
            if (parcelElement.Elements().Count() == 0)
                return new List<Parcel>();
            List<Parcel> p = new List<Parcel>();
            foreach (var par in parcelElement.Elements())
            {
                var x = par.Element("Scheduled").Value;
                p.Add(new Parcel()
                {
                    Id = int.Parse(par.Element("Id").Value),
                    SenderId = int.Parse(par.Element("SenderId").Value),
                    ReciverId = int.Parse(par.Element("ReciverId").Value),
                    Weight = (WeightCategories)int.Parse(par.Element("Weight").Value),
                    Priority = (Priorities)int.Parse(par.Element("Priority").Value),
                    Requested = par.Element("Requested").Value == "" ? null : DateTime.Parse(par.Element("Requested").Value),
                    Scheduled = par.Element("Scheduled").Value == "" ? null : DateTime.Parse(par.Element("Scheduled").Value),
                    PickedUp = par.Element("PickedUp").Value == "" ? null : DateTime.Parse(par.Element("PickedUp").Value),
                    Delivered = par.Element("Delivered").Value == "" ? null : DateTime.Parse(par.Element("Delivered").Value),
                    DroneId = int.Parse(par.Element("DroneId").Value)
                });
            }
            return p;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (stations.Any(st => st.Id == id))
            {
                return stations.First(st => st.Id == id);
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station deleteStation)
        {
            List<Station> stations = XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);

            if (stations.Any(st => st.Id == st.Id))
            {
                stations.Remove(deleteStation);
                XmlTools.SaveListToXMLSerializer<Station>(stations, stationsPath);
            }
            else
                throw new NotExistsException($"id: {deleteStation.Id} not exists!!");
        }

        #endregion

        #region CustomerFuncs
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            List<Customer> Customers = XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
            if (Customers.Any(st => st.Id == id))
            {
                return Customers.First(st => st.Id == id);
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer deleteCustomer)
        {
            List<Customer> Customers = XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);

            if (Customers.Any(st => st.Id == st.Id))
            {
                Customers.Remove(deleteCustomer);
                XmlTools.SaveListToXMLSerializer<Customer>(Customers, CustomersPath);
            }
            else
                throw new NotExistsException($"id: {deleteCustomer.Id} not exists!!");
        }

        #endregion

        #region DroneFuncs
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            List<Drone> Drones = XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
            if (Drones.Any(st => st.Id == id))
            {
                return Drones.First(st => st.Id == id);
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone deleteDrone)
        {
            List<Drone> Drones = XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);

            if (Drones.Any(st => st.Id == st.Id))
            {
                Drones.Remove(deleteDrone);
                XmlTools.SaveListToXMLSerializer<Drone>(Drones, DronesPath);
            }
            else
                throw new NotExistsException($"id: {deleteDrone.Id} not exists!!");
        }

        #endregion

        #region DroneCharge
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge addDroneCharge)
        {
            List<DroneCharge> DC = XmlTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);
            if (DC.Any(DC => DC.DroneId == addDroneCharge.DroneId))
            {
                throw new AlreadyExistsException($"id: {addDroneCharge.DroneId} already exists!!");
            }
            DC.Add(addDroneCharge);
            XmlTools.SaveListToXMLSerializer<DroneCharge>(DC, DroneChargePath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(DroneCharge deleteDroneCharge)
        {
            List<DroneCharge> DC = XmlTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);

            if (DC.Any(st => st.DroneId == st.DroneId))
            {
                DC.Remove(deleteDroneCharge);
                XmlTools.SaveListToXMLSerializer<DroneCharge>(DC, DroneChargePath);
            }
            else
                throw new NotExistsException($"id: {deleteDroneCharge.DroneId} not exists!!");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneCharges()
        {
            return XmlTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath);
        }
        #endregion

        #region Electricity
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

        public double GetElectricityChargePerSec()
        {
            return ElectricityChargePerSec;
        }
        #endregion

    }
}
