using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace pda.Model
{
    [Table("User")]
    public class User
    {
        [PrimaryKey,AutoIncrement]
        public int userId { get; set; }
        [Unique]
        public string mail { get; set; }
        [MaxLength(20)]
        public string password { get; set; }
    }
}
