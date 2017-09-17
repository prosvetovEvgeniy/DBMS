using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBMS.Core
{
    class FileManager
    {
        private const string DATABASES_DIR_NAME = @"databases\";
        private string dbPath; 

        public FileManager()
        {
            this.dbPath = getPath();
        }
       
        private string getPath()
        {
            string path = Environment.CurrentDirectory;
            path = path.Replace(@"bin\Debug", "");
            path += DATABASES_DIR_NAME;

            return path;
        }
        public string getDbPath()
        {
            return dbPath;
        }
        public List<string> getDatabases()
        {
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(this.dbPath));

            for(int i = 0; i < dirs.Count; i++)
            {
                dirs[i] = dirs[i].Replace(this.dbPath, "");
            }

            return dirs;
        }
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
