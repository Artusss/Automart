using System;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automart.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Automart.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public static UserSQLiteHelper userSQLiteH;
        public static UserSQLiteHelper UserSQLiteH
        {
            get
            {
                if (userSQLiteH == null)
                {
                    userSQLiteH = new UserSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return userSQLiteH;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            List<UserViewModel> UsersVM = UserSQLiteH.GetItems().ToList();

            List<string> usersList = new List<string>();

            foreach(var userVM in UsersVM)
            {
                usersList.Add($"{userVM.Id} : {userVM.Login}");
            }

            UserListView.ItemsSource = usersList;
        }
        
        async void SignIn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SignInPage()));
        }
    }
}