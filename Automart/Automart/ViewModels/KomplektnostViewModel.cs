using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Komplektnost_dev02")]
    public class KomplektnostViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Komplektnost komplektnost;

        public KomplektnostViewModel()
        {
            komplektnost = new Komplektnost();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return komplektnost.Id; }
            set
            {
                if (komplektnost.Id != value)
                {
                    komplektnost.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public int AdId
        {
            get { return komplektnost.AdId; }
            set
            {
                if (komplektnost.AdId != value)
                {
                    komplektnost.AdId = value;
                    OnPropertyChanged("AdId");
                }
            }
        }

        public int KeyCollection
        {
            get { return komplektnost.KeyCollection; }
            set
            {
                if (komplektnost.KeyCollection != value)
                {
                    komplektnost.KeyCollection = value;
                    OnPropertyChanged("KeyCollection");
                }
            }
        }

        public int WheelCollection
        {
            get { return komplektnost.WheelCollection; }
            set
            {
                if (komplektnost.WheelCollection != value)
                {
                    komplektnost.WheelCollection = value;
                    OnPropertyChanged("WheelCollection");
                }
            }
        }

        public string Zapaska
        {
            get { return komplektnost.Zapaska; }
            set
            {
                if (komplektnost.Zapaska != value)
                {
                    komplektnost.Zapaska = value;
                    OnPropertyChanged("Zapaska");
                }
            }
        }
        public string MatCollection
        {
            get { return komplektnost.MatCollection; }
            set
            {
                if (komplektnost.MatCollection != value)
                {
                    komplektnost.MatCollection = value;
                    OnPropertyChanged("MatCollection");
                }
            }
        }
        public string ExtraKomplektnost
        {
            get { return komplektnost.ExtraKomplektnost; }
            set
            {
                if (komplektnost.ExtraKomplektnost != value)
                {
                    komplektnost.ExtraKomplektnost = value;
                    OnPropertyChanged("ExtraKomplektnost");
                }
            }
        }

        public bool PTS
        {
            get { return komplektnost.PTS; }
            set
            {
                if (komplektnost.PTS != value)
                {
                    komplektnost.PTS = value;
                    OnPropertyChanged("PTS");
                }
            }
        }
        public bool Rukov
        {
            get { return komplektnost.Rukov; }
            set
            {
                if (komplektnost.Rukov != value)
                {
                    komplektnost.Rukov = value;
                    OnPropertyChanged("Rukov");
                }
            }
        }
        public bool Aptechka
        {
            get { return komplektnost.Aptechka; }
            set
            {
                if (komplektnost.Aptechka != value)
                {
                    komplektnost.Aptechka = value;
                    OnPropertyChanged("Aptechka");
                }
            }
        }
        public bool BoltKey
        {
            get { return komplektnost.BoltKey; }
            set
            {
                if (komplektnost.BoltKey != value)
                {
                    komplektnost.BoltKey = value;
                    OnPropertyChanged("BoltKey");
                }
            }
        }
        public bool ServiceBook
        {
            get { return komplektnost.ServiceBook; }
            set
            {
                if (komplektnost.ServiceBook != value)
                {
                    komplektnost.ServiceBook = value;
                    OnPropertyChanged("ServiceBook");
                }
            }
        }
        public bool Tools
        {
            get { return komplektnost.Tools; }
            set
            {
                if (komplektnost.Tools != value)
                {
                    komplektnost.Tools = value;
                    OnPropertyChanged("Tools");
                }
            }
        }
        public bool FireExt
        {
            get { return komplektnost.FireExt; }
            set
            {
                if (komplektnost.FireExt != value)
                {
                    komplektnost.FireExt = value;
                    OnPropertyChanged("FireExt");
                }
            }
        }
        public bool Jack
        {
            get { return komplektnost.Jack; }
            set
            {
                if (komplektnost.Jack != value)
                {
                    komplektnost.Jack = value;
                    OnPropertyChanged("Jack");
                }
            }
        }
        public bool RegCert
        {
            get { return komplektnost.RegCert; }
            set
            {
                if (komplektnost.RegCert != value)
                {
                    komplektnost.RegCert = value;
                    OnPropertyChanged("RegCert");
                }
            }
        }
        public bool Triangle
        {
            get { return komplektnost.Triangle; }
            set
            {
                if (komplektnost.Triangle != value)
                {
                    komplektnost.Triangle = value;
                    OnPropertyChanged("Triangle");
                }
            }
        }
        public bool BaloonKey
        {
            get { return komplektnost.BaloonKey; }
            set
            {
                if (komplektnost.BaloonKey != value)
                {
                    komplektnost.BaloonKey = value;
                    OnPropertyChanged("BaloonKey");
                }
            }
        }
        public bool Compressor
        {
            get { return komplektnost.Compressor; }
            set
            {
                if (komplektnost.Compressor != value)
                {
                    komplektnost.Compressor = value;
                    OnPropertyChanged("Compressor");
                }
            }
        }

        public DateTime Created_at
        {
            get { return komplektnost.Created_at; }
            set
            {
                if (komplektnost.Created_at != value)
                {
                    komplektnost.Created_at = value;
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
