using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class UserSQLiteHelper
    {
        SQLiteConnection database;

        public UserSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<UserViewModel>();
        }

        public IEnumerable<UserViewModel> GetItems()
        {
            return database.Table<UserViewModel>();
        }

        public UserViewModel GetItem(int Id)
        {
            return database.Get<UserViewModel>(Id);
        }

        public int DeleteItem(int Id)
        {
            return database.Delete<UserViewModel>(Id);
        }

        public int SaveItem(UserViewModel user)
        {
            if (user.Id != 0)
            {
                database.Update(user);
                return user.Id;
            }
            return database.Insert(user);
        }

        public bool IssetToLogin(string login)
        {
            var curUser = from user in database.Table<UserViewModel>()
                          where user.Login.Equals(login) 
                          select user;
            if (curUser.ToList().Count == 0) return false;
            return true;
        }

        public IEnumerable<UserViewModel> GetToPassword(string login, string password)
        {
            var curUser = from user in database.Table<UserViewModel>()
                          where user.Login.Equals(login)
                          && user.Password.Equals(password)
                          select user;
            return curUser.ToList();
        }
    }
}
