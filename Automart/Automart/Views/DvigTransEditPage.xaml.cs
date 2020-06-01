using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft;
using Newtonsoft.Json;
using Automart.ViewModels;
using System.IO;
using Plugin.InputKit.Shared.Controls;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DvigTransEditPage : ContentPage
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
        public DvigTransEditPage()
        {
            InitializeComponent();

            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);
            VolumeEntry.Text = AdVM.Volume.ToString();
            PowerEntry.Text = AdVM.Power.ToString();
            foreach (var DvigType in DvigTypeSQLiteH.GetItems())
            {
                DvigTypePicker.Items.Add(DvigType.Value);
            }
            DvigTypePicker.SelectedIndex = DvigTypePicker.Items.IndexOf(AdVM.DvigType);
            foreach (var KPP in KPPSQLiteH.GetItems())
            {
                KPPPicker.Items.Add(KPP.Value);
            }
            KPPPicker.SelectedIndex = KPPPicker.Items.IndexOf(AdVM.KPP);
            foreach (var DriveUnit in DriveUnitSQLiteH.GetItems())
            {
                DriveUnitPicker.Items.Add(DriveUnit.Value);
            }
            DriveUnitPicker.SelectedIndex = DriveUnitPicker.Items.IndexOf(AdVM.DriveUnit);
        }

        async void DvigTransSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);
            AdVM.DvigType = DvigTypePicker.Items[DvigTypePicker.SelectedIndex];
            AdVM.KPP = KPPPicker.Items[KPPPicker.SelectedIndex];
            AdVM.DriveUnit = DriveUnitPicker.Items[DriveUnitPicker.SelectedIndex];
            AdVM.Volume = Convert.ToDouble(VolumeEntry.Text);
            AdVM.Power = Convert.ToDouble(PowerEntry.Text);

            AdSQLiteH.SaveItem(AdVM);
            await DisplayAlert("", "Двигатель и трансмиссия успешно сохранена", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new AdPage()));
            return;
        }
    }
}