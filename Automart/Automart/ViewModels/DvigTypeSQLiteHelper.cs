using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class DvigTypeSQLiteHelper
    {
        SQLiteConnection database;

        public DvigTypeSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<DvigTypeViewModel>();
        }

        public IEnumerable<DvigTypeViewModel> GetItems()
        {
            return database.Table<DvigTypeViewModel>();
        }

        public void SaveItems(List<DvigTypeViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}
