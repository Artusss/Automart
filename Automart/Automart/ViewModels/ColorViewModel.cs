using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Colors")]
    public class ColorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Color color;

        public ColorViewModel()
        {
            color = new Color();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return color.Id; }
            set
            {
                if (color.Id != value)
                {
                    color.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return color.Value; }
            set
            {
                if (color.Value != value)
                {
                    color.Value = value;
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