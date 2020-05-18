using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Newtonsoft.Json;

namespace Automart.ViewModels
{
    public class AdCommentSQLiteHelper
    {
        SQLiteConnection database;

        public AdCommentSQLiteHelper(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<AdCommentViewModel>();
        }

        public IEnumerable<AdCommentViewModel> GetItems()
        {
            return database.Table<AdCommentViewModel>();
        }

        public AdCommentViewModel GetItem(int Id)
        {
            return database.Get<AdCommentViewModel>(Id);
        }

        public int DeleteItem(int Id)
        {
            return database.Delete<AdCommentViewModel>(Id);
        }

        public int SaveItem(AdCommentViewModel adCommentViewModel)
        {
            if (adCommentViewModel.Id != 0)
            {
                database.Update(adCommentViewModel);
                return adCommentViewModel.Id;
            }
            database.Insert(adCommentViewModel);
            return adCommentViewModel.Id;
        }

        public AdCommentViewModel GetByAd(int Ad_id)
        {
            var AdCommentVM = from AdComment in database.Table<AdCommentViewModel>()
                                 where AdComment.AdId.Equals(Ad_id)
                                 select AdComment;
            return AdCommentVM.ToList().FirstOrDefault();
        }
    }
}
