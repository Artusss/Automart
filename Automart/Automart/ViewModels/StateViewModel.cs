﻿using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;

namespace Automart.ViewModels
{
    public class StateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private State state;

        public StateViewModel()
        {
            state = new State();
        }
        public int Id
        {
            get { return state.Id; }
            set
            {
                if (state.Id != value)
                {
                    state.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public int AdId
        {
            get { return state.AdId; }
            set
            {
                if (state.AdId != value)
                {
                    state.AdId = value;
                    OnPropertyChanged("AdId");
                }
            }
        }

        public string Type
        {
            get { return state.Type; }
            set
            {
                if (state.Type != value)
                {
                    state.Type = value;
                    OnPropertyChanged("Type");
                }
            }
        }
        public string Status
        {
            get { return state.Status; }
            set
            {
                if (state.Status != value)
                {
                    state.Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        public string WhoClosed
        {
            get { return state.WhoClosed; }
            set
            {
                if (state.WhoClosed != value)
                {
                    state.WhoClosed = value;
                    OnPropertyChanged("WhoClosed");
                }
            }
        }

        public DateTime Created_at
        {
            get { return state.Created_at; }
            set
            {
                if (state.Created_at != value)
                {
                    state.Created_at = value;
                    OnPropertyChanged("Created_at");
                }
            }
        }
        public DateTime Closed_at
        {
            get { return state.Created_at; }
            set
            {
                if (state.Created_at != value)
                {
                    state.Created_at = value;
                    OnPropertyChanged("Closed_at");
                }
            }
        }

        public string WhoClosedField
        {
            get
            {
                return string.Format("Кто перевел: {0}", this.WhoClosed);
            }
        }

        public string StatusField
        {
            get
            {
                return string.Format("Статус: {0}", this.Status);
            }
        }

        public string Created_atField
        {
            get
            {
                return string.Format("Создан: {0:MM.dd.yyyy}", this.Created_at);
            }
        }
        public string Closed_atField
        {
            get
            {
                return string.Format("Закрыт: {0:MM.dd.yyyy}", this.Closed_at);
            }
        }
        
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}