using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using VehicleTracking.Common;
using VehicleTracking.GrainInterfaces;

namespace VehicleTracking.Grains
{
    [Reentrant]
    public class VehicleGrain : Grain, IVehicleGrain
    {
        private long _currentGrainId;

        public override Task OnActivateAsync()
        {
            _currentGrainId = this.GetPrimaryKeyLong();

            return base.OnActivateAsync();
        }

        public async Task SetVehicleInfo(VehicleInfo info)
        {
            //some business logics...

            var vehicleTrackingGrain = GrainFactory.GetGrain<IVehicleTrackingGrain>(_currentGrainId);

            await vehicleTrackingGrain.SetVehicleTrackingInfo(info);
        }
    }
}