using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Connection
    {
        public const string SLAVE_CONNECTION = "slave";
        public const string MASTER_CONNECTION = "master";

        private string column;
        private string tableName;
        private string linkedColumn;
        private string linkedTableName;
        private string connectionType;
        private bool isMaster;
        private bool isSlave;

        public Connection(string column, string tableName, string linkedColumn, string linkedTableName, string connectionType)
        {
            this.column = column;
            this.tableName = tableName;
            this.linkedColumn = linkedColumn;
            this.linkedTableName = linkedTableName;
            this.connectionType = connectionType;

            if(this.connectionType == SLAVE_CONNECTION)
            {
                isMaster = false;
                isSlave = true;
            }
            else if(this.connectionType == MASTER_CONNECTION)
            {
                isMaster = true;
                isSlave = false;
            }
        }

        //getters
        public string Column
        {
            get { return column; }
        }

        public string TableName
        {
            get { return tableName; }
        }

        public string LindkedColumn
        {
            get { return linkedColumn; }
        }

        public string LinkedTableName
        {
            get { return linkedTableName; }
        }

        public string ConnectionType
        {
            get { return connectionType; }
        }

        public bool IsMaster
        {
            get { return isMaster; }
        }

        public bool IsSlave
        {
            get { return isSlave; }
        }

        public List<string> getConnectionsAsList()
        {
            List<string> list = new List<string>();

            list.Add(column);
            list.Add(tableName);
            list.Add(linkedTableName);
            list.Add(linkedColumn);

            return list;
        }
    }
}
