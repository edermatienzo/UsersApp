using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UsersApp.Service
{
    /// <summary>
    /// Context implements patterns to access services objects
    /// </summary>
    public class Context
    {
        static DataService dataService;
        static RestService restService;
        public const string REQRES_IN_END_POINT = "https://reqres.in/api/";

        /// <summary>
        /// Gets if device is connected to internet and host is reachable.
        /// </summary>
        public static async Task<bool> IsRestReachable()
        {

            bool isReachable;

            isReachable = CrossConnectivity.Current.IsConnected;

            if (isReachable)
            {
                Uri uri = new Uri(Context.REQRES_IN_END_POINT);

                //only 2 seconds timeout for this test
                isReachable = await CrossConnectivity.Current.IsRemoteReachable(uri.Host, 80, 2000);
            }

            return isReachable;
        }
        /// <summary>
        /// DataService implements a singleton pattern only one instance will be created
        /// </summary>
        public static DataService DataService
        {
            get
            {
                //crear instancia singleton de la base de datos
                if (dataService == null)
                {
                    dataService = new DataService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UsersSQLite.db3"));
                    dataService.Init();
                }
                return dataService;
            }
        }

        /// <summary>
        /// RestService implements a singleton pattern only one instance will be created
        /// </summary>
        public static RestService RestService
        {
            get
            {
                //crear instancia singleton de la base de datos
                if (restService == null)
                {
                    restService = new RestService(REQRES_IN_END_POINT);
                }
                return restService;
            }
        }
    }
}