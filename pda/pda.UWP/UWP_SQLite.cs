using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using pda.UWP;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(UWP_SQLite))]
namespace pda.UWP
{
    public class UWP_SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "localdb.MyDictionary";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
