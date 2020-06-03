using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.ViewModels.Sctructs
{
    struct ExternalStIn
    {
        public Dictionary<string, bool> FrontEnd { get; set; }
        public Dictionary<string, bool> FrontLeftEnd { get; set; }
        public Dictionary<string, bool> FrontRightEnd { get; set; }
        public Dictionary<string, bool> BackEnd { get; set; }
        public Dictionary<string, bool> BackLeftEnd { get; set; }
        public Dictionary<string, bool> BackRightEnd { get; set; }
    }
}
