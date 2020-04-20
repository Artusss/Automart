using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
using Automart.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Automart.Models;
using System.Net;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public const string SMSAeroEMAIL = "avolkov@kit-consulting.ru";
        public const string SMSAeroAPI_KEY = "0o6XHJWLAIfmXb1jmyuPs8UQXQzU";
        public const string SMSAeroURI = "https://gate.smsaero.ru";
        public const int PASS_LENGTH = 10;
        public static UserSQLiteHelper userSQLiteH;
        public static UserSQLiteHelper UserSQLiteH
        {
            get
            {
                if (userSQLiteH == null)
                {
                    userSQLiteH = new UserSQLiteHelper(
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME)
                        );
                }
                return userSQLiteH;
            }
        }

        public SignUpPage()
        {
            InitializeComponent();
        }

        async void SignUp_OnButtonClicked(object sender, EventArgs e)
        {
            NumberErrorLabel.Text    = "";
            FirstNameErrorLabel.Text = "";
            LastNameErrorLabel.Text  = "";
            CityErrorLabel.Text      = "";

            if (LoginEntry.Text.Length < 15)
            {
                NumberErrorLabel.Text = "Введите номер телефона";
                return;
            }
            if (String.IsNullOrEmpty(FirstNameEntry.Text))
            {
                FirstNameErrorLabel.Text = "Введите имя";
                return;
            }
            if (String.IsNullOrEmpty(LastNameEntry.Text))
            {
                LastNameErrorLabel.Text = "Введите фамилию";
                return;
            }

            if (UserSQLiteH.IssetToLogin(LoginEntry.Text))
            {
                await DisplayAlert("Указанный номер уже зарегистрирован", "Восстановить пароль?", "Да", "Нет");
                return;
            }
            string password = CreatePassword();
            UserViewModel userVM = new UserViewModel()
            {
                Login = LoginEntry.Text,
                Password = password,
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                City = CityPicker.Items[CityPicker.SelectedIndex],
                Created_at = DateTime.Now
            };

            string phoneNumber = LoginEntry.Text
                .Replace("+", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace(" ", "");
            string sendMessageParams = "number=" + phoneNumber + "&text=" + password + "&sign=SMS+Aero&channel=DIRECT";
            var sendMessageUri       = new Uri(String.Concat(SMSAeroURI, "/v2/sms/send?", sendMessageParams));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sendMessageUri);
            request.Credentials = new NetworkCredential(SMSAeroEMAIL, SMSAeroAPI_KEY);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();

            UserSQLiteH.SaveItem(userVM);
            await Navigation.PushModalAsync(new NavigationPage(new SignInPage()));
        }

        public string CreatePassword(int length=PASS_LENGTH)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}