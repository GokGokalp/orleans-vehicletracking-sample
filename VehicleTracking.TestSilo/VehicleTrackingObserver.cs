using System;
using VehicleTracking.Common;
using VehicleTracking.GrainInterfaces;

namespace VehicleTracking.TestSilo
{
    public class VehicleTrackingObserver : IVehicleTrackingObserver
    {
        public void ReportToVehicle(VehicleInfo info)
        {
            Console.WriteLine($"The vehicle id {info.DeviceId} moved to {info.Direction} from {info.Location} at {info.Timestamp.ToShortTimeString()} o'clock.");
        }
    }
}