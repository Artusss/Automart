using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class StWheelInspect
    {
        public int Id { get; set; }
        public int StateId { get; set; }

        public bool InAuto { get; set; }
        public string Season { get; set; }
        public string Description { get; set; }
        public string Axis_1 { get; set; } //struct
        public string Axis_2 { get; set; } //struct
    }
}
