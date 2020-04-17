using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class AdSQLiteHelper
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
            database.Insert(adVM);
            return adVM.Id;
        }

        public List<AdViewModel> GetByUser(int User_id)
        {
            var adsVM = from ad in database.Table<AdViewModel>()
                        where ad.UserId.Equals(User_id) 
                        orderby ad.Created_at descending
                        select ad;
            return adsVM.ToList();
        }

        public AdViewModel GetById(int id)
        {
            var adsVM = from ad in database.Table<AdViewModel>()
                        where ad.Id.Equals(id)
                        select ad;
            return adsVM.ToList().FirstOrDefault();
        }
    }
}
