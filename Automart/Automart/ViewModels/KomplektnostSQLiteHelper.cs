using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class KomplektnostSQLiteHelper
    {
        SQLiteConnection database;

        public KomplektnostSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<KomplektnostViewModel>();
        }

        public IEnumerable<KomplektnostViewModel> GetItems()
        {
            return database.Table<KomplektnostViewModel>();
        }

        public KomplektnostViewModel GetItem(int Id)
        {
            return database.Get<KomplektnostViewModel>(Id);
        }

        public int DeleteItem(int Id)
        {
            return database.Delete<KomplektnostViewModel>(Id);
        }

        public int SaveItem(KomplektnostViewModel komplektnostViewModel)
        {
            if (komplektnostViewModel.Id != 0)
            {
                database.Update(komplektnostViewModel);
                return komplektnostViewModel.Id;
            }
            database.Insert(komplektnostViewModel);
            return komplektnostViewModel.Id;
        }

        public KomplektnostViewModel GetByAd(int Ad_id)
        {
            var KomplektnostVM = from Komplektnost in database.Table<KomplektnostViewModel>()
                        where Komplektnost.AdId.Equals(Ad_id)
                        select Komplektnost;
            return KomplektnostVM.ToList().FirstOrDefault();
        }

        public KomplektnostViewModel GetDefaultKomplektnostVM(int Ad_id)
        {
            return new KomplektnostViewModel()
            {
                AdId              = Ad_id,
                KeyCollection     = 0,
                WheelCollection   = 0,
                Zapaska           = "Отсутствует",
                MatCollection     = "Отсутствует",
                ExtraKomplektnost = "",
                PTS               = false,
                Rukov             = false,
                Aptechka          = false,
                BoltKey           = false,
                ServiceBook       = false,
                Tools             = false,
                FireExt           = false,
                Jack              = false,
                RegCert           = false,
                Triangle          = false,
                BaloonKey         = false,
                Compressor        = false,
                Created_at        = DateTime.Now
            };
        }
    }
}
