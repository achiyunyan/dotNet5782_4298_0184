using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DO;
using DalApi;
using DS;
using System.Runtime.CompilerServices;

using System.Xml.Linq;
using System.Xml.Serialization;



namespace Dal
{
    internal sealed class DalObject : IDal
    {
        #region singleton
        private static readonly IDal instance = new DalObject();

        public static IDal Instance { get { return instance; } }

        int parcelNum = 1000000;

        private DalObject()//costructor for dalObject
        {
            DataSource.Config.Initialize();

            // not in use
            #region write to xml
            /*
            XmlTools.SaveListToXMLSerializer<Customer>(DataSource.Customers, @"CustomersXml.xml");
            XmlTools.SaveListToXMLSerializer<Drone>(DataSource.Drones, @"DronesXml.xml");
            XmlTools.SaveListToXMLSerializer<Station>(DataSource.Stations, @"StationsXml.xml");
            XmlTools.SaveListToXMLSerializer<DroneCharge>(DataSource.DroneCharges, @"DCharge.xml");

            XElement parcelElement = XmlTools.LoadListFromXMLElement(@"ParcelsXml.xml");

            foreach (Parcel AddParcel in DataSource.Parcels)
            {
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
            }

            XmlTools.SaveListToXMLElement(parcelElement, @"ParcelsXml.xml");
            */
            #endregion 
            
        }

        #endregion

        #region Add
        /// <summary>
        /// adds station to the stations' list
        /// </summary>
        /// <param name="addStation"/>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddStation(Station addStation)
        {
            if (DataSource.Stations.Any(st => st.Id == addStation.Id))
            {
                throw new AlreadyExistsException($"id: {addStation.Id} already exists!!");
            }
            DataSource.Stations.Add(addStation);
        }

        /// <summary>
        /// Adds drone to the drones' list 
        /// </summary>
        /// <param name="AddDrone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone addDrone)
        {
            if (DataSource.Drones.Any(st => st.Id == addDrone.Id))
            {
                throw new AlreadyExistsException($"id: {addDrone.Id} already exists!!");
            }
            DataSource.Drones.Add(addDrone);
        }

        /// <summary>
        /// Adds customer to the customers' list
        /// </summary>
        /// <param name="AddCustomer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer addCustomer)
        {
            if (DataSource.Customers.Any(st => st.Id == addCustomer.Id))
            {
                throw new AlreadyExistsException($"id: {addCustomer.Id} already exists!!");
            }
            DataSource.Customers.Add(addCustomer);
        }

        /// <summary>
        /// Adds parcel to the parcels' list
        /// </summary>
        /// <param name="AddParcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddParcel(Parcel addParcel)
        {
            addParcel.Id = ++DataSource.Config.parcelNum;
            DataSource.Parcels.Add(addParcel);
        }

