using Orleans;
using VehicleTracking.Common;

namespace VehicleTracking.GrainInterfaces
{
    public interface IVehicleTrackingObserver : IGrainObserver
    {
        void ReportToVehicle(VehicleInfo info);
    }
}