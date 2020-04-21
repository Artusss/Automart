using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class YearSQLiteHelper
    {
        SQLiteConnection database;

        public YearSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<YearViewModel>();
        }

        public IEnumerable<YearViewModel> GetItems()
        {
            return database.Table<YearViewModel>();
        }

        public void SaveItems(List<YearViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}
