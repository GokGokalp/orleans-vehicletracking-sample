using System;
using Orleans;
using Orleans.Runtime.Configuration;
using VehicleTracking.GrainInterfaces;

namespace VehicleTracking.TestSilo
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // The Orleans silo environment is initialized in its own app domain in order to more
            // closely emulate the distributed situation, when the client and the server cannot
            // pass data via shared memory.
            AppDomain hostDomain = AppDomain.CreateDomain("OrleansHost", null, new AppDomainSetup
            {
                AppDomainInitializer = InitSilo,
                AppDomainInitializerArguments = args,
            });

            var config = ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);

            // TODO: once the previous call returns, the silo is up and running.
            //       This is the place your custom logic, for example calling client logic
            //       or initializing an HTTP front end for accepting incoming requests.

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");

            var vehicleTrackingObserver = new VehicleTrackingObserver();
            var vehicleTrackingObserverRef = GrainClient.GrainFactory
                                                    .CreateObjectReference<IVehicleTrackingObserver>(vehicleTrackingObserver).Result;

            var vehicleTrackingGrain = GrainClient.GrainFactory.GetGrain<IVehicleTrackingGrain>(1);
            vehicleTrackingGrain.Subscribe(vehicleTrackingObserverRef).Wait();

            hostDomain.DoCallBack(ShutdownSilo);

            Console.ReadLine();
        }

        static void InitSilo(string[] args)
        {
            hostWrapper = new OrleansHostWrapper(args);

            if (!hostWrapper.Run())
            {
                Console.Error.WriteLine("Failed to initialize Orleans silo");
            }
        }

        static void ShutdownSilo()
        {
            if (hostWrapper != null)
            {
                hostWrapper.Dispose();
                GC.SuppressFinalize(hostWrapper);
            }
        }

        private static OrleansHostWrapper hostWrapper;
    }
}