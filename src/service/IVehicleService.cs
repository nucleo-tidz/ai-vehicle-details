namespace service
{
    using System.Threading.Tasks;

    using model;

    public interface IVehicleService
    {
         Task<string> GetVehicleDetails (byte[] regitraionPlate);
    }
}
