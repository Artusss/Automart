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
    public partial class AddAutoMarkPage : ContentPage
    {
        public AddAutoMarkPage()
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
            if (String.IsNullOrEmpty(AutoMarkEntry.Text))
            {
                AutoMarkErrorLabel.Text = "Введите марку авто";
                AutoMarkErrorLabel.TextColor = Color.Red;
                return;
            }
            CrossSettings.Current.AddOrUpdateValue("Ad_AutoMark", AutoMarkEntry.Text);
            await Navigation.PushAsync(new NavigationPage(new AddAutoMarkPage()));
        }

    }
}