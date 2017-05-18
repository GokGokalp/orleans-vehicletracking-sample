using System.Threading.Tasks;
using Orleans;
using VehicleTracking.Common;

namespace VehicleTracking.GrainInterfaces
{
    public interface  IVehicleTrackingGrain : IGrainWithIntegerKey
    {
        Task SetVehicleTrackingInfo(VehicleInfo info);
        Task Subscribe(IVehicleTrackingObserver observer);
        Task Unsubscribe(IVehicleTrackingObserver observer);
    }
}