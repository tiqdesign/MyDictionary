using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace pda.Model
{
    public class Word
    {
        [PrimaryKey,AutoIncrement]
        public int word_id { get; set; }
        [Unique]
        public string word_tr { get; set; }
        [Unique]
        public string word_en { get; set; }
    }
}
