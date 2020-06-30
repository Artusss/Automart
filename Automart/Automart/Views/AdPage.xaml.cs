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
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;

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

        public static AdCommentSQLiteHelper adCommentSQLiteH;
        public static AdCommentSQLiteHelper AdCommentSQLiteH
        {
            get
            {
                if (adCommentSQLiteH == null)
                {
                    adCommentSQLiteH = new AdCommentSQLiteHelper(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), App.DATABASE_NAME));
                }
                return adCommentSQLiteH;
            }
        }

        public List<StackLayout> AdComponents;
        public List<Button> AdButtons;

        public List<StackLayout> KomplektacyaComponents;
        public List<Button> KomplektacyaButtons;

        public List<StackLayout> PhotoComponents;
        public List<Button> PhotoButtons;

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

            Grid MainInfoGrid = new Grid
            {
                Padding = new Thickness(15, 10)
            };
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            MainInfoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            MainInfoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            var VINLabel = new Label
            {
                TextColor = Color.Black,
                Text = "VIN :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var VINEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.VIN,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var TypeLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Тип :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var TypeEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Type,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var MarkLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Марка :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var MarkEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Mark,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var ModelLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Модель :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var ModelEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Model,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var YearLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Год выпуска :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var YearEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Year,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var MileageLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Пробег, км :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var MileageEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Mileage.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var KuzovLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Кузов :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var KuzovEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Kuzov,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var ColorLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Цвет :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var ColorEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Color,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var SteeringWheelLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Руль :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var SteeringWheelEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.SteeringWheel,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            MainInfoGrid.Children.Add(VINLabel, 0, 1);
            MainInfoGrid.Children.Add(VINEntry, 1, 1);
            MainInfoGrid.Children.Add(TypeLabel, 0, 2);
            MainInfoGrid.Children.Add(TypeEntry, 1, 2);
            MainInfoGrid.Children.Add(MarkLabel, 0, 3);
            MainInfoGrid.Children.Add(MarkEntry, 1, 3);
            MainInfoGrid.Children.Add(ModelLabel, 0, 4);
            MainInfoGrid.Children.Add(ModelEntry, 1, 4);
            MainInfoGrid.Children.Add(YearLabel, 0, 5);
            MainInfoGrid.Children.Add(YearEntry, 1, 5);
            MainInfoGrid.Children.Add(MileageLabel, 0, 6);
            MainInfoGrid.Children.Add(MileageEntry, 1, 6);
            MainInfoGrid.Children.Add(KuzovLabel, 0, 7);
            MainInfoGrid.Children.Add(KuzovEntry, 1, 7);
            MainInfoGrid.Children.Add(ColorLabel, 0, 8);
            MainInfoGrid.Children.Add(ColorEntry, 1, 8);
            MainInfoGrid.Children.Add(SteeringWheelLabel, 0, 9);
            MainInfoGrid.Children.Add(SteeringWheelEntry, 1, 9);

            MainInfoSL.Children.Add(MainInfoGrid);

            Grid DvigTransGrid = new Grid
            {
                Padding = new Thickness(15, 10)
            };
            DvigTransGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            DvigTransGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            DvigTransGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            DvigTransGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            DvigTransGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            DvigTransGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            DvigTransGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            var DvigTypeLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Тип двигателя :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var DvigTypeEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.DvigType,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var KPPLabel = new Label
            {
                TextColor = Color.Black,
                Text = "КПП :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var KPPEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.KPP,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var DriveUnitLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Привод :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var DriveUnitEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.DriveUnit,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            
            var VolumeLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Объем, см3:",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            var VolumeEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Volume.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var PowerLabel = new Label
            {
                TextColor = Color.Black,
                Text = "Мощность, л.с :",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            var PowerEntry = new Label
            {
                TextColor = Color.Black,
                Text = AdVM.Power.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            DvigTransGrid.Children.Add(DvigTypeLabel, 0, 1);
            DvigTransGrid.Children.Add(DvigTypeEntry, 1, 1);
            DvigTransGrid.Children.Add(KPPLabel, 0, 2);
            DvigTransGrid.Children.Add(KPPEntry, 1, 2);
            DvigTransGrid.Children.Add(DriveUnitLabel, 0, 3);
            DvigTransGrid.Children.Add(DriveUnitEntry, 1, 3);
            DvigTransGrid.Children.Add(VolumeLabel, 0, 4);
            DvigTransGrid.Children.Add(VolumeEntry, 1, 4);
            DvigTransGrid.Children.Add(PowerLabel, 0, 5);
            DvigTransGrid.Children.Add(PowerEntry, 1, 5);

            DvigTransSL.Children.Add(DvigTransGrid);

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

            AdComponents = new List<StackLayout>
            {
                MainInfoSL,
                DvigTransSL,
                KomplektnostSL,
                KomplektacyaSL,
                PhotosSL,
                StatesSL,
                DocumentsSL,
                CommentsSL
            };
            AdButtons = new List<Button>
            {
                MainInfoTapButton,
                DvigTransSLTapButton,
                KomplektnostSLTapButton,
                KomplektacyaSLTapButton,
                PhotosSLTapButton,
                StatesSLTapButton,
                DocumentsSLTapButton,
                CommentsSLTapButton
            };

            KomplektacyaComponents = new List<StackLayout>
            {
                SafetySL,
                LightingSL,
                HeatingSL,
                СomfortSL,
                ExteriorSL,
                SecuritySysSL,
                AdjustmentsSL,
                InteriorSL,
                ExtraKomplektacyaSL
            };
            KomplektacyaButtons = new List<Button>
            {
                SafetySLTapButton,
                LightingSLTapButton,
                HeatingSLTapButton,
                СomfortSLTapButton,
                ExteriorSLTapButton,
                SecuritySysSLTapButton,
                AdjustmentsSLTapButton,
                InteriorSLTapButton,
                ExtraKomplektacyaSLTapButton
            };

            PhotoComponents = new List<StackLayout>
            {
                MainViewPhotosSL,
                ExtraViewPhotosSL
            };
            PhotoButtons = new List<Button>
            {
                MainViewPhotosSLTapButton,
                ExtraViewPhotosSLTapButton
            };

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

            var StateVM = new StateViewModel
            {
                // Id
                // AdId
                Type = "Отчет о состоянии Автомарт",
                Status = "Закрыт",
                WhoClosed = "Ivanov Ivan",
                Created_at = DateTime.Now,
                Closed_at = DateTime.Now
            };
            Frame StateFrame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#f5f5f5"),
                BorderColor = Color.FromHex("#e1e1e1"),
                Margin = new Thickness(0),
                Padding = new Thickness(0),
            };
            //StateCollectionView.SelectionChanged += ToState_ItemSelected;
            Grid StateGrid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center
            };
            StateGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            StateGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            StateGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            StateGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            StateGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            StateGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            StateGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Label StTypeLabel = new Label { 
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = StateVM.Type,
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            Label Created_atLabel = new Label { 
                FontAttributes = FontAttributes.Italic,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = $"Создан: {StateVM.Created_at:dd.MM.yyyy}",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            var statusTextColor = StateVM.Status.Equals("Закрыт") ? Color.FromHex("#a94442") : Color.FromHex("#5cb85c");
            Label StatusLabel = new Label { 
                FontAttributes = FontAttributes.Italic,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = $"Статус: {StateVM.Status}",
                TextColor = statusTextColor,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            Label Closed_atLabel = new Label { 
                FontAttributes = FontAttributes.Italic,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = $"Закрыт: {StateVM.Closed_at:dd.MM.yyyy}",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            Label WhoClosedLabel = new Label
            {
                FontAttributes = FontAttributes.Italic,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = $"Перевел: {StateVM.WhoClosed}",
                TextColor = Color.Black,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button))
            };
            Button MoreStateInfoButton = new Button
            {
                TextColor = Color.Black,
                ImageSource = "next.png",
                WidthRequest = 60,
                HeightRequest = 32,
                BackgroundColor = Color.FromHex("#f5f5f5"),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            //MoreStateInfoButton.Clicked += someButFunc();
            Grid.SetColumnSpan(StTypeLabel, 2);
            StateGrid.Children.Add(StTypeLabel);
            StateGrid.Children.Add(StatusLabel, 0, 1);
            StateGrid.Children.Add(MoreStateInfoButton, 1, 1);
            StateGrid.Children.Add(Created_atLabel, 0, 2);
            StateGrid.Children.Add(Closed_atLabel, 0, 3);
            StateGrid.Children.Add(WhoClosedLabel, 0, 4);

            StateFrame.Content = StateGrid;
            StatesSL.Children.Add(StateFrame);

            var AdCommentVM = AdCommentSQLiteH.GetByAd(CurrentAdId);
            if(AdCommentVM != null)
            {
                ExtraCommentInfoEditor.Text = AdCommentVM.Text;
            }
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

        public void changeSLState(StackLayout stackLayout, Button button, bool isWhite = false)
        {
            string imageDown = isWhite ? "downArrowWh.png" : "downArrow.png";
            string imageUp   = isWhite ? "upArrowWh.png" : "upArrow.png";
            if (stackLayout.IsVisible)
            {
                stackLayout.IsVisible = false;
                button.ImageSource = imageDown;
                return;
            }
            stackLayout.IsVisible = true;
            button.ImageSource = imageUp;
        }

        public void hideSLsState(List<StackLayout> listSL, List<Button> listButton, bool isWhite = false)
        {
            foreach(StackLayout stackLayout in listSL)
            {
                stackLayout.IsVisible = false;
            }
            foreach (Button button in listButton)
            {
                button.ImageSource = isWhite ? "downArrowWh.png" : "downArrow.png";
            }
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
        
        async void MainInfoEditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainInfoEditPage());
        }
        
        async void DvigTransEditButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DvigTransEditPage());
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

        async void CommentsSaveButton_Clicked(object sender, EventArgs e)
        {
            int CurrentAdId = CrossSettings.Current.GetValueOrDefault("CurrentAdId", 0);
            if (CurrentAdId.Equals(0)) await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            AdCommentViewModel adCommentVM = AdCommentSQLiteH.GetByAd(CurrentAdId);
            if (adCommentVM == null)
            {
                adCommentVM = new AdCommentViewModel();
                adCommentVM.AdId       = CurrentAdId;
                adCommentVM.Created_at = DateTime.Now;
            }
            adCommentVM.Text = ExtraCommentInfoEditor.Text;

            AdCommentSQLiteH.SaveItem(adCommentVM);
            await DisplayAlert("", "Комментарий успешно сохранен", "OK");
            return;
        }

        void MainInfoTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(MainInfoSL);
            AdButtons.Remove(MainInfoTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(MainInfoSL);
            AdButtons.Add(MainInfoTapButton);

            changeSLState(MainInfoSL, MainInfoTapButton, true);
        }
        void DvigTransTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(DvigTransSL);
            AdButtons.Remove(DvigTransSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(DvigTransSL);
            AdButtons.Add(DvigTransSLTapButton);

            changeSLState(DvigTransSL, DvigTransSLTapButton, true);
        }
        void KomplektnostTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(KomplektnostSL);
            AdButtons.Remove(KomplektnostSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(KomplektnostSL);
            AdButtons.Add(KomplektnostSLTapButton);

            changeSLState(KomplektnostSL, KomplektnostSLTapButton, true);
        }
        void KomplektacyaTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(KomplektacyaSL);
            AdButtons.Remove(KomplektacyaSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(KomplektacyaSL);
            AdButtons.Add(KomplektacyaSLTapButton);

            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);

            changeSLState(KomplektacyaSL, KomplektacyaSLTapButton, true);
        }
        void SafetyTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(SafetySL);
            KomplektacyaButtons.Remove(SafetySLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(SafetySL);
            KomplektacyaButtons.Add(SafetySLTapButton);

            changeSLState(SafetySL, SafetySLTapButton);
        }
        void LightingTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(LightingSL);
            KomplektacyaButtons.Remove(LightingSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(LightingSL);
            KomplektacyaButtons.Add(LightingSLTapButton);

            changeSLState(LightingSL, LightingSLTapButton);
        }
        void HeatingTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(HeatingSL);
            KomplektacyaButtons.Remove(HeatingSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(HeatingSL);
            KomplektacyaButtons.Add(HeatingSLTapButton);

            changeSLState(HeatingSL, HeatingSLTapButton);
        }
        void СomfortTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(СomfortSL);
            KomplektacyaButtons.Remove(СomfortSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(СomfortSL);
            KomplektacyaButtons.Add(СomfortSLTapButton);

            changeSLState(СomfortSL, СomfortSLTapButton);
        }
        void ExteriorTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(ExteriorSL);
            KomplektacyaButtons.Remove(ExteriorSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(ExteriorSL);
            KomplektacyaButtons.Add(ExteriorSLTapButton);

            changeSLState(ExteriorSL, ExteriorSLTapButton);
        }
        void SecuritySysTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(SecuritySysSL);
            KomplektacyaButtons.Remove(SecuritySysSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(SecuritySysSL);
            KomplektacyaButtons.Add(SecuritySysSLTapButton);

            changeSLState(SecuritySysSL, SecuritySysSLTapButton);
        }
        void AdjustmentsTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(AdjustmentsSL);
            KomplektacyaButtons.Remove(AdjustmentsSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(AdjustmentsSL);
            KomplektacyaButtons.Add(AdjustmentsSLTapButton);

            changeSLState(AdjustmentsSL, AdjustmentsSLTapButton);
        }
        void InteriorTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(InteriorSL);
            KomplektacyaButtons.Remove(InteriorSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(InteriorSL);
            KomplektacyaButtons.Add(InteriorSLTapButton);

            changeSLState(InteriorSL, InteriorSLTapButton);
        }
        void ExtraKomplektacyaTapButton_Clicked(object sender, EventArgs e)
        {
            KomplektacyaComponents.Remove(ExtraKomplektacyaSL);
            KomplektacyaButtons.Remove(ExtraKomplektacyaSLTapButton);
            hideSLsState(KomplektacyaComponents, KomplektacyaButtons);
            KomplektacyaComponents.Add(ExtraKomplektacyaSL);
            KomplektacyaButtons.Add(ExtraKomplektacyaSLTapButton);

            changeSLState(ExtraKomplektacyaSL, ExtraKomplektacyaSLTapButton);
        }
        void PhotosTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(PhotosSL);
            AdButtons.Remove(PhotosSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(PhotosSL);
            AdButtons.Add(PhotosSLTapButton);
            
            hideSLsState(PhotoComponents, PhotoButtons);

            changeSLState(PhotosSL, PhotosSLTapButton, true);
        }
        void MainViewPhotosTapButton_Clicked(object sender, EventArgs e)
        {
            PhotoComponents.Remove(MainViewPhotosSL);
            PhotoButtons.Remove(MainViewPhotosSLTapButton);
            hideSLsState(PhotoComponents, PhotoButtons);
            PhotoComponents.Add(MainViewPhotosSL);
            PhotoButtons.Add(MainViewPhotosSLTapButton);

            changeSLState(MainViewPhotosSL, MainViewPhotosSLTapButton);
        }

        void ExtraViewPhotosTapButton_Clicked(object sender, EventArgs e)
        {
            PhotoComponents.Remove(ExtraViewPhotosSL);
            PhotoButtons.Remove(ExtraViewPhotosSLTapButton);
            hideSLsState(PhotoComponents, PhotoButtons);
            PhotoComponents.Add(ExtraViewPhotosSL);
            PhotoButtons.Add(ExtraViewPhotosSLTapButton);

            changeSLState(ExtraViewPhotosSL, ExtraViewPhotosSLTapButton);
        }

        void StatesTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(StatesSL);
            AdButtons.Remove(StatesSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(StatesSL);
            AdButtons.Add(StatesSLTapButton);

            changeSLState(StatesSL, StatesSLTapButton, true);
        }

        void DocumentsTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(DocumentsSL);
            AdButtons.Remove(DocumentsSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(DocumentsSL);
            AdButtons.Add(DocumentsSLTapButton);
  
            changeSLState(DocumentsSL, DocumentsSLTapButton, true);
        }

        void CommentsTapButton_Clicked(object sender, EventArgs e)
        {
            AdComponents.Remove(CommentsSL);
            AdButtons.Remove(CommentsSLTapButton);
            hideSLsState(AdComponents, AdButtons, true);
            AdComponents.Add(CommentsSL);
            AdButtons.Add(CommentsSLTapButton);
 
            changeSLState(CommentsSL, CommentsSLTapButton, true);
        }
        public async void MakePhotoAsync(Image image)
        {
            await DisplayAlert("", "Просьба держать камеру горизонтально", "OK");
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("", "Камера отсутствует", "OK");
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "my_images",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 1500,
                DefaultCamera = CameraDevice.Front
            });
            if (file == null) return;

            /*await DisplayAlert("File Location", file.Path, "OK");*/

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        public async void PickPhotoAsync(Image image)
        {
            await DisplayAlert("", "Просьба выбирать горизонтальные изображения", "OK");
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("", "Выбор фото не поддерживается", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 1500
            });

            if (file == null) return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        void Make_CAR_FRONT_LEFT_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_FRONT_LEFTimg);
        }
        void Pick_CAR_FRONT_LEFT_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_FRONT_LEFTimg);
        }
        void Make_CAR_FRONT_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_FRONTimg);
        }
        void Pick_CAR_FRONT_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_FRONTimg);
        }
        void Make_CAR_REAR_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_REARimg);
        }
        void Pick_CAR_REAR_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_REARimg);
        }
        void Make_CAR_REAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_REAR_RIGHTimg);
        }
        void Pick_CAR_REAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_REAR_RIGHTimg);
        }
        void Make_CAR_LEFT_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_LEFTimg);
        }
        void Pick_CAR_LEFT_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_LEFTimg);
        }
        void Make_CAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_RIGHTimg);
        }
        void Pick_CAR_RIGHT_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_RIGHTimg);
        }
        void Make_CAR_DASHBOARD_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_DASHBOARDimg);
        }
        void Pick_CAR_DASHBOARD_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_DASHBOARDimg);
        }
        void Make_CAR_INTERIOR_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_INTERIORimg);
        }
        void Pick_CAR_INTERIOR_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_INTERIORimg);
        }
        void Make_CAR_REAR_SEATS_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_REAR_SEATSimg);
        }
        void Pick_CAR_REAR_SEATS_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_REAR_SEATSimg);
        }
        void Make_CAR_BAGGAGE_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_BAGGAGEimg);
        }
        void Pick_CAR_BAGGAGE_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_BAGGAGEimg);
        }
        void Make_CAR_ENGINE_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(CAR_ENGINEimg);
        }
        void Pick_CAR_ENGINE_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(CAR_ENGINEimg);
        }
        void Make_RIGHT_WHEEL_Clicked(object sender, EventArgs e)
        {
            MakePhotoAsync(RIGHT_WHEELimg);
        }
        void Pick_RIGHT_WHEEL_Clicked(object sender, EventArgs e)
        {
            PickPhotoAsync(RIGHT_WHEELimg);
        }

        void Make_EXTRA_CAR_Clicked(object sender, EventArgs e)
        {
            Frame imageFrame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#f5f5f5"),
                BorderColor = Color.FromHex("#e1e1e1"),
                Margin = new Thickness(0),
                Padding = new Thickness(0),
            };
            var ImageTmp = new Image()
            {
                WidthRequest = 100,
                HeightRequest = 220
            };
            MakePhotoAsync(ImageTmp);
            imageFrame.Content = ImageTmp;
            ExtraCarPhotosSL.Children.Add(imageFrame);
        }
        void Pick_EXTRA_CAR_Clicked(object sender, EventArgs e)
        {
            Frame imageFrame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#f5f5f5"),
                BorderColor = Color.FromHex("#e1e1e1"),
                Margin = new Thickness(0),
                Padding = new Thickness(0),
            };
            var ImageTmp = new Image()
            {
                WidthRequest = 100,
                HeightRequest = 220
            };
            PickPhotoAsync(ImageTmp);
            imageFrame.Content = ImageTmp;
            ExtraCarPhotosSL.Children.Add(imageFrame);
        }

        public async void PickFileAsync(Label docLabel)
        {
            FileData fileData = await CrossFilePicker.Current.PickFile();
            if (fileData == null)
                return; // user canceled file picking
            await DisplayAlert("", "Документ успешно добавлен", "OK");
            docLabel.Text = fileData.FileName;
        }

        void Pat1DocumentStore_Clicked(object sender, EventArgs e)
        {
            PickFileAsync(Pat1);
            Pat1_store.IsVisible = false;
            Pat1_delete.IsVisible = true;
        }

        void Pat1DocumentDelete_Clicked(object sender, EventArgs e)
        {
            Pat1.Text = "Шаблон";
            Pat1_delete.IsVisible = false;
            Pat1_store.IsVisible = true;
        }
        /*void DocumentsSaveButton_Clicked(object sender, EventArgs e)
        {
            var StackLayoutTmp = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(10, 10)
            };
            PickFileAsync(StackLayoutTmp);
            PickedDocumentsSL.Children.Add(StackLayoutTmp);
        }
        <Button x:Name="DocumentsSave1Button" TextColor="Black" Text="Выбрать" Clicked="DocumentsSaveButton_Clicked" HorizontalOptions="End" VerticalOptions="Center"/>
         */

        async void ToState_ItemSelected(object sender, SelectionChangedEventArgs e)
        { 
            await Navigation.PushAsync(new NavigationPage(new StatePage()));
        }
    }
}