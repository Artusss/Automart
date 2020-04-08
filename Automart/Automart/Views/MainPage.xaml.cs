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
using Newtonsoft;
using Newtonsoft.Json;
using Plugin.Settings;


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

            string curUserVM_json = CrossSettings.Current.GetValueOrDefault("current_user", null);
            if (String.IsNullOrEmpty(curUserVM_json)) SignInToolBar.Text = "Войти";
            else
            {
                SignInToolBar.Text      = "Выйти";
                UserViewModel curUserVM = JsonConvert.DeserializeObject<UserViewModel>(curUserVM_json);
                UserNameLabel.Text      = $"{curUserVM.FirstName} {curUserVM.LastName} #{curUserVM.Id}";
                UserCreatedAtLabel.Text = $"{curUserVM.Created_at.ToString()}";
            }
        }
        
        async void SignIn_Clicked(object sender, EventArgs e)
        {
            SessionEnd();
            await Navigation.PushModalAsync(new NavigationPage(new SignInPage()));
        }

        private static void SessionEnd()
        {
            string curUserVM_json = CrossSettings.Current.GetValueOrDefault("current_user", null);
            if (!String.IsNullOrEmpty(curUserVM_json)) CrossSettings.Current.Remove("current_user");
        }
    }
}