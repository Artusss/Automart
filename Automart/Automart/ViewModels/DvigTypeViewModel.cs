using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("DvigTypes")]
    public class DvigTypeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DvigType dvigType;

        public DvigTypeViewModel()
        {
            dvigType = new DvigType();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return dvigType.Id; }
            set
            {
                if (dvigType.Id != value)
                {
                    dvigType.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return dvigType.Value; }
            set
            {
                if (dvigType.Value != value)
                {
                    dvigType.Value = value;
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