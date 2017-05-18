using System;
using Orleans.Concurrency;

namespace VehicleTracking.Common
{
    [Immutable]
    public class VehicleInfo
    {
        public long DeviceId { get; set; }
        public string Location { get; set; }
        public string Direction { get; set; }
        public DateTime Timestamp { get; set; }
    }
}