using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Automart
{
    public class FatNumberMaskBehavior : Behavior<Entry>
    {
        public static FatNumberMaskBehavior Instance = new FatNumberMaskBehavior();

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                // If the new value is longer than the old value, the user is  
                if (args.OldTextValue != null && args.NewTextValue.Length < args.OldTextValue.Length)
                    return;

                var value = args.NewTextValue;

                if (value.Length == 4)
                {
                    ((Entry)sender).Text = getWhiteSpace(value);
                    return;
                }

                if (value.Length == 6)
                {
                    ((Entry)sender).Text = swapWhiteSpace(value, 1);
                    return;
                }

                if (value.Length == 7)
                {
                    ((Entry)sender).Text = swapWhiteSpace(value, 2);
                    return;
                }

                if (value.Length == 8)
                {
                    ((Entry)sender).Text = getWhiteSpace(value);
                    ((Entry)sender).Text = swapWhiteSpace(value, 4);
                    return;
                }

                if (value.Length == 10)
                {
                    ((Entry)sender).Text = swapWhiteSpace(value, 1);
                    ((Entry)sender).Text = swapWhiteSpace(value, 5);
                    return;
                }

                ((Entry)sender).Text = args.NewTextValue;
            }
        }

        public string getWhiteSpace(string Entry)
        {
            string newEntry = "";
            for (int i = 0; i < Entry.Length; i++)
            {
                if (i == 1) newEntry += "_";
                newEntry += Entry[i];
            }
            return newEntry;
        }
        public string swapWhiteSpace(string Entry, int startIndex)
        {
            var EntryArr = Entry.ToCharArray();
            EntryArr[startIndex] = EntryArr[startIndex + 1];
            EntryArr[startIndex + 1] = '_';
            return EntryArr.ToString();
        }
    }
}