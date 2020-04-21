using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Automart.Models;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Marks")]
    public class MarkViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Mark mark;

        public MarkViewModel()
        {
            mark = new Mark();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return mark.Id; }
            set
            {
                if (mark.Id != value)
                {
                    mark.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Value
        {
            get { return mark.Value; }
            set
            {
                if (mark.Value != value)
                {
                    mark.Value = value;
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
