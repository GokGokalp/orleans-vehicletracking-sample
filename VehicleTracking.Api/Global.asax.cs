using Orleans;
using System.Web.Http;

namespace VehicleTracking.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo();
            GrainClient.Initialize(config);
        }
    }
}