using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAdPage : ContentPage
    {
        public AddAdPage()
        {
            InitializeComponent();
        }

        async void Quit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        async void Continue_OnClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(VINEntry.Text))
            {
                VINErrorLabel.Text = "Введите VIN";
                VINErrorLabel.TextColor = Color.Red;
                return;
            }
            CrossSettings.Current.AddOrUpdateValue("Ad_VIN", VINEntry.Text);
            await Navigation.PushAsync(new NavigationPage(new AddGovNumPage()));
        }
    }
}