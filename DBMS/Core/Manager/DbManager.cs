using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBMS.Core.Manager
{
    class DbManager : FileManager
    {

        public DbManager()
        {
            
        }

        //вовращает имена баз данных ввиде строк
        public List<string> getDatabases()
        {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(this.dbPath));

            for(int i = 0; i < dirs.Count; i++)
            {
                dirs[i] = dirs[i].Replace(this.dbPath, "");
            }

            return dirs;
        }

        //возращает имена таблиц для определенной бд в виде строк
        public List<string> getTables(string dbName)
        {
            string pathToTables = this.dbPath + dbName + @"\tables\";
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(pathToTables));

            for (int i = 0; i < dirs.Count; i++)
            {
                dirs[i] = dirs[i].Replace(pathToTables, "");
            }

            return dirs;
        }
    }
}
