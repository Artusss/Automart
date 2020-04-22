using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class DriveUnitSQLiteHelper
    {
        SQLiteConnection database;

        public DriveUnitSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<DriveUnitViewModel>();
        }

        public IEnumerable<DriveUnitViewModel> GetItems()
        {
            return database.Table<DriveUnitViewModel>();
        }

        public void SaveItems(List<DriveUnitViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}
