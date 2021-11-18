using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        void AddDrone(Drone blDrone, int stationId);
        void AddStation(Station blStation);
        void AddCustomer(Customer customer);
        void AddParcel(int senderId, int reciverId, int weight, int priority);
        void UpdateDrone(int id, string model);
        void UpdateStation(int id, string name, int chargingSlots);
        void UpdateCustomer(int id, string name, string phone);
        void SendDroneToCharge(int id);
        Station GetStation(int id);
    }
}