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
    /// <summary>
    /// Dal class which saves the data to xml files
    /// Implement of the IDal interface
    /// </summary>
    internal sealed class DalXml : IDal
    {
        #region singleton
        private static readonly IDal instance = new DalXml();

        public static IDal Instance { get { return instance; } }

        private DalXml()
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
        private static string solutionDirectory;

        //Relevant storage paths of the xml files
        private string xmlPath ;
        private string parcelsPath = @"ParcelsXml.xml";
        private string stationsPath = @"StationsXml.xml";
        private string CustomersPath = @"CustomersXml.xml";
        private string DronesPath = @"DronesXml.xml";
        private string DroneChargePath = @"DCharge.xml";
        private string ConfigPath = @"dal-config.Xml";

        private void Config()
        {
            solutionDirectory= Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName).FullName;
            xmlPath = solutionDirectory + @"\xml\";
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
            XmlTools.SaveListToXMLSerializer(new List<DroneCharge>(), DroneChargePath);//clear the charges
        }
        #endregion

        #region ParcelFuncs
        /// <summary>
        /// Add the specified parcel to the parcels' xml file
        /// </summary>
        /// <param name="AddParcel"></param>
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

        /// <summary>
        ///  Update the specified parcel to the parcels' xml file
        /// </summary>
        /// <param name="updateParcel"></param>
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

        /// <summary>
        /// Return a specific parcel by its Id if exists,else throw an exeption
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return the parcels in the database,according to rhe predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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
            return predicate==null?p:p.Where(predicate);
        }

        /// <summary>
        /// Delete a parcel,or throw exeption if not found
        /// </summary>
        /// <param name="deleteParcel"></param>
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
        /// <summary>
        /// Add the specified station to the stations' xml file
        /// </summary>
        /// <param name="addStation"></param>
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

        /// <summary>
        /// Return a specified station by id, if not found throw exeption
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Retutn the stations in the database according to the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Station>(stationsPath);
        }

        /// <summary>
        /// Update the specified station to the database
        /// </summary>
        /// <param name="updateStation"></param>
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
            XmlTools.SaveListToXMLSerializer(stations,stationsPath);
        }

        /// <summary>
        /// Delete the specified station from the database, if not found throw exeption
        /// </summary>
        /// <param name="deleteStation"></param>
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
        /// <summary>
        /// Add the specified customer to the database
        /// throw exeption if already exists
        /// </summary>
        /// <param name="addCustomer"></param>
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

        /// <summary>
        /// Return a customer by Id, exeption if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return the customers in the database by the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Customer>(CustomersPath);
        }

        /// <summary>
        /// Update the specified customer to the database
        /// </summary>
        /// <param name="updateCustomer"></param>
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
            XmlTools.SaveListToXMLSerializer<Customer>(Customers, CustomersPath);
        }

        /// <summary>
        /// Delete the specified customer from the database 
        /// </summary>
        /// <param name="deleteCustomer"></param>
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

        /// <summary>
        /// Add a drone to the database 
        /// Throw an exeption if already exists
        /// </summary>
        /// <param name="addDrone"></param>
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

        /// <summary>
        /// Return a specified drone its id 
        /// throw exeption if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return the drones in the database by a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null)
        {
            return XmlTools.LoadListFromXMLSerializer<Drone>(DronesPath);
        }

        /// <summary>
        /// Update the specified drone to the databasde
        /// Trow exeption if not exists
        /// </summary>
        /// <param name="updateDrone"></param>
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
            XmlTools.SaveListToXMLSerializer(Drones, DronesPath);
        }

        /// <summary>
        /// Delete a drone from the database
        /// Trow exeption if not exists
        /// </summary>
        /// <param name="deleteDrone"></param>
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
        /// <summary>
        /// Add the given droneCharge instance to the database
        /// Throw exeption if already exists
        /// </summary>
        /// <param name="addDroneCharge"></param>
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

        /// <summary>
        /// Delete the given instance of a droneCharge from the database
        /// Trow exeption if not exists
        /// </summary>
        /// <param name="deleteDroneCharge"></param>
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
        
        /// <summary>
        /// Return the instances of the droneCharges in the database by a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneChargesList(Func<DroneCharge, bool> predicate = null)
        {
            return predicate==null? XmlTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath):XmlTools.LoadListFromXMLSerializer<DroneCharge>(DroneChargePath).Where(predicate);
        }
        #endregion

        #region Electricity
        //'Get' functions for the Electricity constants

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
