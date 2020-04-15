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
        const string CANCEL       = "Отмена";
        const string PASS_AUTO    = "Легковой авто";
        const string FREIGHT_AUTO = "Грузовой авто";

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
            CrossSettings.Current.AddOrUpdateValue("AdData", "");
            string curUserVM_json = CrossSettings.Current.GetValueOrDefault("current_user", null);
            if (String.IsNullOrEmpty(curUserVM_json))
            {
                StackLayout notSignedSL = new StackLayout
                {
                    Margin = new Thickness(10, 0),
                    Padding = new Thickness(10)
                };
                SignInToolBar.Text = "Войти";
                Label InfoLabel = new Label
                {
                    Text = "Авторизуйтесь чтобы добавить объявление",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button))
                };

                notSignedSL.Children.Add(InfoLabel);
                this.Content = notSignedSL;
            }
            else
            {
                UserViewModel curUserVM = JsonConvert.DeserializeObject<UserViewModel>(curUserVM_json);

                StackLayout signedSL = new StackLayout
                {
                    Margin = new Thickness(10, 0),
                    Padding = new Thickness(10)
                };
                SignInToolBar.Text       = "Выйти";
                Label UserNameLabel      = new Label {
                    Text     = $"{curUserVM.FirstName} {curUserVM.LastName} #{curUserVM.Id}",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button))
                };
                Label AdsLabel = new Label
                {
                    Text      = "Объявления",
                    TextColor = Color.Black,
                    FontSize  = Device.GetNamedSize(NamedSize.Large, typeof(Button))
                };
                Label AdsListLabel = new Label
                {
                    Text = "У вас пока нет объявлений",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
                };
                Label UserCreatedAtLabel = new Label {
                    Text     = $"{curUserVM.Created_at.ToString()}",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
                };
                Button AddAdvertisement = new Button {
                    Text              = "Добавить объявление",
                    BackgroundColor   = Color.Green,
                    TextColor         = Color.White,
                    FontSize          = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                    BorderWidth       = 1,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions   = LayoutOptions.EndAndExpand,
                    Margin            = new Thickness(0, 20),
                    Padding           = new Thickness(50, 0)
                };
                AddAdvertisement.Clicked += AddAd_Clicked;

                signedSL.Children.Add(UserNameLabel);
                signedSL.Children.Add(UserCreatedAtLabel);
                signedSL.Children.Add(AdsLabel);
                signedSL.Children.Add(AdsListLabel);
                signedSL.Children.Add(AddAdvertisement);
                this.Content = signedSL;
            }
        }

        async void AddAd_Clicked(object sender, EventArgs e)
        {
            var categoryAction = await DisplayActionSheet("Выберите категорию", CANCEL, null, PASS_AUTO, FREIGHT_AUTO);
            if (!categoryAction.Equals(CANCEL))
            {
                Dictionary<string, string> AdDict = new Dictionary<string, string>();
                AdDict.Add("AutoType", categoryAction);
                string AdDataJson = JsonConvert.SerializeObject(AdDict);
                CrossSettings.Current.AddOrUpdateValue("AdData", AdDataJson);
                await Navigation.PushModalAsync(new NavigationPage(new OneAdPage()));
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
            if (!String.IsNullOrEmpty(curUserVM_json)) 
                CrossSettings.Current.Remove("current_user");
        }
    }
}