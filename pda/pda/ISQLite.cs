using System;
using System.Collections.Generic;
using System.Text;

namespace pda
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