        /// <summary>
        /// Adds drone charge to drone charges' list
        /// </summary>
        /// <param name="addDroneCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge addDroneCharge)
        {
            DataSource.DroneCharges.Add(addDroneCharge);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deletes a station
        /// </summary>
        /// <param name="deleteStation"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteStation(Station deleteStation)
        {
            if (!DataSource.Stations.Remove(deleteStation))
            {
                throw new NotExistsException($"id: {deleteStation.Id} not exists!!");
            }
        }
        
        /// <summary>
        /// Deletes a drone
        /// </summary>
        /// <param name="deleteDrone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(Drone deleteDrone)
        {
            if (!DataSource.Drones.Remove(deleteDrone))
            {
                throw new NotExistsException($"id: {deleteDrone.Id} not exists!!");
            }
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="deleteCustomer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteCustomer(Customer deleteCustomer)
        {
            if (!DataSource.Customers.Remove(deleteCustomer))
            {
                throw new NotExistsException($"id: {deleteCustomer.Id} not exists!!");
            }
        }

        /// <summary>
        /// Deletes a parcel
        /// </summary>
        /// <param name="deleteParcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(Parcel deleteParcel)
        {
            if (!DataSource.Parcels.Remove(deleteParcel))
            {
                throw new NotExistsException($"id: {deleteParcel.Id} not exists!!");
            }
        }

        /// <summary>
        /// Deletes a drone charge
        /// </summary>
        /// <param name="deleteDroneCharge"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(DroneCharge deleteDroneCharge)
        {
            if (!DataSource.DroneCharges.Remove(deleteDroneCharge))
            {
                throw new NotExistsException($"id: {deleteDroneCharge.DroneId} not exists!!");
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates a drone
        /// </summary>
        /// <param name="updateDrone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateDrone(Drone updateDrone)
        {
            int indexOfDrone = DataSource.Drones.IndexOf(DataSource.Drones.Find(dr => dr.Id == updateDrone.Id));
            if (indexOfDrone == -1)
            {
                throw new NotExistsException($"id: {updateDrone.Id} not exists!!");
            }
            DataSource.Drones[indexOfDrone] = updateDrone;
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="updateCustomer"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer updateCustomer)
        {
            int indexOfCustomer = DataSource.Customers.IndexOf(DataSource.Customers.Find(cu => cu.Id == updateCustomer.Id));
            if (indexOfCustomer == -1)
            {
                throw new NotExistsException($"id: {updateCustomer.Id} not exists!!");
            }
            DataSource.Customers[indexOfCustomer] = updateCustomer;
        }

        /// <summary>
        /// Updates a station
        /// </summary>
        /// <param name="updateStation"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateStation(Station updateStation)
        {
            int indexOfStation = DataSource.Stations.IndexOf(DataSource.Stations.Find(st => st.Id == updateStation.Id));
            if (indexOfStation == -1)
            {
                throw new NotExistsException($"id: {updateStation.Id} not exists!!");
            }
            DataSource.Stations[indexOfStation] = updateStation;
        }

        /// <summary>
        /// Updates a parcel
        /// </summary>
        /// <param name="updateParcel"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcel(Parcel updateParcel)
        {
            int indexOfParcel = DataSource.Parcels.IndexOf(DataSource.Parcels.Find(pr => pr.Id == updateParcel.Id));
            if (indexOfParcel == -1)
            {
                throw new NotExistsException($"id: {updateParcel.Id} not exists!!");
            }
            DataSource.Parcels[indexOfParcel] = updateParcel;
        }
        #endregion

        #region Get Item
        /// <summary>
        /// returns a station by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Station GetStation(int id)
        {
            Station targ;
            for (int i = 0; i < DataSource.Stations.Count; i++)
            {
                targ = DataSource.Stations[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        /// <summary>
        /// returns a drone by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int id)
        {
            Drone targ;
            for (int i = 0; i < DataSource.Drones.Count; i++)
            {
                targ = DataSource.Drones[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new DO.NotExistsException($"id: {id} not exists!!");
        }

        /// <summary>
        /// returns a customer by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int id)
        {
            Customer targ;
            for (int i = 0; i < DataSource.Customers.Count; i++)
            {
                targ = DataSource.Customers[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }

        /// <summary>
        /// returns a parcel by chosen id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int id)
        {
            Parcel targ;
            for (int i = 0; i < DataSource.Parcels.Count; i++)
            {
                targ = DataSource.Parcels[i];
                if (targ.Id == id)
                {
                    return targ;
                }
            }
            throw new NotExistsException($"id: {id} not exists!!");
        }
        #endregion

        #region Get List
        /// <summary>
        /// returns a copy of he stations' list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Station> GetStationsList(Func<Station, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.Stations.ToList();
            return DataSource.Stations.ToList().Where(predicate);
        }

        /// <summary>
        /// returns a copy of he Drones' list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDronesList(Func<Drone, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.Drones.ToList();
            return DataSource.Drones.ToList().Where(predicate);
        }

        /// <summary>
        /// returns a copy of he Customers' list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomersList(Func<Customer, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.Customers.ToList();
            return DataSource.Customers.ToList().Where(predicate);
        }

        /// <summary>
        /// returns a copy of he Parcels' list
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcelsList(Func<Parcel, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.Parcels.ToList();
            return DataSource.Parcels.ToList().Where(predicate);
        }

        /// <summary>
        /// returns a copy of he drone charges' list
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDroneChargesList(Func<DroneCharge, bool> predicate = null)
        {
            if (predicate == null)
                return DataSource.DroneCharges.ToList();
            return DataSource.DroneCharges.ToList().Where(predicate);
        }
        #endregion

        #region Get Electricity Data
        /// <summary>
        /// returns the Electricity Use Per Km Available
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetElectricityUsePerKmAvailable()
        {
            return DataSource.Config.ElectricityUsePerKmAvailable;
        }

        /// <summary>
        /// return the Electricity Use Per Km carrying Light weight
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetElectricityUsePerKmLight()
        {
            return DataSource.Config.ElectricityUsePerKmLight;
        }

        /// <summary>
        /// returns the Electricity Use Per Km carrying Medium weight
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetElectricityUsePerKmMedium()
        {
            return DataSource.Config.ElectricityUsePerKmMedium;
        }

        /// <summary>
        /// return the Electricity Use Per Km carrying Heavy weight
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetElectricityUsePerKmHeavy()
        {
            return DataSource.Config.ElectricityUsePerKmHeavy;
        }

        /// <summary>
        /// return the Electricity Charge Per Second 
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public double GetElectricityChargePerSec()
        {
            return DataSource.Config.ElectricityChargePerSec;
        }
        #endregion

    }
}
