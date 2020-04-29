using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct Safety
    {
        public bool TractionControlSys { get; set; }
        public bool AntiLockSys { get; set; }
        public bool CursStable { get; set; }
        public bool DifBlockSys { get; set; }
        public bool BrakeForceDistSys { get; set; }
        public bool ParkHelperSys { get; set; }
        public bool DeadZoneContr { get; set; }
        public bool AdjacentStripSys { get; set; }

        public bool VoditelAirBag { get; set; }
        public bool FrontPassAirBag { get; set; }
        public bool BackPassAirBag { get; set; }
        public bool SideAirBag { get; set; }
        public bool CurtainsAirBag { get; set; }

        public bool FrontParkSens { get; set; }
        public bool BackParkSens { get; set; }
    }
}
