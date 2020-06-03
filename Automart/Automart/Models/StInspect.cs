using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class StInspect
    {
        public int Id { get; set; }
        public int StateId { get; set; }

        public string External { get; set; } //struct
        public string Cabin { get; set; } //struct
    }
}
