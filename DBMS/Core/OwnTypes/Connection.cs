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

        public Connection(string column, string tableName, string linkedColumn, string linkedTableName)
        {
            this.column = column;
            this.tableName = tableName;
            this.linkedColumn = linkedColumn;
            this.linkedTableName = linkedTableName;
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

    }
}
