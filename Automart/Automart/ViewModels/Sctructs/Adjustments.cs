using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct Adjustments
    {
        public bool HeightDriverSeat { get; set; }
        public bool ElectricDriverSeat { get; set; }
        public bool MemoryDriverSeat { get; set; }

        public bool HeightPassSeat { get; set; }
        public bool ElectricPassSeat { get; set; }
        public bool MemoryPassSeat { get; set; }
        public bool ElectricBackPassSeat { get; set; }

        public string SteeringWheel { get; set; }
    }
}