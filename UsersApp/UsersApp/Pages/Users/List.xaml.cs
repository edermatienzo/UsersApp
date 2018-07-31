using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersApp.Helpers;
using UsersApp.Model.Local;
using UsersApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersApp.Pages.Users
{
    /// <summary>
    /// Page Users > List display a list of users. Includes an infinite scroll to display more pages of data in online mode.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List : ContentPage
    {
        bool loading = true; //indicator of first loading
        bool noMoreData = false; //indicator of no more data for online infinite scroll
        public ObservableRangeCollection<User> Users = new ObservableRangeCollection<User>(); //observable collection of users
        public List()
        {
            InitializeComponent();

            
            lstUsers.ItemsSource = Users; //binding to ObservableCollection
            lstUsers.RefreshCommand = new Command(async () => await RefreshData());
        }

        /// <summary>
        /// Refresh Users data from API Rest
        /// </summary>
        private async Task RefreshData()
        {
            
            lstUsers.IsRefreshing = true;
            //if connected to internet refresh current data
            if (await Context.IsRestReachable())
            {
                noMoreData = false;
                
                await BL.Users.ResetUserAsync(); //clear current users data
                List<User> users = await BL.Users.DownloadUsersAsync(1, 3); //get three pages of data

                Users.Clear();
                Users.AddRange(users);
            }
            lstUsers.IsRefreshing = false;


        }


        /// <summary>
        /// Page Appearing Event
        /// </summary>
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            
            if (loading) //if is first loading
            {
                //get user lists from local database
                List<User> users = await BL.Users.GetUsersAsync();

                //if doesnot exists any record in local database get records from API Rest
                if (users.Count == 0)
                {
                    await RefreshData();
                }
                else
                {
                    Users.AddRange(users);
                }

                loading = false;
            }
        }

        /// <summary>
        /// List View Item Appearing Event
        /// </summary>
        private async void lstUsers_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            actLoadingMore.IsRunning = true;
            
            if (await Context.IsRestReachable()) //if connected to internet refresh current data
            {                
                if (!lstUsers.IsRefreshing && !noMoreData) //if not refreshing and exists more data
                {

                    if (Users.Count - 1 <= Users.IndexOf((User)e.Item)) //if last item in List View
                    {
                        
                        int pageIndexMax = (from _user in Users select _user.PageIndex).Max(); //get max page Index
                        List<User> newUsers = await BL.Users.DownloadUsersAsync(pageIndexMax + 1, 1); //download and get new users
                        if (newUsers.Count != 0)
                        {
                            Users.AddRange(newUsers); //add new Users to binding ObservableCollection
                        }
                        else
                        {
                            noMoreData = true; //if no data put indicator to on
                        }


                        
                    }

                }
                

            }
            actLoadingMore.IsRunning = false;
        }
    }
}