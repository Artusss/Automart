using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class MarkSQLiteHelper
    {
        SQLiteConnection database;

        public MarkSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<MarkViewModel>();
        }

        public IEnumerable<MarkViewModel> GetItems()
        {
            return database.Table<MarkViewModel>();
        }

        public void SaveItems(List<MarkViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}
