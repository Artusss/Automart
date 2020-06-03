using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class StDiagnostic
    {
        public int Id { get; set; }
        public int StateId { get; set; }

        public string WorkCheck { get; set; } //struct
        public string UnderHoodCheck { get; set; } //struct
        public string LiftCheck { get; set; } //struct
        public bool Computer { get; set; }
    }
}
