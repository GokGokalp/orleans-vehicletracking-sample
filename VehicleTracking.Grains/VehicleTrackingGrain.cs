using System;
using System.Threading.Tasks;
using Orleans;
using VehicleTracking.Common;
using VehicleTracking.GrainInterfaces;
using Orleans.Concurrency;

namespace VehicleTracking.Grains
{
    [Reentrant]
    public class VehicleTrackingGrain : Grain, IVehicleTrackingGrain
    {
        private ObserverSubscriptionManager<IVehicleTrackingObserver> _observers;
        private VehicleInfo _vehicleInfo;

        public override Task OnActivateAsync()
        {
            _observers = new ObserverSubscriptionManager<IVehicleTrackingObserver>();

            RegisterTimer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

            return base.OnActivateAsync();
        }

        Task Callback(object callbackState)
        {
            if (_vehicleInfo != null)
            {
                _observers.Notify(x => x.ReportToVehicle(_vehicleInfo));

                _vehicleInfo = null;
            }

            return TaskDone.Done;
        }

        public Task SetVehicleTrackingInfo(VehicleInfo info)
        {
            _vehicleInfo = info;

            return TaskDone.Done;
        }

        public Task Subscribe(IVehicleTrackingObserver observer)
        {
            _observers.Subscribe(observer);

            return TaskDone.Done;
        }

        public Task Unsubscribe(IVehicleTrackingObserver observer)
        {
            _observers.Unsubscribe(observer);

            return TaskDone.Done;
        }
    }
}