using System;
using System.Collections.Generic;
using System.Text;
using Automart.Models;
using System.ComponentModel;
using SQLite;

namespace Automart.ViewModels
{
    [Table("AdComment_dev02")]
    public class AdCommentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AdComment adComment;

        public AdCommentViewModel()
        {
            adComment = new AdComment();
        }
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id
        {
            get { return adComment.Id; }
            set
            {
                if (adComment.Id != value)
                {
                    adComment.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public int AdId
        {
            get { return adComment.AdId; }
            set
            {
                if (adComment.AdId != value)
                {
                    adComment.AdId = value;
                    OnPropertyChanged("AdId");
                }
            }
        }

        public string Text
        {
            get { return adComment.Text; }
            set
            {
                if (adComment.Text != value)
                {
                    adComment.Text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
       
        public DateTime Created_at
        {
            get { return adComment.Created_at; }
            set
            {
                if (adComment.Created_at != value)
                {
                    adComment.Created_at = value;
                    OnPropertyChanged("Created_at");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
