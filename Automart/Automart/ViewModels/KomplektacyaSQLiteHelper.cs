using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Newtonsoft.Json;
using Automart.ViewModels.Sctructs;

namespace Automart.ViewModels
{
    public class KomplektacyaSQLiteHelper
    {
        SQLiteConnection database;

        public KomplektacyaSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<KomplektacyaViewModel>();
        }

        public IEnumerable<KomplektacyaViewModel> GetItems()
        {
            return database.Table<KomplektacyaViewModel>();
        }

        public KomplektacyaViewModel GetItem(int Id)
        {
            return database.Get<KomplektacyaViewModel>(Id);
        }

        public int DeleteItem(int Id)
        {
            return database.Delete<KomplektacyaViewModel>(Id);
        }

        public int SaveItem(KomplektacyaViewModel KomplektacyaViewModel)
        {
            if (KomplektacyaViewModel.Id != 0)
            {
                database.Update(KomplektacyaViewModel);
                return KomplektacyaViewModel.Id;
            }
            database.Insert(KomplektacyaViewModel);
            return KomplektacyaViewModel.Id;
        }

        public KomplektacyaViewModel GetByAd(int Ad_id)
        {
            var KomplektacyaVM = from Komplektacya in database.Table<KomplektacyaViewModel>()
                                 where Komplektacya.AdId.Equals(Ad_id)
                                 select Komplektacya;
            return KomplektacyaVM.ToList().FirstOrDefault();
        }

        public KomplektacyaViewModel GetDefaultKomplektacyaVM(int Ad_id)
        {
            return new KomplektacyaViewModel()
            {
                AdId              = Ad_id,
                Safety            = JsonConvert.SerializeObject(new Safety()),
                Lightning         = JsonConvert.SerializeObject(new Lightning()),
                Heating           = JsonConvert.SerializeObject(new Heating()),
                Comfort           = JsonConvert.SerializeObject(new Comfort()),
                Exterior          = JsonConvert.SerializeObject(new Exterior()),
                SecuritySys       = JsonConvert.SerializeObject(new SecuritySys()),
                Adjustments       = JsonConvert.SerializeObject(new Adjustments()),
                Interior          = JsonConvert.SerializeObject(new Interior()),
                ExtraKomplektacya = JsonConvert.SerializeObject(new ExtraKomplektacya()),
                Created_at        = DateTime.Now
            };
        }
    }
}
