using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class AdComment
    {
        public int Id { get; set; }
        public int AdId { get; set; }

        public string Text { get; set; }

        public DateTime Created_at { get; set; }
    }
}
