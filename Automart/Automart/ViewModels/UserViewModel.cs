using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;
using SQLite;

namespace Automart.ViewModels
{
    [Table("Users_dev04")]
    public class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private User user;

        public UserViewModel()
        {
            user = new User();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return user.Id; }
            set
            {
                if (user.Id != value)
                {
                    user.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        [MaxLength(15), Unique]
        public string Login
        {
            get { return user.Login; }
            set
            {
                if (user.Login != value)
                {
                    user.Login = value;
                    OnPropertyChanged("Login");
                }
            }
        }

        public string Password
        {
            get { return user.Password; }
            set
            {
                if (user.Password != value)
                {
                    user.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                if (user.FirstName != value)
                {
                    user.FirstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get { return user.LastName; }
            set
            {
                if (user.LastName != value)
                {
                    user.LastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }

        public string City
        {
            get { return user.City; }
            set
            {
                if (user.City != value)
                {
                    user.City = value;
                    OnPropertyChanged("City");
                }
            }
        }

        public DateTime Created_at
        {
            get { return user.Created_at; }
            set
            {
                if (user.Created_at != value)
                {
                    user.Created_at = value;
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
