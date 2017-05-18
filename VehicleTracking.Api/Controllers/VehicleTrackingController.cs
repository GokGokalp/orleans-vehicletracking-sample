using Orleans;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using VehicleTracking.Common;
using VehicleTracking.GrainInterfaces;

namespace VehicleTracking.Api.Controllers
{
    public class VehicleTrackingController : ApiController
    {
        [Route("api/vehicle-trackings")]
        public async Task Post(long deviceId, string location, string direction)
        {
            var vehicleGrain = GrainClient.GrainFactory.GetGrain<IVehicleGrain>(deviceId);

            VehicleInfo trafficInfo = new VehicleInfo()
            {
                DeviceId = deviceId,
                Location = location,
                Direction = direction,
                Timestamp = DateTime.Now
            };

            await vehicleGrain.SetVehicleInfo(trafficInfo);
        }
    }
}