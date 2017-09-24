using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Table
    {
        private string tableName;
        private List<Data> data;
        private List<Connections> connections;
        private List<Description> fields;

        public Table(string tableName)
        {
            this.tableName = tableName;

            data = new List<Data>();
            connections = new List<Connections>();
            fields = new List<Description>();
        }
        
        public void setTableName(string name)
        {
            this.tableName = name;
        }

        public string TableName {
            get { return tableName; }
        }

        public void setData(List<Data> data)
        {
            this.data = data;
        }

        public void addConnection(Connections connection)
        {
            this.connections.Add(connection);
        }

        public void setFields(List<Description> fields)
        {
            this.fields = fields;
        }

        public List<Data> getData()
        {
            return data;
        }

        public List<Connections> getConnections()
        {
            return connections;
        }

        public List<Description> getFields()
        {
            return fields;
        }
    }
}
