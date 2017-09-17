using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.Manager
{
    class BaseManager
    {
        protected const string DATABASES_DIR_NAME = @"databases\";
        protected string dbPath; //путь к базам данных

        public BaseManager()
        {
            this.dbPath = getPath();
        }

        //получает путь к базам данных(к папке databases)
        protected string getPath()
        {
            string path = Environment.CurrentDirectory;
            path = path.Replace(@"bin\Debug", "");
            path += DATABASES_DIR_NAME;

            return path;
        }

        //возращает путь к базам данных
        public string getDbPath()
        {
            return dbPath;
        }

        //возвращает путь к связям таблицы
        public string getPathToTableConnections(string dbName, string tableName)
        {
            string path = this.dbPath + dbName + @"\tables\" + tableName + @"\connections\";
            return path;
        }

        //возвращает путь к связям таблицы
        public string getPathToTableData(string dbName, string tableName)
        {
            string path = this.dbPath + dbName + @"\tables\" + tableName + @"\data\";
            return path;
        }

        //возвращает путь к связям таблицы
        public string getPathToTableDescription(string dbName, string tableName)
        {
            string path = this.dbPath + dbName + @"\tables\" + tableName + @"\description\";
            return path;
        }
    }
}
