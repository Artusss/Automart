using System;
using System.Collections.Generic;
using System.Text;

namespace Automart.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime Created_at { get; set; }
    }
}
