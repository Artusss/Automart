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
using System.IO;
using Plugin.InputKit.Shared.Controls;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneAdPage : ContentPage
    {
        public static MarkSQLiteHelper markSQLiteH;
        public static MarkSQLiteHelper MarkSQLiteH
        {
            get
            {
                if (markSQLiteH == null)
                {
                    markSQLiteH = new MarkSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return markSQLiteH;
            }
        }

        public static ModelSQLiteHelper modelSQLiteH;
        public static ModelSQLiteHelper ModelSQLiteH
        {
            get
            {
                if (modelSQLiteH == null)
                {
                    modelSQLiteH = new ModelSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return modelSQLiteH;
            }
        }

        public static KuzovSQLiteHelper kuzovSQLiteH;
        public static KuzovSQLiteHelper KuzovSQLiteH
        {
            get
            {
                if (kuzovSQLiteH == null)
                {
                    kuzovSQLiteH = new KuzovSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return kuzovSQLiteH;
            }
        }

        public static YearSQLiteHelper yearSQLiteH;
        public static YearSQLiteHelper YearSQLiteH
        {
            get
            {
                if (yearSQLiteH == null)
                {
                    yearSQLiteH = new YearSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return yearSQLiteH;
            }
        }

        public static ColorSQLiteHelper colorSQLiteH;
        public static ColorSQLiteHelper ColorSQLiteH
        {
            get
            {
                if (colorSQLiteH == null)
                {
                    colorSQLiteH = new ColorSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return colorSQLiteH;
            }
        }
        public OneAdPage()
        {
            InitializeComponent();

            foreach (var Mark in MarkSQLiteH.GetItems())
            {
                MarkPicker.Items.Add(Mark.Value);
            }
            foreach (var Model in ModelSQLiteH.GetItems())
            {
                ModelPicker.Items.Add(Model.Value);
            }
            foreach (var Kuzov in KuzovSQLiteH.GetItems())
            {
                KuzovPicker.Items.Add(Kuzov.Value);
            }
            foreach (var Year in YearSQLiteH.GetItems())
            {
                YearPicker.Items.Add(Year.Value);
            }
            foreach (var Color in ColorSQLiteH.GetItems())
            {
                ColorPicker.Items.Add(Color.Value);
            }

        }

        public string GetterRB(RadioButtonGroupView ItemsRB)
        {
            string FieldValue = "";
            foreach (var Item in ItemsRB.Children)
            {
                if (Item is RadioButton ItemRB && ItemRB.IsChecked) FieldValue = ItemRB.Text;
            }
            return FieldValue;
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
            else if (VINEntry.Text.Length < 10 || VINEntry.Text.Length > 17)
            {
                VINErrorLabel.Text = "VIN должен содержать от 10 до 17 символов";
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
            adVM.SteeringWheel = GetterRB(SteeringWheelRB);

            string AdJson = JsonConvert.SerializeObject(adVM);
            CrossSettings.Current.AddOrUpdateValue("AdData", AdJson);
            await Navigation.PushAsync(new NavigationPage(new TwoAdPage()));
        }
    }
}