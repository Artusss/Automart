using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    class State
    {
        public int Id { get; set; }
        public int AdId { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public string WhoClosed { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Closed_at { get; set; }
    }
}
