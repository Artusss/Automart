using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft;
using Newtonsoft.Json;
using Automart.ViewModels;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwoAdPage : ContentPage
    {
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
        public TwoAdPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //InfoLabel.Text = CrossSettings.Current.GetValueOrDefault("current_user", null); ;
        }

        async void Quit_Clicked(object sender, EventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("AdData", "");
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        async void AddAd_OnClicked(object sender, EventArgs e)
        {
            string AdDataJson        = CrossSettings.Current.GetValueOrDefault("AdData", null);
            if (String.IsNullOrEmpty(AdDataJson))
            {
                await DisplayAlert("Предупреждение", "Не удалось создать объявление", "ОК");
                return;
            }
            var adVM                 = JsonConvert.DeserializeObject<AdViewModel>(AdDataJson);
            DvigTypeErrorLabel.Text  = "";
            KPPErrorLabel.Text       = "";
            DriveUnitErrorLabel.Text = "";
            VolumeErrorLabel.Text    = "";
            PowerErrorLabel.Text     = "";
            if (DvigTypePicker.SelectedIndex.Equals(-1))
            {
                DvigTypeErrorLabel.Text      = "Выберите тип двигателя";
                DvigTypeErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.DvigType = DvigTypePicker.Items[DvigTypePicker.SelectedIndex];
            if (KPPPicker.SelectedIndex.Equals(-1))
            {
                KPPErrorLabel.Text      = "Выберите КПП";
                KPPErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.KPP = KPPPicker.Items[KPPPicker.SelectedIndex];
            if (DriveUnitPicker.SelectedIndex.Equals(-1))
            {
                DriveUnitErrorLabel.Text      = "Выберите привод";
                DriveUnitErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.DriveUnit = DriveUnitPicker.Items[DriveUnitPicker.SelectedIndex];
            if (String.IsNullOrEmpty(VolumeEntry.Text))
            {
                VolumeErrorLabel.Text      = "Введите объем";
                VolumeErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Volume = Convert.ToDouble(VolumeEntry.Text);
            if (String.IsNullOrEmpty(PowerEntry.Text))
            {
                PowerErrorLabel.Text      = "Введите мощность";
                PowerErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Power = Convert.ToDouble(PowerEntry.Text);

            CrossSettings.Current.AddOrUpdateValue("AdData", "");
            string curUserVM_json   = CrossSettings.Current.GetValueOrDefault("current_user", null);
            int curUserVMId         = JsonConvert.DeserializeObject<UserViewModel>(curUserVM_json).Id;
            adVM.UserId             = curUserVMId;
            adVM.Created_at         = DateTime.Now;

            int CurrentAdId = AdSQLiteH.SaveItem(adVM);
            CrossSettings.Current.AddOrUpdateValue("CurrentAdId", CurrentAdId);
            await Navigation.PushModalAsync(new NavigationPage(new AdPage()));
        }
    }
}