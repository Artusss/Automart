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

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwoAdPage : ContentPage
    {
        public TwoAdPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //InfoLabel.Text = CrossSettings.Current.GetValueOrDefault("AdData", null); ;
        }

        async void Quit_Clicked(object sender, EventArgs e)
        {
            CrossSettings.Current.AddOrUpdateValue("AdData", "");
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        async void AddAd_OnClicked(object sender, EventArgs e)
        {
            string AdDataJson        = CrossSettings.Current.GetValueOrDefault("AdData", null);
            var AdDict               = JsonConvert.DeserializeObject<Dictionary<string, string>>(AdDataJson);
            DvigTypeErrorLabel.Text  = "";
            KPPErrorLabel.Text       = "";
            DriveUnitErrorLabel.Text = "";
            VolumeErrorLabel.Text    = "";
            PowerErrorLabel.Text     = "";
            if (DvigTypePicker.SelectedIndex.Equals(-1))
            {
                DvigTypeErrorLabel.Text = "Выберите тип двигателя";
                DvigTypeErrorLabel.TextColor = Color.Red;
                return;
            }
            else AdDict.Add("DvigType", DvigTypePicker.Items[DvigTypePicker.SelectedIndex]);
            if (KPPPicker.SelectedIndex.Equals(-1))
            {
                KPPErrorLabel.Text = "Выберите КПП";
                KPPErrorLabel.TextColor = Color.Red;
                return;
            }
            else AdDict.Add("KPP", KPPPicker.Items[KPPPicker.SelectedIndex]);
            if (DriveUnitPicker.SelectedIndex.Equals(-1))
            {
                DriveUnitErrorLabel.Text = "Выберите привод";
                DriveUnitErrorLabel.TextColor = Color.Red;
                return;
            }
            else AdDict.Add("DriveUnit", DriveUnitPicker.Items[DriveUnitPicker.SelectedIndex]);
            if (String.IsNullOrEmpty(VolumeEntry.Text))
            {
                VolumeErrorLabel.Text = "Введите объем";
                VolumeErrorLabel.TextColor = Color.Red;
                return;
            }
            else AdDict.Add("Volume", VolumeEntry.Text);
            if (String.IsNullOrEmpty(PowerEntry.Text))
            {
                PowerErrorLabel.Text = "Введите мощность";
                PowerErrorLabel.TextColor = Color.Red;
                return;
            }
            else AdDict.Add("Power", PowerEntry.Text);

            string AdJson = JsonConvert.SerializeObject(AdDict);
            CrossSettings.Current.AddOrUpdateValue("AdData", AdJson);

            // Create new ad here
            // get 'CurrentAdId' from created ad
            // CrossSettings.Current.AddOrUpdateValue("AdData", "");
            // CrossSettings.Current.AddOrUpdateValue("CurrentAdId", {ID});

            await Navigation.PushModalAsync(new NavigationPage(new AdPage()));
        }
    }
}