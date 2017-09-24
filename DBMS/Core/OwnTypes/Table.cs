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
        private List<Line> lines;
        private List<Connections> connections;
        private List<Description> fields;

        public Table(string tableName)
        {
            this.tableName = tableName;

            lines = new List<Line>();
            connections = new List<Connections>();
            fields = new List<Description>();
        }

        //adders
        public void addConnection(Connections connection)
        {
            this.connections.Add(connection);
        }

        //setters
        public void setTableName(string name)
        {
            this.tableName = name;
        }
    
        public void setLines(List<Line> lines)
        {
            this.lines = lines;
        }

        public void setFields(List<Description> fields)
        {
            this.fields = fields;
        }

        //getters
        public Line getLineByIndex(int index)
        {
            return lines[index];
        }
        
        public Description getFieldByIndex(int index)
        {
            return fields[index];
        }

        public List<Line> getLines()
        {
            return lines;
        }

        public List<Connections> getConnections()
        {
            return connections;
        }

        public List<Description> getFields()
        {
            return fields;
        }

        public string TableName
        {
            get { return tableName; }
        }

        public int CountFields
        {
            get { return fields.Count; }
        }

        public int CountLines
        {
            get { return lines.Count; }
        }
    }
}
