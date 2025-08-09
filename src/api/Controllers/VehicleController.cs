namespace api.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.SemanticKernel;

    using service;

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController(IVehicleService vehicleService) : ControllerBase
    {
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }
           await vehicleService.GetVehicleDetails(fileBytes);
            return Ok("File uploaded successfully.");
        }
    }
}
