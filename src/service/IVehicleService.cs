namespace service
{
    using System.Threading.Tasks;

    using model;

    public interface IVehicleService
    {
         Task<VehicleModel> GetVehicleDetails (byte[] regitraionPlate);
    }
}
