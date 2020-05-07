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
using Plugin.Media;
using Plugin.Media.Abstractions;

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
            string curUserCity = JsonConvert.DeserializeObject<UserViewModel>(
                CrossSettings.Current.GetValueOrDefault("current_user", null)
            ).City;
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);

            InfoLabel.Text       = $"{AdVM.Mark} {AdVM.Model} ({AdVM.Power} л.с.) {AdVM.DvigType} {AdVM.DriveUnit} {AdVM.Year} год {AdVM.Mileage} км";
            CityLabel.Text       = $"Город: {curUserCity}";
            TransTypeLabel.Text  = $"Тип транспорта: {AdVM.Type}";
            VINiLabel.Text       = $"VIN: {AdVM.VIN}";

            VINEntry.Text           = AdVM.VIN;
            TypeEntry.Text          = AdVM.Type;
            MarkEntry.Text          = AdVM.Mark;
            ModelEntry.Text         = AdVM.Model;
            YearEntry.Text          = AdVM.Year;
            MileageEntry.Text       = AdVM.Mileage.ToString();
            KuzovEntry.Text         = AdVM.Kuzov;
            ColorEntry.Text         = AdVM.Color;
            SteeringWheelEntry.Text = AdVM.SteeringWheel;

            DvigTypeEntry.Text  = AdVM.DvigType;
            KPPEntry.Text       = AdVM.KPP;
            DriveUnitEntry.Text = AdVM.DriveUnit;
            VolumeEntry.Text    = AdVM.Volume.ToString();
            PowerEntry.Text     = AdVM.Power.ToString();

            var komplektnostVM            = KomplektnostSQLiteH.GetByAd(CurrentAdId);

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

            var komplektacyaVM                   = KomplektacyaSQLiteH.GetByAd(CurrentAdId);

            Safety safety                        = JsonConvert.DeserializeObject<Safety>(komplektacyaVM.Safety);
            TractionControlSysCheckBox.IsChecked = safety.TractionControlSys;
            AntiLockSysCheckBox.IsChecked        = safety.AntiLockSys;
            CursStableCheckBox.IsChecked         = safety.CursStable;
            DifBlockSysCheckBox.IsChecked        = safety.DifBlockSys;
            BrakeForceDistSysCheckBox.IsChecked  = safety.BrakeForceDistSys;
            ParkHelperSysCheckBox.IsChecked      = safety.ParkHelperSys;
            DeadZoneContrCheckBox.IsChecked      = safety.DeadZoneContr;
            AdjacentStripSysCheckBox.IsChecked   = safety.AdjacentStripSys;
            VoditelAirBagCheckBox.IsChecked      = safety.VoditelAirBag;
            FrontPassAirBagCheckBox.IsChecked    = safety.FrontPassAirBag;
            BackPassAirBagCheckBox.IsChecked     = safety.BackPassAirBag;
            SideAirBagCheckBox.IsChecked         = safety.SideAirBag;
            CurtainsAirBagCheckBox.IsChecked     = safety.CurtainsAirBag;
            FrontParkSensCheckBox.IsChecked      = safety.FrontParkSens;
            BackParkSensCheckBox.IsChecked       = safety.BackParkSens;

            Lightning lightning                  = JsonConvert.DeserializeObject<Lightning>(komplektacyaVM.Lightning);
            SetterRB(LightsTypeRB, lightning.LightsType);
            FogLightsCheckBox.IsChecked          = lightning.FogLights;
            DayRunLightsCheckBox.IsChecked       = lightning.DayRunLights;
            HeadlightWashersCheckBox.IsChecked   = lightning.HeadlightWashers;
            HeadlightRimLightsCheckBox.IsChecked = lightning.HeadlightRimLights;

            Heating heating                        = JsonConvert.DeserializeObject<Heating>(komplektacyaVM.Heating);
            MirrorHeatingCheckBox.IsChecked        = heating.Mirror;
            StearingWheelHeatingCheckBox.IsChecked = heating.StearingWheel;
            FrontSeatHeatingCheckBox.IsChecked     = heating.FrontSeat;
            BackSeatHeatingCheckBox.IsChecked      = heating.BackSeat;
            BackGlassHeatingCheckBox.IsChecked     = heating.BackGlass;
            RelaxGlassHeatingCheckBox.IsChecked    = heating.RelaxGlass;
            NozzlesGlassHeatingCheckBox.IsChecked  = heating.NozzlesGlass;
            FrontGlassHeatingCheckBox.IsChecked    = heating.FrontGlass;
            PreheaterCheckBox.IsChecked            = heating.Preheater;
            EngineHeaterCheckBox.IsChecked         = heating.EngineHeater;

            Comfort comfort                              = JsonConvert.DeserializeObject<Comfort>(komplektacyaVM.Comfort);
            FrontPowerWindowCheckBox.IsChecked           = comfort.FrontPowerWindow;
            BackPowerWindowCheckBox.IsChecked            = comfort.BackPowerWindow;
            DoorCurtainsPowerWindowCheckBox.IsChecked    = comfort.DoorCurtainsPowerWindow;
            RearWindowBlindPowerWindowCheckBox.IsChecked = comfort.RearWindowBlindPowerWindow;
            SetterRB(PowerSteeringRB, comfort.PowerSteering);
            RainSensorCheckBox.IsChecked                 = comfort.RainSensor;
            LightSensorCheckBox.IsChecked                = comfort.LightSensor;
            SetterRB(ClimatRB, comfort.Climat);
            FrontSeatVentCheckBox.IsChecked              = comfort.FrontSeatVent;
            BackSeatVentCheckBox.IsChecked               = comfort.BackSeatVent;
            CruiseControlCheckBox.IsChecked              = comfort.CruiseControl;
            CassetteMultimediaCheckBox.IsChecked         = comfort.CassetteMultimedia;
            CDMultimediaCheckBox.IsChecked               = comfort.CDMultimedia;
            DVDMultimediaCheckBox.IsChecked              = comfort.DVDMultimedia;
            USBMultimediaCheckBox.IsChecked              = comfort.USBMultimedia;
            AUXMultimediaCheckBox.IsChecked              = comfort.AUXMultimedia;
            NavSysCheckBox.IsChecked                     = comfort.NavSys;
            OnBoardCompCheckBox.IsChecked                = comfort.OnBoardComp;
            FrontCameraCheckBox.IsChecked                = comfort.FrontCamera;
            BackCameraCheckBox.IsChecked                 = comfort.BackCamera;
            SideInMirrorsCameraCheckBox.IsChecked        = comfort.SideInMirrorsCamera;

            Exterior exterior                  = JsonConvert.DeserializeObject<Exterior>(komplektacyaVM.Exterior);
            AlloyWheelsCheckBox.IsChecked      = exterior.AlloyWheels;
            FrontTintedGlassCheckBox.IsChecked = exterior.FrontTintedGlass;
            BackTintedGlassCheckBox.IsChecked  = exterior.BackTintedGlass;
            HitchCheckBox.IsChecked            = exterior.Hitch;
            RoofRailsCheckBox.IsChecked        = exterior.RoofRails;
            TrunkCheckBox.IsChecked            = exterior.Trunk;

            SecuritySys securitySys          = JsonConvert.DeserializeObject<SecuritySys>(komplektacyaVM.SecuritySys);
            CentralLockingCheckBox.IsChecked = securitySys.CentralLocking;
            SignalingCheckBox.IsChecked      = securitySys.Signaling;
            KeylessAccessCheckBox.IsChecked  = securitySys.KeylessAccess;

            Adjustments adjustments                          = JsonConvert.DeserializeObject<Adjustments>(komplektacyaVM.Adjustments);
            HeightDriverSeatAdjustmentCheckBox.IsChecked     = adjustments.HeightDriverSeat;
            ElectricDriverSeatAdjustmentCheckBox.IsChecked   = adjustments.ElectricDriverSeat;
            MemoryDriverSeatAdjustmentCheckBox.IsChecked     = adjustments.MemoryDriverSeat;
            HeightPassSeatAdjustmentCheckBox.IsChecked       = adjustments.HeightPassSeat;
            ElectricPassSeatAdjustmentCheckBox.IsChecked     = adjustments.ElectricPassSeat;
            MemoryPassSeatAdjustmentCheckBox.IsChecked       = adjustments.MemoryPassSeat;
            ElectricBackPassSeatAdjustmentCheckBox.IsChecked = adjustments.ElectricBackPassSeat;
            SetterRB(SteeringWheelAdjustmentRB, adjustments.SteeringWheel);

            Interior interior                      = JsonConvert.DeserializeObject<Interior>(komplektacyaVM.Interior);
            SetterRB(UpholsteryRB, interior.Upholstery);
            SetterRB(InteriorColorRB, interior.InteriorColor);
            SetterRB(LukeRB, interior.Luke);
            LeatherSteeringWheelCheckBox.IsChecked = interior.LeatherSteeringWheel;

            ExtraKomplektacya extraKomplektacya = JsonConvert.DeserializeObject<ExtraKomplektacya>(komplektacyaVM.ExtraKomplektacya);
            GasCylinderEquipCheckBox.IsChecked  = extraKomplektacya.GasCylinderEquip;
            ElectricMirrorsCheckBox.IsChecked   = extraKomplektacya.ElectricMirrors;
            EngineAutoStartCheckBox.IsChecked   = extraKomplektacya.EngineAutoStart;
            AirSuspensionCheckBox.IsChecked     = extraKomplektacya.AirSuspension;
            DoorClosersCheckBox.IsChecked       = extraKomplektacya.DoorClosers;
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

        async void ToMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
        }
        
        async void KomplektnostSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            KomplektnostViewModel komplektnostVM = KomplektnostSQLiteH.GetByAd(CurrentAdId);
            if (komplektnostVM == null)
            {
                await DisplayAlert("", "Не удалось сохранить", "Cancel");
                return;
            }
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
        async void MainInfoSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);
            AdVM.VIN           = VINEntry.Text;
            AdVM.Type          = TypeEntry.Text;
            AdVM.Mark          = MarkEntry.Text;
            AdVM.Model         = ModelEntry.Text;
            AdVM.Year          = YearEntry.Text;
            AdVM.Mileage       = Convert.ToDouble(MileageEntry.Text);
            AdVM.Kuzov         = KuzovEntry.Text;
            AdVM.Color         = ColorEntry.Text;
            AdVM.SteeringWheel = SteeringWheelEntry.Text;

            AdSQLiteH.SaveItem(AdVM);
            await DisplayAlert("", "Общая информация успешно сохранена", "OK");

            VINEntry.IsEnabled           = false;
            TypeEntry.IsEnabled          = false;
            MarkEntry.IsEnabled          = false;
            ModelEntry.IsEnabled         = false;
            YearEntry.IsEnabled          = false;
            MileageEntry.IsEnabled       = false;
            KuzovEntry.IsEnabled         = false;
            ColorEntry.IsEnabled         = false;
            SteeringWheelEntry.IsEnabled = false;

            MainInfoEditButton.IsVisible = true;
            MainInfoSaveButton.IsVisible = false;
            return;
        }
        void MainInfoEditButton_Clicked(object sender, EventArgs e)
        {
            VINEntry.IsEnabled           = true;
            TypeEntry.IsEnabled          = true;
            MarkEntry.IsEnabled          = true;
            ModelEntry.IsEnabled         = true;
            YearEntry.IsEnabled          = true;
            MileageEntry.IsEnabled       = true;
            KuzovEntry.IsEnabled         = true;
            ColorEntry.IsEnabled         = true;
            SteeringWheelEntry.IsEnabled = true;

            MainInfoEditButton.IsVisible = false;
            MainInfoSaveButton.IsVisible = true;
        }
        async void DvigTransSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            var AdVM = AdSQLiteH.GetById(CurrentAdId);
            AdVM.DvigType  = DvigTypeEntry.Text;
            AdVM.KPP       = KPPEntry.Text;
            AdVM.DriveUnit = DriveUnitEntry.Text;
            AdVM.Volume    = Convert.ToDouble(VolumeEntry.Text);
            AdVM.Power     = Convert.ToDouble(PowerEntry.Text);

            AdSQLiteH.SaveItem(AdVM);
            await DisplayAlert("", "Двигатель и трансмиссия успешно сохранена", "OK");

            DvigTypeEntry.IsEnabled       = false;
            KPPEntry.IsEnabled            = false;
            DriveUnitEntry.IsEnabled      = false;
            VolumeEntry.IsEnabled         = false;
            PowerEntry.IsEnabled          = false;

            DvigTransEditButton.IsVisible = true;
            DvigTransSaveButton.IsVisible = false;
            return;
        }
        void DvigTransEditButton_Clicked(object sender, EventArgs e)
        {
            DvigTypeEntry.IsEnabled       = true;
            KPPEntry.IsEnabled            = true;
            DriveUnitEntry.IsEnabled      = true;
            VolumeEntry.IsEnabled         = true;
            PowerEntry.IsEnabled          = true;

            DvigTransEditButton.IsVisible = false;
            DvigTransSaveButton.IsVisible = true;
        }

        async void KomplektacyaSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            KomplektacyaViewModel komplektacyaVM = KomplektacyaSQLiteH.GetByAd(CurrentAdId);
            if (komplektacyaVM == null)
            {
                await DisplayAlert("", "Не удалось сохранить", "Cancel");
                return; 
            }

            Safety safety             = JsonConvert.DeserializeObject<Safety>(komplektacyaVM.Safety);
            safety.TractionControlSys = TractionControlSysCheckBox.IsChecked;
            safety.AntiLockSys        = AntiLockSysCheckBox.IsChecked;
            safety.CursStable         = CursStableCheckBox.IsChecked;
            safety.DifBlockSys        = DifBlockSysCheckBox.IsChecked;
            safety.BrakeForceDistSys  = BrakeForceDistSysCheckBox.IsChecked;
            safety.ParkHelperSys      = ParkHelperSysCheckBox.IsChecked;
            safety.DeadZoneContr      = DeadZoneContrCheckBox.IsChecked;
            safety.AdjacentStripSys   = AdjacentStripSysCheckBox.IsChecked;
            safety.VoditelAirBag      = VoditelAirBagCheckBox.IsChecked;
            safety.FrontPassAirBag    = FrontPassAirBagCheckBox.IsChecked;
            safety.BackPassAirBag     = BackPassAirBagCheckBox.IsChecked;
            safety.SideAirBag         = SideAirBagCheckBox.IsChecked;
            safety.CurtainsAirBag     = CurtainsAirBagCheckBox.IsChecked;
            safety.FrontParkSens      = FrontParkSensCheckBox.IsChecked;
            safety.BackParkSens       = BackParkSensCheckBox.IsChecked;
            komplektacyaVM.Safety     = JsonConvert.SerializeObject(safety);

            Lightning lightning          = JsonConvert.DeserializeObject<Lightning>(komplektacyaVM.Lightning);
            lightning.LightsType         = GetterRB(LightsTypeRB);
            lightning.FogLights          = FogLightsCheckBox.IsChecked;
            lightning.DayRunLights       = DayRunLightsCheckBox.IsChecked;
            lightning.HeadlightWashers   = HeadlightWashersCheckBox.IsChecked;
            lightning.HeadlightRimLights = HeadlightRimLightsCheckBox.IsChecked;
            komplektacyaVM.Lightning     = JsonConvert.SerializeObject(lightning);

            Heating heating        = JsonConvert.DeserializeObject<Heating>(komplektacyaVM.Heating);
            heating.Mirror         = MirrorHeatingCheckBox.IsChecked;
            heating.StearingWheel  = StearingWheelHeatingCheckBox.IsChecked;
            heating.FrontSeat      = FrontSeatHeatingCheckBox.IsChecked;
            heating.BackSeat       = BackSeatHeatingCheckBox.IsChecked;
            heating.BackGlass      = BackGlassHeatingCheckBox.IsChecked;
            heating.RelaxGlass     = RelaxGlassHeatingCheckBox.IsChecked;
            heating.NozzlesGlass   = NozzlesGlassHeatingCheckBox.IsChecked;
            heating.FrontGlass     = FrontGlassHeatingCheckBox.IsChecked;
            heating.Preheater      = PreheaterCheckBox.IsChecked;
            heating.EngineHeater   = EngineHeaterCheckBox.IsChecked;
            komplektacyaVM.Heating = JsonConvert.SerializeObject(heating);

            Comfort comfort                    = JsonConvert.DeserializeObject<Comfort>(komplektacyaVM.Comfort);
            comfort.FrontPowerWindow           = FrontPowerWindowCheckBox.IsChecked;
            comfort.BackPowerWindow            = BackPowerWindowCheckBox.IsChecked;
            comfort.DoorCurtainsPowerWindow    = DoorCurtainsPowerWindowCheckBox.IsChecked;
            comfort.RearWindowBlindPowerWindow = RearWindowBlindPowerWindowCheckBox.IsChecked;
            comfort.PowerSteering              = GetterRB(PowerSteeringRB);
            comfort.RainSensor                 = RainSensorCheckBox.IsChecked;
            comfort.LightSensor                = LightSensorCheckBox.IsChecked;
            comfort.Climat                     = GetterRB(ClimatRB);
            comfort.FrontSeatVent              = FrontSeatVentCheckBox.IsChecked;
            comfort.BackSeatVent               = BackSeatVentCheckBox.IsChecked;
            comfort.CruiseControl              = CruiseControlCheckBox.IsChecked;
            comfort.CassetteMultimedia         = CassetteMultimediaCheckBox.IsChecked;
            comfort.CDMultimedia               = CDMultimediaCheckBox.IsChecked;
            comfort.DVDMultimedia              = DVDMultimediaCheckBox.IsChecked;
            comfort.USBMultimedia              = USBMultimediaCheckBox.IsChecked;
            comfort.AUXMultimedia              = AUXMultimediaCheckBox.IsChecked;
            comfort.NavSys                     = NavSysCheckBox.IsChecked;
            comfort.OnBoardComp                = OnBoardCompCheckBox.IsChecked;
            comfort.FrontCamera                = FrontCameraCheckBox.IsChecked;
            comfort.BackCamera                 = BackCameraCheckBox.IsChecked;
            comfort.SideInMirrorsCamera        = SideInMirrorsCameraCheckBox.IsChecked;
            komplektacyaVM.Comfort             = JsonConvert.SerializeObject(comfort);

            Exterior exterior         = JsonConvert.DeserializeObject<Exterior>(komplektacyaVM.Exterior);
            exterior.AlloyWheels      = AlloyWheelsCheckBox.IsChecked;
            exterior.FrontTintedGlass = FrontTintedGlassCheckBox.IsChecked;
            exterior.BackTintedGlass  = BackTintedGlassCheckBox.IsChecked;
            exterior.Hitch            = HitchCheckBox.IsChecked;
            exterior.RoofRails        = RoofRailsCheckBox.IsChecked;
            exterior.Trunk            = TrunkCheckBox.IsChecked;
            komplektacyaVM.Exterior   = JsonConvert.SerializeObject(exterior);

            SecuritySys securitySys    = JsonConvert.DeserializeObject<SecuritySys>(komplektacyaVM.SecuritySys);
            securitySys.CentralLocking = CentralLockingCheckBox.IsChecked;
            securitySys.Signaling      = SignalingCheckBox.IsChecked;
            securitySys.KeylessAccess  = KeylessAccessCheckBox.IsChecked;
            komplektacyaVM.SecuritySys = JsonConvert.SerializeObject(securitySys);

            Adjustments adjustments          = JsonConvert.DeserializeObject<Adjustments>(komplektacyaVM.Adjustments);
            adjustments.HeightDriverSeat     = HeightDriverSeatAdjustmentCheckBox.IsChecked ;
            adjustments.ElectricDriverSeat   = ElectricDriverSeatAdjustmentCheckBox.IsChecked ;
            adjustments.MemoryDriverSeat     = MemoryDriverSeatAdjustmentCheckBox.IsChecked ;
            adjustments.HeightPassSeat       = HeightPassSeatAdjustmentCheckBox.IsChecked ;
            adjustments.ElectricPassSeat     = ElectricPassSeatAdjustmentCheckBox.IsChecked ;
            adjustments.MemoryPassSeat       = MemoryPassSeatAdjustmentCheckBox.IsChecked ;
            adjustments.ElectricBackPassSeat = ElectricBackPassSeatAdjustmentCheckBox.IsChecked ;
            adjustments.SteeringWheel        = GetterRB(SteeringWheelAdjustmentRB);
            komplektacyaVM.Adjustments       = JsonConvert.SerializeObject(adjustments);

            Interior interior             = JsonConvert.DeserializeObject<Interior>(komplektacyaVM.Interior);
            interior.Upholstery           = GetterRB(UpholsteryRB);
            interior.InteriorColor        = GetterRB(InteriorColorRB);
            interior.Luke                 = GetterRB(LukeRB);
            interior.LeatherSteeringWheel = LeatherSteeringWheelCheckBox.IsChecked;
            komplektacyaVM.Interior       = JsonConvert.SerializeObject(interior);

            ExtraKomplektacya extraKomplektacya = JsonConvert.DeserializeObject<ExtraKomplektacya>(komplektacyaVM.ExtraKomplektacya);
            extraKomplektacya.GasCylinderEquip  = GasCylinderEquipCheckBox.IsChecked ;
            extraKomplektacya.ElectricMirrors   = ElectricMirrorsCheckBox.IsChecked ;
            extraKomplektacya.EngineAutoStart   = EngineAutoStartCheckBox.IsChecked ;
            extraKomplektacya.AirSuspension     = AirSuspensionCheckBox.IsChecked ;
            extraKomplektacya.DoorClosers       = DoorClosersCheckBox.IsChecked ;
            komplektacyaVM.ExtraKomplektacya    = JsonConvert.SerializeObject(extraKomplektacya);

            KomplektacyaSQLiteH.SaveItem(komplektacyaVM);
            await DisplayAlert("", "Комплектация успешно сохранена", "OK");
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
        void PhotosTapButton_Clicked(object sender, EventArgs e)
        {
            PhotosSL.IsVisible = PhotosSL.IsVisible ? false : true;
        }
        void MainViewPhotosTapButton_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }

        async void Make_CAR_FRONT_LEFT_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("", "No Camera", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "my_images",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });
            if (file == null) return;

            await DisplayAlert("File Location", file.Path, "OK");

            CAR_FRONT_LEFTimg.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        async void Pick_CAR_FRONT_LEFT_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("", "Photos Not Supported", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });

            if (file == null) return;

            CAR_FRONT_LEFTimg.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        void Make_CAR_FRONT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_FRONT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_REAR_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_REAR_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_REAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_REAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_LEFT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_LEFT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_DASHBOARD_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_DASHBOARD_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_INTERIOR_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_INTERIOR_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_REAR_SEATS_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_REAR_SEATS_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_BAGGAGE_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_BAGGAGE_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_CAR_ENGINE_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_CAR_ENGINE_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Make_RIGHT_WHEEL_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
        void Pick_RIGHT_WHEEL_Clicked(object sender, EventArgs e)
        {
            MainViewPhotosSL.IsVisible = MainViewPhotosSL.IsVisible ? false : true;
        }
    }
}