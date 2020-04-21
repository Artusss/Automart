using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class ColorSQLiteHelper
    {
        SQLiteConnection database;

        public ColorSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<ColorViewModel>();
        }

        public IEnumerable<ColorViewModel> GetItems()
        {
            return database.Table<ColorViewModel>();
        }

        public void SaveItems(List<ColorViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}

