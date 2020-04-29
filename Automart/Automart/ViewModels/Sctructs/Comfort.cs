using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct Comfort
    {
        public bool FrontPowerWindow { get; set; }
        public bool BackPowerWindow { get; set; }
        public bool DoorCurtainsPowerWindow { get; set; }
        public bool RearWindowBlindPowerWindow { get; set; }

        public string PowerSteering { get; set; }

        public bool RainSensor { get; set; }
        public bool LightSensor { get; set; }

        public string Climat { get; set; }

        public bool FrontSeatVent { get; set; }
        public bool BackSeatVent { get; set; }
        public bool CruiseControl { get; set; }

        public bool CassetteMultimedia { get; set; }
        public bool CDMultimedia { get; set; }
        public bool DVDMultimedia { get; set; }
        public bool USBMultimedia { get; set; }
        public bool AUXMultimedia { get; set; }

        public bool NavSys { get; set; }
        public bool OnBoardComp { get; set; }

        public bool FrontCamera { get; set; }
        public bool BackCamera { get; set; }
        public bool SideInMirrorsCamera { get; set; }
    }
}
