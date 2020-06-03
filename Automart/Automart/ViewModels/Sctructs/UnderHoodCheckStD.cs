using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct UnderHoodCheckStD
    {
        public bool EngineOilLeak { get; set; }
        public bool KppOilLeak { get; set; }
        public bool EngineOilLevel { get; set; }
        public bool AntifreezeLevel { get; set; }
        public bool BrakeFluidLevel { get; set; }
        public bool GurFluidLevel { get; set; }
    }
}
