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

        public static DvigTypeSQLiteHelper dvigTypeSQLiteH;
        public static DvigTypeSQLiteHelper DvigTypeSQLiteH
        {
            get
            {
                if (dvigTypeSQLiteH == null)
                {
                    dvigTypeSQLiteH = new DvigTypeSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return dvigTypeSQLiteH;
            }
        }

        public static KPPSQLiteHelper kPPSQLiteH;
        public static KPPSQLiteHelper KPPSQLiteH
        {
            get
            {
                if (kPPSQLiteH == null)
                {
                    kPPSQLiteH = new KPPSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return kPPSQLiteH;
            }
        }

        public static DriveUnitSQLiteHelper driveUnitSQLiteH;
        public static DriveUnitSQLiteHelper DriveUnitSQLiteH
        {
            get
            {
                if (driveUnitSQLiteH == null)
                {
                    driveUnitSQLiteH = new DriveUnitSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return driveUnitSQLiteH;
            }
        }

        public static KomplektnostSQLiteHelper komplektnostSQLiteH;
        public static KomplektnostSQLiteHelper KomplektnostSQLiteH
        {
            get
            {
                if (komplektnostSQLiteH == null)
                {
                    komplektnostSQLiteH = new KomplektnostSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return komplektnostSQLiteH;
            }
        }

        public static KomplektacyaSQLiteHelper komplektacyaSQLiteH;
        public static KomplektacyaSQLiteHelper KomplektacyaSQLiteH
        {
            get
            {
                if (komplektacyaSQLiteH == null)
                {
                    komplektacyaSQLiteH = new KomplektacyaSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return komplektacyaSQLiteH;
            }
        }

        public TwoAdPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            /*KPPSQLiteH.DeleteAll();
            List<string> KPPItemsList = new List<string>()
            {"Default"};
            List<KPPViewModel> KPPVMs = new List<KPPViewModel>();
            foreach (string KPPItem in KPPItemsList)
            {
                KPPVMs.Add(new KPPViewModel() { Value = KPPItem });
            }
            KPPSQLiteH.SaveItems(KPPVMs);*/

            foreach (var DvigType in DvigTypeSQLiteH.GetItems())
            {
                DvigTypePicker.Items.Add(DvigType.Value);
            }
            foreach (var KPP in KPPSQLiteH.GetItems())
            {
                KPPPicker.Items.Add(KPP.Value);
            }
            foreach (var DriveUnit in DriveUnitSQLiteH.GetItems())
            {
                DriveUnitPicker.Items.Add(DriveUnit.Value);
            }
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
                return;
            }
            else adVM.DvigType = DvigTypePicker.Items[DvigTypePicker.SelectedIndex];
            if (KPPPicker.SelectedIndex.Equals(-1))
            {
                KPPErrorLabel.Text      = "Выберите КПП";
                return;
            }
            else adVM.KPP = KPPPicker.Items[KPPPicker.SelectedIndex];
            if (DriveUnitPicker.SelectedIndex.Equals(-1))
            {
                DriveUnitErrorLabel.Text      = "Выберите привод";
                return;
            }
            else adVM.DriveUnit = DriveUnitPicker.Items[DriveUnitPicker.SelectedIndex];
            if (String.IsNullOrEmpty(VolumeEntry.Text))
            {
                VolumeErrorLabel.Text      = "Введите объем";
                return;
            }
            else adVM.Volume = Convert.ToDouble(VolumeEntry.Text);
            if (String.IsNullOrEmpty(PowerEntry.Text))
            {
                PowerErrorLabel.Text      = "Введите мощность";
                return;
            }
            else if (PowerEntry.Text.Length < 2 || PowerEntry.Text.Length > 6)
            {
                PowerErrorLabel.Text = "Мощность должна содержать от 2 до 6 символов";
                return;
            }
            else adVM.Power = Convert.ToDouble(PowerEntry.Text);

            CrossSettings.Current.AddOrUpdateValue("AdData", "");
            string curUserVM_json   = CrossSettings.Current.GetValueOrDefault("current_user", null);
            int curUserVMId         = JsonConvert.DeserializeObject<UserViewModel>(curUserVM_json).Id;
            adVM.UserId             = curUserVMId;
            adVM.Created_at         = DateTime.Now;

            int CurrentAdId = AdSQLiteH.SaveItem(adVM);

            KomplektnostSQLiteH.SaveItem(KomplektnostSQLiteH.GetDefaultKomplektnostVM(CurrentAdId));
            KomplektacyaSQLiteH.SaveItem(KomplektacyaSQLiteH.GetDefaultKomplektacyaVM(CurrentAdId));

            CrossSettings.Current.AddOrUpdateValue("CurrentAdId", CurrentAdId);
            await Navigation.PushModalAsync(new NavigationPage(new AdPage()));
        }
    }
}