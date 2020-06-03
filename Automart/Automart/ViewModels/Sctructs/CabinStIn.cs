using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct CabinStIn
    {
        public Dictionary<string, bool> FrontEnd { get; set; }
        public Dictionary<string, bool> DriverSeat { get; set; }
        public Dictionary<string, bool> FrontSeat { get; set; }
        public Dictionary<string, bool> BackLeftSeat { get; set; }
        public Dictionary<string, bool> BackRightSeat { get; set; }
        public Dictionary<string, bool> Trunk { get; set; } 
    }
}
