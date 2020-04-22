using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("KPPs")]
    public class KPPViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private KPP kPP;

        public KPPViewModel()
        {
            kPP = new KPP();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return kPP.Id; }
            set
            {
                if (kPP.Id != value)
                {
                    kPP.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return kPP.Value; }
            set
            {
                if (kPP.Value != value)
                {
                    kPP.Value = value;
                    OnPropertyChanged("Value");
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