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
            CrossSettings.Current.AddOrUpdateValue("AdData", "");
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        async void Continue_OnClicked(object sender, EventArgs e)
        {
            string AdDataJson            = CrossSettings.Current.GetValueOrDefault("AdData", null);
            var adVM                     = JsonConvert.DeserializeObject<AdViewModel>(AdDataJson);
            VINErrorLabel.Text           = "";
            MarkErrorLabel.Text          = "";
            ModelErrorLabel.Text         = "";
            YearErrorLabel.Text          = "";
            MileageErrorLabel.Text       = "";
            KuzovErrorLabel.Text         = "";
            ColorErrorLabel.Text         = "";
            SteeringWheelErrorLabel.Text = "";
            if (String.IsNullOrEmpty(VINEntry.Text))
            {
                VINErrorLabel.Text = "Введите VIN";
                VINErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.VIN = VINEntry.Text;
            if (MarkPicker.SelectedIndex.Equals(-1))
            {
                MarkErrorLabel.Text = "Выберите марку";
                MarkErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Mark = MarkPicker.Items[MarkPicker.SelectedIndex];
            if (ModelPicker.SelectedIndex.Equals(-1))
            {
                ModelErrorLabel.Text = "Выберите модель";
                ModelErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Model = ModelPicker.Items[ModelPicker.SelectedIndex];
            if (YearPicker.SelectedIndex.Equals(-1))
            {
                YearErrorLabel.Text = "Выберите год выпуска";
                YearErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Year = YearPicker.Items[YearPicker.SelectedIndex];
            if (String.IsNullOrEmpty(MileageEntry.Text))
            {
                MileageErrorLabel.Text = "Введите пробег";
                MileageErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Mileage = Convert.ToDouble(MileageEntry.Text);
            if (KuzovPicker.SelectedIndex.Equals(-1))
            {
                KuzovErrorLabel.Text = "Выберите кузов";
                KuzovErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Kuzov = KuzovPicker.Items[KuzovPicker.SelectedIndex];
            if (ColorPicker.SelectedIndex.Equals(-1))
            {
                ColorErrorLabel.Text = "Выберите цвет";
                ColorErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.Color = ColorPicker.Items[ColorPicker.SelectedIndex];
            if (SteeringWheelPicker.SelectedIndex.Equals(-1))
            {
                SteeringWheelErrorLabel.Text = "Выберите руль";
                SteeringWheelErrorLabel.TextColor = Color.Red;
                return;
            }
            else adVM.SteeringWheel = SteeringWheelPicker.Items[SteeringWheelPicker.SelectedIndex];

            string AdJson = JsonConvert.SerializeObject(adVM);
            CrossSettings.Current.AddOrUpdateValue("AdData", AdJson);
            await Navigation.PushAsync(new NavigationPage(new TwoAdPage()));
        }
    }
}