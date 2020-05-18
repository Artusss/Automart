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
    public partial class MainInfoEditPage : ContentPage
    {
        public static AdSQLiteHelper adSQLiteH;
        public static AdSQLiteHelper AdSQLiteH
        {
            get
            {
                if (adSQLiteH == null)
                {
                    adSQLiteH = new AdSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return adSQLiteH;
            }
        }

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

        public MainInfoEditPage()
        {
            InitializeComponent();

            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);

            VINEntry.Text     = AdVM.VIN;
            MileageEntry.Text = AdVM.Mileage.ToString();
            SetterRB(SteeringWheelRB, AdVM.SteeringWheel);
            foreach (var Mark in MarkSQLiteH.GetItems())
            {
                MarkPicker.Items.Add(Mark.Value); 
            }
            MarkPicker.SelectedIndex = MarkPicker.Items.IndexOf(AdVM.Mark);
            foreach (var Model in ModelSQLiteH.GetItems())
            {
                ModelPicker.Items.Add(Model.Value);
            }
            ModelPicker.SelectedIndex = ModelPicker.Items.IndexOf(AdVM.Model);
            foreach (var Kuzov in KuzovSQLiteH.GetItems())
            {
                KuzovPicker.Items.Add(Kuzov.Value);
            }
            KuzovPicker.SelectedIndex = KuzovPicker.Items.IndexOf(AdVM.Kuzov);
            foreach (var Year in YearSQLiteH.GetItems())
            {
                YearPicker.Items.Add(Year.Value);
            }
            YearPicker.SelectedIndex = YearPicker.Items.IndexOf(AdVM.Year);
            foreach (var Color in ColorSQLiteH.GetItems())
            {
                ColorPicker.Items.Add(Color.Value);
            }
            ColorPicker.SelectedIndex = ColorPicker.Items.IndexOf(AdVM.Color);
        }

        public void SetterRB(RadioButtonGroupView ItemsRB, string FieldValue)
        {
            foreach (var Item in ItemsRB.Children)
            {
                if (Item is Plugin.InputKit.Shared.Controls.RadioButton ItemRB && ItemRB.Text.Equals(FieldValue)) ItemRB.IsChecked = true;
            }
        }

        public string GetterRB(RadioButtonGroupView ItemsRB)
        {
            string FieldValue = "";
            foreach (var Item in ItemsRB.Children)
            {
                if (Item is Plugin.InputKit.Shared.Controls.RadioButton ItemRB && ItemRB.IsChecked) FieldValue = ItemRB.Text;
            }
            return FieldValue;
        }

        async void MainInfoSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);
            AdVM.VIN = VINEntry.Text;
            AdVM.Mark = MarkPicker.Items[MarkPicker.SelectedIndex];
            AdVM.Model = ModelPicker.Items[ModelPicker.SelectedIndex];
            AdVM.Year = YearPicker.Items[YearPicker.SelectedIndex];
            AdVM.Mileage = Convert.ToDouble(MileageEntry.Text);
            AdVM.Kuzov = KuzovPicker.Items[KuzovPicker.SelectedIndex];
            AdVM.Color = ColorPicker.Items[ColorPicker.SelectedIndex];
            AdVM.SteeringWheel = GetterRB(SteeringWheelRB);

            AdSQLiteH.SaveItem(AdVM);
            await DisplayAlert("", "Общая информация успешно сохранена", "OK");
            await Navigation.PushModalAsync(new NavigationPage(new AdPage()));
            return;
        }
    }
}