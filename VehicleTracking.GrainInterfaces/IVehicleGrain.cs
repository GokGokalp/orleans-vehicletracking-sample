using System.Threading.Tasks;
using Orleans;
using VehicleTracking.Common;

namespace VehicleTracking.GrainInterfaces
{
    public interface IVehicleGrain : IGrainWithIntegerKey
    {
        Task SetVehicleInfo(VehicleInfo info);
    }
}