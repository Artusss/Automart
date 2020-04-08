using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automart.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft;
using Newtonsoft.Json;
using Plugin.Settings;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public static UserSQLiteHelper userSQLiteH;
        public static UserSQLiteHelper UserSQLiteH
        {
            get
            {
                if (userSQLiteH == null)
                {
                    userSQLiteH = new UserSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return userSQLiteH;
            }
        }

        public SignInPage()
        {
            InitializeComponent();
        }

        async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void SignIn_OnButtonClicked(object sender, EventArgs e)
        {
            LoginErrorLabel.Text           = "";
            PasswordErrorLabel.Text        = "";
            ForPersonalDataErrorLabel.Text = "";
            if (LoginEntry.Text.Length < 15)
            {
                LoginErrorLabel.Text = "Введите номер телефона";
                return;
            }
            if (String.IsNullOrEmpty(PasswordEntry.Text))
            {
                PasswordErrorLabel.Text = "Введите пароль";
                return;
            }
            if (!ForPersonalDataCheckBox.IsChecked)
            {
                ForPersonalDataErrorLabel.Text = "Необходимо согласие на обработку";
                return;
            }

            if (!UserSQLiteH.IssetToLogin(LoginEntry.Text))
            {
                LoginErrorLabel.Text = "Данного пользователя не существует";
                return;
            }

            var curUserVM = UserSQLiteH.GetToPassword(LoginEntry.Text, PasswordEntry.Text).FirstOrDefault();
            if (curUserVM == null)
            {
                await DisplayAlert("Неверный пароль", 
                                   "Если у вас возникли сложности с входом или восстановлением пароля, свяжитесь с нами по телефону +7(499)700-0880",
                                   "OK");
                return;
            }

            SessionStart(curUserVM);

            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        private static void SessionStart(UserViewModel curUserVM)
        {
            string userData_json = JsonConvert.SerializeObject(curUserVM);
            CrossSettings.Current.AddOrUpdateValue("current_user", userData_json);
        }
    }
}