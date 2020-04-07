using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Automart.Views;

namespace Automart
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "Automart_dev.db";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
