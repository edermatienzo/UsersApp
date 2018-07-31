using System;
using System.Collections.Generic;
using System.Text;

namespace UsersApp.Model.Rest
{
    /// <summary>
    /// Represents object User Page for API Rest operations
    /// </summary>
    public class UserPage
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public User[] data;
    }
}
