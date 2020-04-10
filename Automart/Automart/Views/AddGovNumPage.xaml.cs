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
    public partial class AddGovNumPage : ContentPage
    {
        public AddGovNumPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void Quit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        async void Continue_OnClicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(GovNumEntry.Text))
            {
                GovNumErrorLabel.Text = "Введите гос. номер";
                GovNumErrorLabel.TextColor = Color.Red;
                return;
            }
            CrossSettings.Current.AddOrUpdateValue("Ad_GovNum", GovNumEntry.Text);
            await Navigation.PushAsync(new NavigationPage(new AddAutoMarkPage()));
        }
    }
}