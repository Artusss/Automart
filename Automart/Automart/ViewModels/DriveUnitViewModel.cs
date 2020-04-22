using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("DriveUnits")]
    public class DriveUnitViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DriveUnit driveUnit;

        public DriveUnitViewModel()
        {
            driveUnit = new DriveUnit();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return driveUnit.Id; }
            set
            {
                if (driveUnit.Id != value)
                {
                    driveUnit.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return driveUnit.Value; }
            set
            {
                if (driveUnit.Value != value)
                {
                    driveUnit.Value = value;
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