using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automart.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;
using Newtonsoft.Json;
using Xamarin.Forms.Internals;
using Plugin.InputKit.Shared.Controls;

namespace Automart.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdPage : ContentPage
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

        public static KomplektnostSQLiteHelper komplektnostSQLiteH;
        public static KomplektnostSQLiteHelper KomplektnostSQLiteH
        {
            get
            {
                if (komplektnostSQLiteH == null)
                {
                    komplektnostSQLiteH = new KomplektnostSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return komplektnostSQLiteH;
            }
        }
        public AdPage()
        {
            InitializeComponent();
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);

            InfoLabel.Text = $"Объявление № {CurrentAdId}";

            Label VINLabel = new Label
            {
                Text = $"VIN: {AdVM.VIN}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(VINLabel);

            Label TypeLabel = new Label
            {
                Text = $"Тип: {AdVM.Type}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(TypeLabel);

            Label MarkLabel = new Label
            {
                Text = $"Марка: {AdVM.Mark}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(MarkLabel);

            Label ModelLabel = new Label
            {
                Text = $"Модель: {AdVM.Model}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(ModelLabel);

            Label YearLabel = new Label
            {
                Text = $"Год выпуска: {AdVM.Year}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(YearLabel);

            Label MileageLabel = new Label
            {
                Text = $"Пробег: {AdVM.Mileage}, км",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(MileageLabel);

            Label KuzovLabel = new Label
            {
                Text = $"Кузов: {AdVM.Kuzov}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(KuzovLabel);

            Label ColorLabel = new Label
            {
                Text = $"Цвет: {AdVM.Color}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(ColorLabel);

            Label SteeringWheelLabel = new Label
            {
                Text = $"Руль: {AdVM.SteeringWheel}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            MainInfoSL.Children.Add(SteeringWheelLabel);

            Label DvigTypeLabel = new Label
            {
                Text = $"Тип двигателя: {AdVM.DvigType}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            DvigTransSL.Children.Add(DvigTypeLabel);

            Label KPPLabel = new Label
            {
                Text = $"КПП: {AdVM.KPP}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            DvigTransSL.Children.Add(KPPLabel);

            Label DriveUnitLabel = new Label
            {
                Text = $"Привод: {AdVM.DriveUnit}",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            DvigTransSL.Children.Add(DriveUnitLabel);

            Label VolumeLabel = new Label
            {
                Text = $"Объем: {AdVM.Volume}, л",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            DvigTransSL.Children.Add(VolumeLabel);

            Label PowerLabel = new Label
            {
                Text = $"Мощность: {AdVM.Power}, л.с.",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button))
            };
            DvigTransSL.Children.Add(PowerLabel);

            ////////////////////////////////////////////////////////////////////
            var komplektnostVM = KomplektnostSQLiteH.GetByAd(CurrentAdId);
            KeyCollectionEntry.Text       = Convert.ToString(komplektnostVM.KeyCollection);
            WheelCollectionEntry.Text     = Convert.ToString(komplektnostVM.WheelCollection);
            ExtraKomplektnostEditor.Text  = komplektnostVM.ExtraKomplektnost;
            PTSCheckBox.IsChecked         = komplektnostVM.PTS;
            RukovCheckBox.IsChecked       = komplektnostVM.Rukov;
            AptechkaCheckBox.IsChecked    = komplektnostVM.Aptechka;
            BoltKeyCheckBox.IsChecked     = komplektnostVM.BoltKey;
            ServiceBookCheckBox.IsChecked = komplektnostVM.ServiceBook;
            ToolsCheckBox.IsChecked       = komplektnostVM.Tools;
            FireExtCheckBox.IsChecked     = komplektnostVM.FireExt;
            JackCheckBox.IsChecked        = komplektnostVM.Jack;
            RegCertCheckBox.IsChecked     = komplektnostVM.RegCert;
            TriangleCheckBox.IsChecked    = komplektnostVM.Triangle;
            BaloonKeyCheckBox.IsChecked   = komplektnostVM.BaloonKey;
            CompressorCheckBox.IsChecked  = komplektnostVM.Compressor;
            foreach (var Zapaska in ZapaskaRB.Children)
            {
                if (Zapaska is RadioButton ZapaskaRB && ZapaskaRB.Text.Equals(komplektnostVM.Zapaska)) ZapaskaRB.IsChecked = true;
            }
            foreach (var MatCollection in MatCollectionRB.Children)
            {
                if (MatCollection is RadioButton MatCollectionRB && MatCollectionRB.Text.Equals(komplektnostVM.MatCollection)) MatCollectionRB.IsChecked = true;
            }
            ////////////////////////////////////////////////////////////////////

            this.Content = AdSL;
        }

        async void ToMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }
        
        async void KomplektnostSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            KomplektnostViewModel komplektnostVM = KomplektnostSQLiteH.GetByAd(CurrentAdId);
            if (komplektnostVM == null) return;
            komplektnostVM.KeyCollection     = Convert.ToInt32(KeyCollectionEntry.Text);
            komplektnostVM.WheelCollection   = Convert.ToInt32(WheelCollectionEntry.Text);
            foreach (var Zapaska in ZapaskaRB.Children)
            {
                if (Zapaska is RadioButton ZapaskaRB && ZapaskaRB.IsChecked) komplektnostVM.Zapaska = ZapaskaRB.Text;
            }
            foreach (var MatCollection in MatCollectionRB.Children)
            {
                if (MatCollection is RadioButton MatCollectionRB && MatCollectionRB.IsChecked) komplektnostVM.MatCollection = MatCollectionRB.Text;
            }
            komplektnostVM.ExtraKomplektnost = ExtraKomplektnostEditor.Text;
            komplektnostVM.PTS               = PTSCheckBox.IsChecked;
            komplektnostVM.Rukov             = RukovCheckBox.IsChecked;
            komplektnostVM.Aptechka          = AptechkaCheckBox.IsChecked;
            komplektnostVM.BoltKey           = BoltKeyCheckBox.IsChecked;
            komplektnostVM.ServiceBook       = ServiceBookCheckBox.IsChecked;
            komplektnostVM.Tools             = ToolsCheckBox.IsChecked;
            komplektnostVM.FireExt           = FireExtCheckBox.IsChecked;
            komplektnostVM.Jack              = JackCheckBox.IsChecked;
            komplektnostVM.RegCert           = RegCertCheckBox.IsChecked;
            komplektnostVM.Triangle          = TriangleCheckBox.IsChecked;
            komplektnostVM.BaloonKey         = BaloonKeyCheckBox.IsChecked;
            komplektnostVM.Compressor        = CompressorCheckBox.IsChecked;
            KomplektnostSQLiteH.SaveItem(komplektnostVM);
            await DisplayAlert("", "Комплектность успешно сохранена", "OK");
            return;
        }

        async void KomplektacyaSaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }

        void MainInfoTapButton_Clicked(object sender, EventArgs e)
        {
            if (MainInfoSL.IsVisible)
            {
                MainInfoSL.IsVisible = false;
                return;
            }
            MainInfoSL.IsVisible = true;
        }
        void DvigTransTapButton_Clicked(object sender, EventArgs e)
        {
            if (DvigTransSL.IsVisible)
            {
                DvigTransSL.IsVisible = false;
                return;
            }
            DvigTransSL.IsVisible = true;
        }

        void KomplektnostTapButton_Clicked(object sender, EventArgs e)
        {
            if (KomplektnostSL.IsVisible)
            {
                KomplektnostSL.IsVisible = false;
                return;
            }
            KomplektnostSL.IsVisible = true;
        }
        void KomplektacyaTapButton_Clicked(object sender, EventArgs e)
        {
            if (KomplektacyaSL.IsVisible)
            {
                KomplektacyaSL.IsVisible = false;
                return;
            }
            KomplektacyaSL.IsVisible = true;
        }
        void CommentsTapButton_Clicked(object sender, EventArgs e)
        {
            if (CommentsSL.IsVisible)
            {
                CommentsSL.IsVisible = false;
                return;
            }
            CommentsSL.IsVisible = true;
        }
        void SafetyTapButton_Clicked(object sender, EventArgs e)
        {
            if (SafetySL.IsVisible)
            {
                SafetySL.IsVisible = false;
                return;
            }
            SafetySL.IsVisible = true;
        }
        void LightingTapButton_Clicked(object sender, EventArgs e)
        {
            if (LightingSL.IsVisible)
            {
                LightingSL.IsVisible = false;
                return;
            }
            LightingSL.IsVisible = true;
        }
        void HeatingTapButton_Clicked(object sender, EventArgs e)
        {
            if (HeatingSL.IsVisible)
            {
                HeatingSL.IsVisible = false;
                return;
            }
            HeatingSL.IsVisible = true;
        }
        void СomfortTapButton_Clicked(object sender, EventArgs e)
        {
            if (СomfortSL.IsVisible)
            {
                СomfortSL.IsVisible = false;
                return;
            }
            СomfortSL.IsVisible = true;
        }
        void ExteriorTapButton_Clicked(object sender, EventArgs e)
        {
            if (ExteriorSL.IsVisible)
            {
                ExteriorSL.IsVisible = false;
                return;
            }
            ExteriorSL.IsVisible = true;
        }
        void SecuritySysTapButton_Clicked(object sender, EventArgs e)
        {
            if (SecuritySysSL.IsVisible)
            {
                SecuritySysSL.IsVisible = false;
                return;
            }
            SecuritySysSL.IsVisible = true;
        }
        void AdjustmentsTapButton_Clicked(object sender, EventArgs e)
        {
            if (AdjustmentsSL.IsVisible)
            {
                AdjustmentsSL.IsVisible = false;
                return;
            }
            AdjustmentsSL.IsVisible = true;
        }
        void InteriorTapButton_Clicked(object sender, EventArgs e)
        {
            if (InteriorSL.IsVisible)
            {
                InteriorSL.IsVisible = false;
                return;
            }
            InteriorSL.IsVisible = true;
        }
        void ExtraKomplektacyaTapButton_Clicked(object sender, EventArgs e)
        {
            if (ExtraKomplektacyaSL.IsVisible)
            {
                ExtraKomplektacyaSL.IsVisible = false;
                return;
            }
            ExtraKomplektacyaSL.IsVisible = true;
        }
    }
}