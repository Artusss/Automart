using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}