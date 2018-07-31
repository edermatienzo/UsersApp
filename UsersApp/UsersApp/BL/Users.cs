using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Service;

namespace UsersApp.BL
{
    /// <summary>
    /// Represents Business Logic for User operations
    /// </summary>
    public class Users
    {

        /// <summary>
        /// Remove all Users from table
        /// </summary>
        public static async Task ResetUserAsync()
        {
            await Context.DataService.ResetUserAsync();
        }

        /// <summary>
        /// Get Users records from API Rest and storage them in local database
        /// </summary>
        public static async Task<List<Model.Local.User>> DownloadUsersAsync(int page, int pageCount)
        {
            //we need return users for UI interaction
            List<Model.Local.User> users = new List<Model.Local.User>();
            
            //iterate  in each requested page
            for (int pageIndex = page; pageIndex <= page + (pageCount - 1); pageIndex++)
            {
                //Get user page from API Rest
                Model.Rest.UserPage userPage = await Context.RestService.GetUserPageAsync(pageIndex);
                foreach (Model.Rest.User userRest in userPage.data)
                {
                    Model.Local.User userLocal = new Model.Local.User()
                    {
                        ID = userRest.id,
                        Avatar = userRest.avatar,
                        FirstName = userRest.first_name,
                        LastName = userRest.last_name,
                        PageIndex = userPage.page
                    };

                    users.Add(userLocal);
                    await Context.DataService.InsertUserAsync(userLocal); //storage in local database
                }
            }
            return users;
        }

        /// <summary>
        /// Get all users in table User
        /// </summary>
        public static Task<List<Model.Local.User>> GetUsersAsync()
        {
            return Context.DataService.GetUsersAsync();
        }
    }
}
