using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class Advertisement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AutoType { get; set; }      // 1
        public string VIN { get; set; }           // 2
        public string GovNumber { get; set; }     // 3
        public string AutoMark { get; set; }      // 4
        public string Color { get; set; }         // 5
        public string SteeringWheel { get; set; } // 6
        public double EngineVolume { get; set; }  // 7

        // В будущем еще фотографии ракурсов
        public DateTime Created_at { get; set; }
    }
}
