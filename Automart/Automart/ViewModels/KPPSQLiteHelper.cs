using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class KPPSQLiteHelper
    {
        SQLiteConnection database;

        public KPPSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<KPPViewModel>();
        }

        public IEnumerable<KPPViewModel> GetItems()
        {
            return database.Table<KPPViewModel>();
        }

        public void SaveItems(List<KPPViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }

        public void DeleteAll()
        {
            database.DeleteAll<KPPViewModel>();
        }
    }
}
