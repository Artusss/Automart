using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automart.ViewModels;
using Automart.ViewModels.Sctructs;
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

        public static KomplektacyaSQLiteHelper komplektacyaSQLiteH;
        public static KomplektacyaSQLiteHelper KomplektacyaSQLiteH
        {
            get
            {
                if (komplektacyaSQLiteH == null)
                {
                    komplektacyaSQLiteH = new KomplektacyaSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return komplektacyaSQLiteH;
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
            SetterRB(ZapaskaRB, komplektnostVM.Zapaska);
            SetterRB(MatCollectionRB, komplektnostVM.MatCollection);

            var komplektacyaVM                               = KomplektacyaSQLiteH.GetByAd(CurrentAdId);

            Safety safety                                    = JsonConvert.DeserializeObject<Safety>(komplektacyaVM.Safety);
            TractionControlSysCheckBox.IsChecked             = safety.TractionControlSys;
            AntiLockSysCheckBox.IsChecked                    = safety.AntiLockSys;
            CursStableCheckBox.IsChecked                     = safety.CursStable;
            DifBlockSysCheckBox.IsChecked                    = safety.DifBlockSys;
            BrakeForceDistSysCheckBox.IsChecked              = safety.BrakeForceDistSys;
            ParkHelperSysCheckBox.IsChecked                  = safety.ParkHelperSys;
            DeadZoneContrCheckBox.IsChecked                  = safety.DeadZoneContr;
            AdjacentStripSysCheckBox.IsChecked               = safety.AdjacentStripSys;
            VoditelAirBagCheckBox.IsChecked                  = safety.VoditelAirBag;
            FrontPassAirBagCheckBox.IsChecked                = safety.FrontPassAirBag;
            BackPassAirBagCheckBox.IsChecked                 = safety.BackPassAirBag;
            SideAirBagCheckBox.IsChecked                     = safety.SideAirBag;
            CurtainsAirBagCheckBox.IsChecked                 = safety.CurtainsAirBag;
            FrontParkSensCheckBox.IsChecked                  = safety.FrontParkSens;
            BackParkSensCheckBox.IsChecked                   = safety.BackParkSens;

            Lightning lightning                              = JsonConvert.DeserializeObject<Lightning>(komplektacyaVM.Lightning);
            SetterRB(LightsTypeRB, lightning.LightsType);
            FogLightsCheckBox.IsChecked                      = lightning.FogLights;
            DayRunLightsCheckBox.IsChecked                   = lightning.DayRunLights;
            HeadlightWashersCheckBox.IsChecked               = lightning.HeadlightWashers;
            HeadlightRimLightsCheckBox.IsChecked             = lightning.HeadlightRimLights;

            Heating heating                                  = JsonConvert.DeserializeObject<Heating>(komplektacyaVM.Heating);
            MirrorHeatingCheckBox.IsChecked                  = heating.Mirror;
            StearingWheelHeatingCheckBox.IsChecked           = heating.StearingWheel;
            FrontSeatHeatingCheckBox.IsChecked               = heating.FrontSeat;
            BackSeatHeatingCheckBox.IsChecked                = heating.BackSeat;
            BackGlassHeatingCheckBox.IsChecked               = heating.BackGlass;
            RelaxGlassHeatingCheckBox.IsChecked              = heating.RelaxGlass;
            NozzlesGlassHeatingCheckBox.IsChecked            = heating.NozzlesGlass;
            FrontGlassHeatingCheckBox.IsChecked              = heating.FrontGlass;
            PreheaterCheckBox.IsChecked                      = heating.Preheater;
            EngineHeaterCheckBox.IsChecked                   = heating.EngineHeater;

            Comfort comfort                                  = JsonConvert.DeserializeObject<Comfort>(komplektacyaVM.Comfort);
            FrontPowerWindowCheckBox.IsChecked               = comfort.FrontPowerWindow;
            BackPowerWindowCheckBox.IsChecked                = comfort.BackPowerWindow;
            DoorCurtainsPowerWindowCheckBox.IsChecked        = comfort.DoorCurtainsPowerWindow;
            RearWindowBlindPowerWindowCheckBox.IsChecked     = comfort.RearWindowBlindPowerWindow;
            SetterRB(PowerSteeringRB, comfort.PowerSteering);
            RainSensorCheckBox.IsChecked                     = comfort.RainSensor;
            LightSensorCheckBox.IsChecked                    = comfort.LightSensor;
            SetterRB(ClimatRB, comfort.Climat);
            FrontSeatVentCheckBox.IsChecked                  = comfort.FrontSeatVent;
            BackSeatVentCheckBox.IsChecked                   = comfort.BackSeatVent;
            CruiseControlCheckBox.IsChecked                  = comfort.CruiseControl;
            CassetteMultimediaCheckBox.IsChecked             = comfort.CassetteMultimedia;
            CDMultimediaCheckBox.IsChecked                   = comfort.CDMultimedia;
            DVDMultimediaCheckBox.IsChecked                  = comfort.DVDMultimedia;
            USBMultimediaCheckBox.IsChecked                  = comfort.USBMultimedia;
            AUXMultimediaCheckBox.IsChecked                  = comfort.AUXMultimedia;
            NavSysCheckBox.IsChecked                         = comfort.NavSys;
            OnBoardCompCheckBox.IsChecked                    = comfort.OnBoardComp;
            FrontCameraCheckBox.IsChecked                    = comfort.FrontCamera;
            BackCameraCheckBox.IsChecked                     = comfort.BackCamera;
            SideInMirrorsCameraCheckBox.IsChecked            = comfort.SideInMirrorsCamera;

            Exterior exterior                                = JsonConvert.DeserializeObject<Exterior>(komplektacyaVM.Exterior);
            AlloyWheelsCheckBox.IsChecked                    = exterior.AlloyWheels;
            FrontTintedGlassCheckBox.IsChecked               = exterior.FrontTintedGlass;
            BackTintedGlassCheckBox.IsChecked                = exterior.BackTintedGlass;
            HitchCheckBox.IsChecked                          = exterior.Hitch;
            RoofRailsCheckBox.IsChecked                      = exterior.RoofRails;
            TrunkCheckBox.IsChecked                          = exterior.Trunk;

            SecuritySys securitySys                          = JsonConvert.DeserializeObject<SecuritySys>(komplektacyaVM.SecuritySys);
            CentralLockingCheckBox.IsChecked                 = securitySys.CentralLocking;
            SignalingCheckBox.IsChecked                      = securitySys.Signaling;
            KeylessAccessCheckBox.IsChecked                  = securitySys.KeylessAccess;

            Adjustments adjustments                          = JsonConvert.DeserializeObject<Adjustments>(komplektacyaVM.Adjustments);
            HeightDriverSeatAdjustmentCheckBox.IsChecked     = adjustments.HeightDriverSeat;
            ElectricDriverSeatAdjustmentCheckBox.IsChecked   = adjustments.ElectricDriverSeat;
            MemoryDriverSeatAdjustmentCheckBox.IsChecked     = adjustments.MemoryDriverSeat;
            HeightPassSeatAdjustmentCheckBox.IsChecked       = adjustments.HeightPassSeat;
            ElectricPassSeatAdjustmentCheckBox.IsChecked     = adjustments.ElectricPassSeat;
            MemoryPassSeatAdjustmentCheckBox.IsChecked       = adjustments.MemoryPassSeat;
            ElectricBackPassSeatAdjustmentCheckBox.IsChecked = adjustments.ElectricBackPassSeat;
            SetterRB(SteeringWheelAdjustmentRB, adjustments.SteeringWheel);

            Interior interior                                = JsonConvert.DeserializeObject<Interior>(komplektacyaVM.Interior);
            SetterRB(UpholsteryRB, interior.Upholstery);
            SetterRB(InteriorColorRB, interior.InteriorColor);
            SetterRB(LukeRB, interior.Luke);
            LeatherSteeringWheelCheckBox.IsChecked           = interior.LeatherSteeringWheel;

            ExtraKomplektacya extraKomplektacya              = JsonConvert.DeserializeObject<ExtraKomplektacya>(komplektacyaVM.ExtraKomplektacya);
            GasCylinderEquipCheckBox.IsChecked               = extraKomplektacya.GasCylinderEquip;
            ElectricMirrorsCheckBox.IsChecked                = extraKomplektacya.ElectricMirrors;
            EngineAutoStartCheckBox.IsChecked                = extraKomplektacya.EngineAutoStart;
            AirSuspensionCheckBox.IsChecked                  = extraKomplektacya.AirSuspension;
            DoorClosersCheckBox.IsChecked                    = extraKomplektacya.DoorClosers;
            ////////////////////////////////////////////////////////////////////

            this.Content = AdSL;
        }

        public void SetterRB(RadioButtonGroupView ItemsRB, string FieldValue)
        {
            foreach (var Item in ItemsRB.Children)
            {
                if (Item is RadioButton ItemRB && ItemRB.Text.Equals(FieldValue)) ItemRB.IsChecked = true;
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
            komplektnostVM.Zapaska           = GetterRB(ZapaskaRB);
            komplektnostVM.MatCollection     = GetterRB(MatCollectionRB);
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
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            KomplektacyaViewModel komplektacyaVM = KomplektacyaSQLiteH.GetByAd(CurrentAdId);
            if (komplektacyaVM == null) return;
            // Add to cur fields here
            await DisplayAlert("", "Комплектность успешно сохранена", "OK");
            return;
        }

        void MainInfoTapButton_Clicked(object sender, EventArgs e)
        {
            MainInfoSL.IsVisible = MainInfoSL.IsVisible ? false : true;
        }
        void DvigTransTapButton_Clicked(object sender, EventArgs e)
        {
            DvigTransSL.IsVisible = DvigTransSL.IsVisible ? false : true;
        }
        void KomplektnostTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektnostSL.IsVisible = KomplektnostSL.IsVisible ? false : true;
        }
        void KomplektacyaTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible            = false;
            LightingSL.IsVisible          = false;
            HeatingSL.IsVisible           = false;
            СomfortSL.IsVisible           = false;
            ExteriorSL.IsVisible          = false;
            SecuritySysSL.IsVisible       = false;
            AdjustmentsSL.IsVisible       = false;
            InteriorSL.IsVisible          = false;
            ExtraKomplektacyaSL.IsVisible = false;
            KomplektacyaSL.IsVisible      = KomplektacyaSL.IsVisible ? false : true;
        }
        void CommentsTapButton_Clicked(object sender, EventArgs e)
        {
            CommentsSL.IsVisible = CommentsSL.IsVisible ? false : true;
        }
        void SafetyTapButton_Clicked(object sender, EventArgs e)
        {
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            SafetySL.IsVisible = SafetySL.IsVisible ? false : true;
        }
        void LightingTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            LightingSL.IsVisible = LightingSL.IsVisible ? false : true;
        }
        void HeatingTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            HeatingSL.IsVisible = HeatingSL.IsVisible ? false : true;
        }
        void СomfortTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            СomfortSL.IsVisible = СomfortSL.IsVisible ? false : true;
        }
        void ExteriorTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            ExteriorSL.IsVisible = ExteriorSL.IsVisible ? false : true;
        }
        void SecuritySysTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            SecuritySysSL.IsVisible = SecuritySysSL.IsVisible ? false : true;
        }
        void AdjustmentsTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            AdjustmentsSL.IsVisible = AdjustmentsSL.IsVisible ? false : true;
        }
        void InteriorTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = false;
            InteriorSL.IsVisible = InteriorSL.IsVisible ? false : true;
        }
        void ExtraKomplektacyaTapButton_Clicked(object sender, EventArgs e)
        {
            SafetySL.IsVisible = false;
            LightingSL.IsVisible = false;
            HeatingSL.IsVisible = false;
            СomfortSL.IsVisible = false;
            ExteriorSL.IsVisible = false;
            SecuritySysSL.IsVisible = false;
            AdjustmentsSL.IsVisible = false;
            InteriorSL.IsVisible = false;
            ExtraKomplektacyaSL.IsVisible = ExtraKomplektacyaSL.IsVisible ? false : true;
        }
    }
}