using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Komplektacya_dev01")]
    public class KomplektacyaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Komplektacya komplektacya;

        public KomplektacyaViewModel()
        {
            komplektacya = new Komplektacya();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return komplektacya.Id; }
            set
            {
                if (komplektacya.Id != value)
                {
                    komplektacya.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public int AdId
        {
            get { return komplektacya.AdId; }
            set
            {
                if (komplektacya.AdId != value)
                {
                    komplektacya.AdId = value;
                    OnPropertyChanged("AdId");
                }
            }
        }

        public string Safety
        {
            get { return komplektacya.Safety; }
            set
            {
                if (komplektacya.Safety != value)
                {
                    komplektacya.Safety = value;
                    OnPropertyChanged("Safety");
                }
            }
        }
        public string Lightning
        {
            get { return komplektacya.Lightning; }
            set
            {
                if (komplektacya.Lightning != value)
                {
                    komplektacya.Lightning = value;
                    OnPropertyChanged("Lightning");
                }
            }
        }
        public string Heating
        {
            get { return komplektacya.Heating; }
            set
            {
                if (komplektacya.Heating != value)
                {
                    komplektacya.Heating = value;
                    OnPropertyChanged("Heating");
                }
            }
        }
        public string Comfort
        {
            get { return komplektacya.Comfort; }
            set
            {
                if (komplektacya.Comfort != value)
                {
                    komplektacya.Comfort = value;
                    OnPropertyChanged("Comfort");
                }
            }
        }
        public string Exterior
        {
            get { return komplektacya.Exterior; }
            set
            {
                if (komplektacya.Exterior != value)
                {
                    komplektacya.Exterior = value;
                    OnPropertyChanged("Exterior");
                }
            }
        }
        public string SecuritySys
        {
            get { return komplektacya.SecuritySys; }
            set
            {
                if (komplektacya.SecuritySys != value)
                {
                    komplektacya.SecuritySys = value;
                    OnPropertyChanged("SecuritySys");
                }
            }
        }
        public string Adjustments
        {
            get { return komplektacya.Adjustments; }
            set
            {
                if (komplektacya.Adjustments != value)
                {
                    komplektacya.Adjustments = value;
                    OnPropertyChanged("Adjustments");
                }
            }
        }
        public string Interior
        {
            get { return komplektacya.Interior; }
            set
            {
                if (komplektacya.Interior != value)
                {
                    komplektacya.Interior = value;
                    OnPropertyChanged("Interior");
                }
            }
        }
        public string ExtraKomplektacya
        {
            get { return komplektacya.ExtraKomplektacya; }
            set
            {
                if (komplektacya.ExtraKomplektacya != value)
                {
                    komplektacya.ExtraKomplektacya = value;
                    OnPropertyChanged("ExtraKomplektacya");
                }
            }
        }

        public DateTime Created_at
        {
            get { return komplektacya.Created_at; }
            set
            {
                if (komplektacya.Created_at != value)
                {
                    komplektacya.Created_at = value;
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
