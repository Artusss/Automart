using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Kuzovs")]
    public class KuzovViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Kuzov kuzov;

        public KuzovViewModel()
        {
            kuzov = new Kuzov();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return kuzov.Id; }
            set
            {
                if (kuzov.Id != value)
                {
                    kuzov.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return kuzov.Value; }
            set
            {
                if (kuzov.Value != value)
                {
                    kuzov.Value = value;
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
