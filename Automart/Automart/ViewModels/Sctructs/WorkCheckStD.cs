using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Automart.ViewModels.Sctructs
{
    struct WorkCheckStD
    {
        public bool BatteryState { get; set; }
        public bool Headlights { get; set; }
        public bool Foglights { get; set; }
        public bool Taillights { get; set; }
        public bool GUR { get; set; }
        public bool EngineOperation { get; set; }
        public bool KPP { get; set; }
        public bool Clutch { get; set; }
        public bool ABS { get; set; }
        public bool StabilitySys { get; set; }
        public bool CheckEngine { get; set; }
        public bool AirBag { get; set; }
        public bool AirCondFront { get; set; }
        public bool AirCondBack { get; set; }
        public bool AudioSys { get; set; }
        public bool Navigation { get; set; }
        public bool AirSusp { get; set; }
        public bool KeylessAccess { get; set; }
        public bool DoorClosers { get; set; }
    }
}
