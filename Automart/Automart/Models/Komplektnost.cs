using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class Komplektnost
    {
        public int Id { get; set; }
        public int AdId { get; set; }

        public int KeyCollection { get; set; }
        public int WheelCollection { get; set; }

        public string Zapaska { get; set; }
        public string MatCollection { get; set; }
        public string ExtraKomplektnost { get; set; }

        public bool PTS { get; set; }
        public bool Rukov { get; set; }
        public bool Aptechka { get; set; }
        public bool BoltKey { get; set; }

        public bool ServiceBook { get; set; }
        public bool Tools { get; set; }
        public bool FireExt { get; set; }
        public bool Jack { get; set; }

        public bool RegCert { get; set; }
        public bool Triangle { get; set; }
        public bool BaloonKey { get; set; }
        public bool Compressor { get; set; }

        public DateTime Created_at { get; set; }
    }
}
