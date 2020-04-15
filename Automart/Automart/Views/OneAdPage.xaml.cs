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
    public partial class OneAdPage : ContentPage
    {
        public OneAdPage()
        {
            InitializeComponent();
        }

        async void Quit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        async void Continue_OnClicked(object sender, EventArgs e)
        {
            /*if (String.IsNullOrEmpty(VINEntry.Text))
            {
                VINErrorLabel.Text = "Введите VIN";
                VINErrorLabel.TextColor = Color.Red;
                return;
            }
            CrossSettings.Current.AddOrUpdateValue("Ad_VIN", VINEntry.Text);*/
            Dictionary<string, string> OneAdDict = new Dictionary<string, string>();
            OneAdDict.Add("VIN", VINEntry.Text);
            OneAdDict.Add("Mark", MarkPicker.Items[MarkPicker.SelectedIndex]);
            OneAdDict.Add("Model", ModelPicker.Items[ModelPicker.SelectedIndex]);
            OneAdDict.Add("Year", YearPicker.Items[YearPicker.SelectedIndex]);
            OneAdDict.Add("Mileage", MileageEntry.Text);
            OneAdDict.Add("Kuzov", KuzovPicker.Items[KuzovPicker.SelectedIndex]);
            OneAdDict.Add("Color", ColorPicker.Items[ColorPicker.SelectedIndex]);
            OneAdDict.Add("SteeringWheel", SteeringWheelPicker.Items[SteeringWheelPicker.SelectedIndex]);
            string OneAdJson = JsonConvert.SerializeObject(OneAdDict);
            CrossSettings.Current.AddOrUpdateValue("OnePageJsonData", OneAdJson);
            await Navigation.PushAsync(new NavigationPage(new TwoAdPage()));
        }
    }
}