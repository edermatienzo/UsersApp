using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersApp.Model.Rest
{
    /// <summary>
    /// Represents object User for API Rest operations
    /// </summary>
    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }        
    }
}
