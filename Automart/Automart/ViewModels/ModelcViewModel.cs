using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Models")]
    public class ModelcViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Modelc model;

        public ModelcViewModel()
        {
            model = new Modelc();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return model.Id; }
            set
            {
                if (model.Id != value)
                {
                    model.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return model.Value; }
            set
            {
                if (model.Value != value)
                {
                    model.Value = value;
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
