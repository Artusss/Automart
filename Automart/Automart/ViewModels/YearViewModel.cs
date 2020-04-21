using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Years")]
    public class YearViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Year year;

        public YearViewModel()
        {
            year = new Year();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return year.Id; }
            set
            {
                if (year.Id != value)
                {
                    year.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return year.Value; }
            set
            {
                if (year.Value != value)
                {
                    year.Value = value;
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