using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Connection
    {
        private string column;
        private string tableName;
        private string linkedColumn;
        private string linkedTableName;
        private string connectionType;

        public Connection(string column, string tableName, string linkedColumn, string linkedTableName, string connectionType)
        {
            this.column = column;
            this.tableName = tableName;
            this.linkedColumn = linkedColumn;
            this.linkedTableName = linkedTableName;
            this.connectionType = connectionType;
        }

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
    }
}
