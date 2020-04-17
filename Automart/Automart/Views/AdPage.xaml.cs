using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automart.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdPage : ContentPage
    {
        public static AdSQLiteHelper adSQLiteH;
        public static AdSQLiteHelper AdSQLiteHelper
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
        public AdPage()
        {
            InitializeComponent();

            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteHelper.GetById(CurrentAdId);

            StackLayout AdSL = new StackLayout
            {
                Margin = new Thickness(10, 0),
                Padding = new Thickness(10)
            };
            Label InfoLabel = new Label
            {
                Text = $"Объявление № {CurrentAdId}",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button))
            };

            StackLayout stackLayout = new StackLayout();

            AdSL.Children.Add(InfoLabel);
            
            Label MainAutoAboutLabel = new Label
            {
                Text = "Общая информация",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            stackLayout.Children.Add(MainAutoAboutLabel);

            Label VINLabel = new Label
            {
                Text = $"VIN: {AdVM.VIN}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(VINLabel);

            Label TypeLabel = new Label
            {
                Text = $"Тип: {AdVM.Type}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(TypeLabel);

            Label MarkLabel = new Label
            {
                Text = $"Марка: {AdVM.Mark}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(MarkLabel);

            Label ModelLabel = new Label
            {
                Text = $"Модель: {AdVM.Model}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(ModelLabel);

            Label YearLabel = new Label
            {
                Text = $"Год выпуска: {AdVM.Year}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(YearLabel);

            Label MileageLabel = new Label
            {
                Text = $"Пробег: {AdVM.Mileage}, км",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(MileageLabel);

            Label KuzovLabel = new Label
            {
                Text = $"Кузов: {AdVM.Kuzov}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(KuzovLabel);

            Label ColorLabel = new Label
            {
                Text = $"Цвет: {AdVM.Color}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(ColorLabel);

            Label SteeringWheelLabel = new Label
            {
                Text = $"Руль: {AdVM.SteeringWheel}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(SteeringWheelLabel);

            Label DvigTransAboutLabel = new Label
            {
                Text = "Двигатель и трансмиссия",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            stackLayout.Children.Add(DvigTransAboutLabel);

            Label DvigTypeLabel = new Label
            {
                Text = $"Тип двигателя: {AdVM.DvigType}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(DvigTypeLabel);

            Label KPPLabel = new Label
            {
                Text = $"КПП: {AdVM.KPP}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(KPPLabel);

            Label DriveUnitLabel = new Label
            {
                Text = $"Привод: {AdVM.DriveUnit}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(DriveUnitLabel);

            Label VolumeLabel = new Label
            {
                Text = $"Объем: {AdVM.Volume}, л",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(VolumeLabel);

            Label PowerLabel = new Label
            {
                Text = $"Мощность: {AdVM.Power}, л.с.",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            stackLayout.Children.Add(PowerLabel);

            ScrollView scrollView = new ScrollView
            {
                Content = stackLayout
            };
            AdSL.Children.Add(scrollView);
            this.Content = AdSL;
        }

        async void ToMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }
    }
}