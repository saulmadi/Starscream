using System.Configuration;

namespace IvoryTower.Data
{
    public class ConnectionStrings
    {
        public static string Get()
        {
            var local = ConfigurationManager.ConnectionStrings["local"].ToString();

            var remote = (ConfigurationManager.ConnectionStrings["remote"].ConnectionString);

            //var production = (ConfigurationManager.ConnectionStrings["production"].ConnectionString);

            var environment = (ConfigurationManager.AppSettings["Environment"] ?? "").ToLower();
            var connectionStringToUse = local;


            if (environment == "qa" || environment == "remote")
            {
                connectionStringToUse = remote;
            }
            //else if (environment == "production")
            //{
            //    connectionStringToUse = production;
            //}

            return connectionStringToUse;
        }
    }
}