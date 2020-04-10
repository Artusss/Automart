using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Advertisements")]
    class AdViewModel : INotifyPropertyChanged
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
        public string AutoType
        {
            get { return advertisement.AutoType; }
            set
            {
                if (advertisement.AutoType != value)
                {
                    advertisement.AutoType = value;
                    OnPropertyChanged("AutoType");
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
        public string GovNumber
        {
            get { return advertisement.GovNumber; }
            set
            {
                if (advertisement.GovNumber != value)
                {
                    advertisement.GovNumber = value;
                    OnPropertyChanged("GovNumber");
                }
            }
        }
        public string AutoMark
        {
            get { return advertisement.AutoMark; }
            set
            {
                if (advertisement.AutoMark != value)
                {
                    advertisement.AutoMark = value;
                    OnPropertyChanged("AutoMark");
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
        public double EngineVolume
        {
            get { return advertisement.EngineVolume; }
            set
            {
                if (advertisement.EngineVolume != value)
                {
                    advertisement.EngineVolume = value;
                    OnPropertyChanged("EngineVolume");
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
