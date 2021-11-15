using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        void AddDrone(Drone blDrone, int stationId);
        void AddStation(Station blStation);
        void AddCustomer(Customer customer);
        void AddParcel(int senderId, int reciverId, int weight, int priority);
        void UpdateDrone(Drone drone);
    }
}