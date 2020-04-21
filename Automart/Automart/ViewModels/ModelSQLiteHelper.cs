using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Automart.ViewModels
{
    public class ModelSQLiteHelper
    {
        SQLiteConnection database;

        public ModelSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<ModelcViewModel>();
        }

        public IEnumerable<ModelcViewModel> GetItems()
        {
            return database.Table<ModelcViewModel>();
        }

        public void SaveItems(List<ModelcViewModel> MarkVMs)
        {
            foreach (var MarkVM in MarkVMs)
            {
                if (MarkVM.Id != 0) database.Update(MarkVM);
                database.Insert(MarkVM);
            }
        }
    }
}
