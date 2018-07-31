using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace UsersApp.Model.Local
{
    /// <summary>
    /// Represents object User for local database
    /// </summary>
    public class User
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PageIndex { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string Avatar { get; set; }
    }
}
