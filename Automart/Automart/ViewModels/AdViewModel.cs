using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Advertisements")]
    public class AdViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Advertisement advertisement;

        public AdViewModel()
        {
            advertisement = new Advertisement();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return advertisement.Id; }
            set
            {
                if (advertisement.Id != value)
                {
                    advertisement.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public int UserId
        {
            get { return advertisement.UserId; }
            set
            {
                if (advertisement.UserId != value)
                {
                    advertisement.UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        // Общая инфа об авто

        public string Type
        {
            get { return advertisement.Type; }
            set
            {
                if (advertisement.Type != value)
                {
                    advertisement.Type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public string VIN
        {
            get { return advertisement.VIN; }
            set
            {
                if (advertisement.VIN != value)
                {
                    advertisement.VIN = value;
                    OnPropertyChanged("VIN");
                }
            }
        }
        public string Mark
        {
            get { return advertisement.Mark; }
            set
            {
                if (advertisement.Mark != value)
                {
                    advertisement.Mark = value;
                    OnPropertyChanged("Mark");
                }
            }
        }
        public string Model
        {
            get { return advertisement.Model; }
            set
            {
                if (advertisement.Model != value)
                {
                    advertisement.Model = value;
                    OnPropertyChanged("Model");
                }
            }
        }
        public string Year
        {
            get { return advertisement.Year; }
            set
            {
                if (advertisement.Year != value)
                {
                    advertisement.Year = value;
                    OnPropertyChanged("Year");
                }
            }
        }
        public double Mileage
        {
            get { return advertisement.Mileage; }
            set
            {
                if (advertisement.Mileage != value)
                {
                    advertisement.Mileage = value;
                    OnPropertyChanged("Mileage");
                }
            }
        }
        public string Kuzov
        {
            get { return advertisement.Kuzov; }
            set
            {
                if (advertisement.Kuzov != value)
                {
                    advertisement.Kuzov = value;
                    OnPropertyChanged("Kuzov");
                }
            }
        }
        public string Color
        {
            get { return advertisement.Color; }
            set
            {
                if (advertisement.Color != value)
                {
                    advertisement.Color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        public string SteeringWheel
        {
            get { return advertisement.SteeringWheel; }
            set
            {
                if (advertisement.SteeringWheel != value)
                {
                    advertisement.SteeringWheel = value;
                    OnPropertyChanged("SteeringWheel");
                }
            }
        }

        // Двигатель и трансмиссия

        public string DvigType
        {
            get { return advertisement.DvigType; }
            set
            {
                if (advertisement.DvigType != value)
                {
                    advertisement.DvigType = value;
                    OnPropertyChanged("DvigType");
                }
            }
        }
        public string KPP
        {
            get { return advertisement.KPP; }
            set
            {
                if (advertisement.KPP != value)
                {
                    advertisement.KPP = value;
                    OnPropertyChanged("KPP");
                }
            }
        }
        public string DriveUnit
        {
            get { return advertisement.DriveUnit; }
            set
            {
                if (advertisement.DriveUnit != value)
                {
                    advertisement.DriveUnit = value;
                    OnPropertyChanged("DriveUnit");
                }
            }
        }
        public double Volume
        {
            get { return advertisement.Volume; }
            set
            {
                if (advertisement.Volume != value)
                {
                    advertisement.Volume = value;
                    OnPropertyChanged("Volume");
                }
            }
        }
        public double Power
        {
            get { return advertisement.Power; }
            set
            {
                if (advertisement.Power != value)
                {
                    advertisement.Power = value;
                    OnPropertyChanged("Power");
                }
            }
        }
        // В будущем еще фотографии ракурсов
        public DateTime Created_at
        {
            get { return advertisement.Created_at; }
            set
            {
                if (advertisement.Created_at != value)
                {
                    advertisement.Created_at = value;
                    OnPropertyChanged("Created_at");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
