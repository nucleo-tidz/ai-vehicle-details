namespace service
{
    using infrastructure.Agents;
    using model;
    public class VehicleService(IVehicleAgent vehicleAgent): IVehicleService
    {
        public async Task<VehicleModel> GetVehicleDetails(byte[] regitraionPlate)
        {           
            string response = await vehicleAgent.Execute(regitraionPlate);  
            return new VehicleModel(); 
        }
    }
   
}
