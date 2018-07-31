using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Model.Rest;

namespace UsersApp.Service
{
    /// <summary>
    /// RestService Object include all methods to access data from API Rest reqres.in
    /// </summary>
    public class RestService
    {
        HttpClient client;
        string _endpoint;

        public RestService(string endpoint)
        {
            _endpoint = endpoint;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        /// <summary>
        /// Gets User Page object from API
        /// </summary>
        public async Task<UserPage> GetUserPageAsync(int page)
        {

            UserPage userPage = null;
            Uri uri = new Uri(String.Format(_endpoint + "users?page={0}", page));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                userPage = JsonConvert.DeserializeObject<UserPage>(content);
            }
            return userPage;
        }
    }
}
