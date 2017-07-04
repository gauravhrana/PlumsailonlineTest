using System.Web.Http;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace PlumsailOnlineTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
