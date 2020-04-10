using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    class AdSQLiteHelper
    {
        SQLiteConnection database;

        public AdSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<AdViewModel>();
        }

        public IEnumerable<AdViewModel> GetItems()
        {
            return database.Table<AdViewModel>();
        }

        public AdViewModel GetItem(int Id)
        {
            return database.Get<AdViewModel>(Id);
        }

        public int DeleteItem(int Id)
        {
            return database.Delete<AdViewModel>(Id);
        }

        public int SaveItem(AdViewModel adVM)
        {
            if (adVM.Id != 0)
            {
                database.Update(adVM);
                return adVM.Id;
            }
            return database.Insert(adVM);
        }
    }
}
