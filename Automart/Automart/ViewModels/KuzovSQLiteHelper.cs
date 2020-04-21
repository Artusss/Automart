using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class KuzovSQLiteHelper
    {
        SQLiteConnection database;

        public KuzovSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<KuzovViewModel>();
        }

        public IEnumerable<KuzovViewModel> GetItems()
        {
            return database.Table<KuzovViewModel>();
        }

        public void SaveItems(List<KuzovViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}

