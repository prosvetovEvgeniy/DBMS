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
        private List<Row> rows;
        private List<Connections> connections;
        private List<Description> fields;

        public Table(string tableName)
        {
            this.tableName = tableName;

            rows = new List<Row>();
            connections = new List<Connections>();
            fields = new List<Description>();
        }

        //adders
        public void addConnection(Connections connection)
        {
            this.connections.Add(connection);
        }

        public void addRow(List<string> rowContent)
        {
            Row addedRow = new Row();
            for(int i = 0; i < rowContent.Count; i++)
            {
                addedRow.addField(this.fields[i].FieldName);
                addedRow.addContent(rowContent[i]);
            }

            rows.Add(addedRow);
        }
        //removers

        public void removeRowByIndex(int index)
        {
            rows.RemoveAt(index);
        }
        //setters
        public void setTableName(string name)
        {
            this.tableName = name;
        }
    
        public void setRows(List<Row> rows)
        {
            this.rows = rows;
        }

        public void setFields(List<Description> fields)
        {
            this.fields = fields;
        }

        //getters
        public Row getRowByIndex(int index)
        {
            return rows[index];
        }
        
        public Description getFieldByIndex(int index)
        {
            return fields[index];
        }

        public List<Row> getRows()
        {
            return rows;
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

        public int CountRows
        {
            get { return rows.Count; }
        }
    }
}
