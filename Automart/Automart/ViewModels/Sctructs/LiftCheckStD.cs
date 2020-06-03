using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct LiftCheckStD
    {
        public bool BrakeLines { get; set; }
        public bool FrontShockAbsorbers { get; set; }
        public bool BackShockAbsorbers { get; set; }
        public bool UnderbodyProtect { get; set; }
        public bool UnderbodyState { get; set; }
        public bool Muffler { get; set; }
        public bool SilencerPipes { get; set; }
        public bool BodyGeometry { get; set; }
    }
}
