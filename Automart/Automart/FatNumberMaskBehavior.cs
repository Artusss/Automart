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
                    value = getWhiteSpace(value, 1);
                    ((Entry)sender).Text = value;
                    return;
                }
                else if (value.Length == 6)
                {
                    value = delWhiteSpace(value, 1);
                    value = getWhiteSpace(value, 2);
                    ((Entry)sender).Text = value;
                    return;
                }
                else if (value.Length == 7)
                {
                    value = delWhiteSpace(value, 2);
                    value = getWhiteSpace(value, 3);
                    ((Entry)sender).Text = value;
                    return;
                }
                else if (value.Length == 8)
                {
                    value = delWhiteSpace(value, 3);
                    value = getWhiteSpace(value, 4);
                    value = getWhiteSpace(value, 1);
                    ((Entry)sender).Text = value;
                    return;
                }
                else if (value.Length == 10)
                {
                    value = delWhiteSpace(value, 1);
                    value = getWhiteSpace(value, 2);
                    value = delWhiteSpace(value, 4);
                    value = getWhiteSpace(value, 5);
                    ((Entry)sender).Text = value;
                    return;
                }

                ((Entry)sender).Text = value;
            }
        }

        public string getWhiteSpace(string Entry, int startIndex)
        {
            string newEntry = Entry.Insert(startIndex, "_");
            return newEntry;
        }
        public string delWhiteSpace(string Entry, int startIndex)
        {
            string newEntry = Entry.Remove(startIndex, 1);
            return newEntry;
        }
    }
}