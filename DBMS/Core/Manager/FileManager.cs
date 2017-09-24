using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DBMS.Core.Manager
{
    class FileManager
    {
        protected const string DESCRIPTION_FILE_NAME = "description.txt";
        protected const string DATA_FILE_NAME = "data.txt";
        protected const string CONNECTIONS_FILE_NAME = "connections.txt";

        protected const string DATABASES_DIR_NAME = @"databases\";
        protected string dbPath; //путь к базам данных

        public FileManager()
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
            string path = this.dbPath + dbName + @"\tables\" + tableName + @"\connections\" + CONNECTIONS_FILE_NAME;
            return path;
        }

        //возвращает путь к строкам таблицы
        public string getPathToTableData(string dbName, string tableName)
        {
            string path = this.dbPath + dbName + @"\tables\" + tableName + @"\data\" + DATA_FILE_NAME;
            return path;
        }

        //возвращает путь к описанию таблицы
        public string getPathToTableDescription(string dbName, string tableName)
        {
            string path = this.dbPath + dbName + @"\tables\" + tableName + @"\description\" + DESCRIPTION_FILE_NAME;
            return path;
        }
    }
}
