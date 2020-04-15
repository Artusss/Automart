using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class Advertisement
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        // Общая инфа об авто
        public string Type { get; set; }      // 1
        public string VIN { get; set; }           // 2
        public string Mark { get; set; }     // 3
        public string Model { get; set; }      // 4
        public string Year { get; set; }         // 5
        public double Mileage { get; set; } // 6
        public string Kuzov { get; set; }  // 7
        public string Color { get; set; }  // 8
        public string SteeringWheel { get; set; }  // 9

        // Двигатель и трансмиссия

        public string DvigType { get; set; }         // 10
        public string KPP { get; set; } // 11
        public string DriveUnit { get; set; }  // 12
        public double Volume { get; set; }  // 13
        public double Power { get; set; }  // 14

        // В будущем еще фотографии ракурсов
        public DateTime Created_at { get; set; }
    }
}
