namespace service
{
    using infrastructure.Agents;
    using model;
    public class VehicleService(IVehicleAgent vehicleAgent): IVehicleService
    {
        public async Task<string> GetVehicleDetails(byte[] regitraionPlate)
        {           
            return await vehicleAgent.Execute(regitraionPlate);           
        }
    }
   
}
