﻿using System;
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

        public static AdSQLiteHelper adSQLiteH;
        public static AdSQLiteHelper AdSQLiteH
        {
            get
            {
                if (adSQLiteH == null)
                {
                    adSQLiteH = new AdSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return adSQLiteH;
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
                    Text = JsonConvert.SerializeObject(UserSQLiteH.GetItems()),
                    //"Авторизуйтесь чтобы добавить объявление",
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
                signedSL.Children.Add(UserNameLabel);

                Label AdsLabel = new Label
                {
                    Text      = "Объявления",
                    TextColor = Color.Black,
                    FontSize  = Device.GetNamedSize(NamedSize.Large, typeof(Button))
                };
                signedSL.Children.Add(AdsLabel);

                List<AdViewModel> AdVMs = AdSQLiteH.GetByUser(curUserVM.Id);
                if (AdVMs.Count == 0)
                {
                    Label AdsListLabel = new Label
                    {
                        Text = "У вас пока нет объявлений",
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
                    };
                    signedSL.Children.Add(AdsListLabel);
                }
                else
                {
                    List<AdInfoViewModel> AdInfoVMs = new List<AdInfoViewModel>();
                    foreach (var AdVM in AdVMs)
                    {
                        AdInfoVMs.Add(new AdInfoViewModel(AdVM));
                    }
                    CollectionView AdCollectionView = new CollectionView();
                    AdCollectionView.ItemsSource = AdInfoVMs;
                    AdCollectionView.ItemTemplate = new DataTemplate(() =>
                    {
                        Grid grid = new Grid();
                        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        Image image = new Image
                        {
                            Aspect = Aspect.AspectFill,
                            HeightRequest = 60,
                            WidthRequest = 60,
                            Source = "empty_CAR_FRONT_LEFT_pencil.png"
                        };

                        Label InfoLabel_1 = new Label { FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.StartAndExpand };
                        InfoLabel_1.SetBinding(Label.TextProperty, "Label_1");

                        Label InfoLabel_2 = new Label { FontAttributes = FontAttributes.Italic, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.StartAndExpand };
                        InfoLabel_2.SetBinding(Label.TextProperty, "Label_2");

                        Label InfoLabel_3 = new Label { FontAttributes = FontAttributes.Italic, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.StartAndExpand };
                        InfoLabel_3.SetBinding(Label.TextProperty, "Label_3");

                        Grid.SetRowSpan(image, 2);

                        grid.Children.Add(image);
                        grid.Children.Add(InfoLabel_1, 1, 0);
                        grid.Children.Add(InfoLabel_2, 1, 1);
                        grid.Children.Add(InfoLabel_3, 1, 2);

                        return grid;
                    });
                    signedSL.Children.Add(AdCollectionView);

                    /*ListView AdVMsLV = new ListView
                    {
                        ItemsSource = AdVMs
                    };
                    AdVMsLV.ItemSelected += ToAdPage_ItemSelected;
                    signedSL.Children.Add(AdVMsLV);*/

                }
                /*Label UserCreatedAtLabel = new Label {
                    Text     = $"{curUserVM.Created_at.ToString()}",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
                };
                signedSL.Children.Add(UserCreatedAtLabel);*/

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
                signedSL.Children.Add(AddAdvertisement);

                this.Content = signedSL;
            }
        }

        async void AddAd_Clicked(object sender, EventArgs e)
        {
            var categoryAction = await DisplayActionSheet("Выберите категорию", CANCEL, null, PASS_AUTO, FREIGHT_AUTO);
            if (!categoryAction.Equals(CANCEL))
            {
                AdViewModel adVM = new AdViewModel();
                adVM.Type = categoryAction;
                string AdDataJson = JsonConvert.SerializeObject(adVM);
                CrossSettings.Current.AddOrUpdateValue("AdData", AdDataJson);
                await Navigation.PushModalAsync(new NavigationPage(new OneAdPage()));
            }
        }

        async void ToAdPage_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            AdViewModel selectedAd = (AdViewModel)e.SelectedItem;
            if(selectedAd != null)
            {
                CrossSettings.Current.AddOrUpdateValue("CurrentAdId", selectedAd.Id);
                await Navigation.PushModalAsync(new NavigationPage(new AdPage()));
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