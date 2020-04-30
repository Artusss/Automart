using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class Komplektacya
    {
        public int Id { get; set; }
        public int AdId { get; set; }

        public string Safety { get; set; }
        public string Lightning { get; set; }
        public string Heating { get; set; }
        public string Comfort { get; set; }
        public string Exterior { get; set; }
        public string SecuritySys { get; set; }
        public string Adjustments { get; set; }
        public string Interior { get; set; }
        public string ExtraKomplektacya { get; set; }

        public DateTime Created_at { get; set; }
    }
}
