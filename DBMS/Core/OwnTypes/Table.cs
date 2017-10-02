using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.Manager;
using System.IO;

namespace DBMS.Core.OwnTypes
{
    class Table
    {
        private string tableName;
        private string dbName;
        private List<Row> rows;
        private List<Connection> connections;
        private List<Description> fields;

        public Table(string tableName, string dbName)
        {
            this.tableName = tableName;
            this.dbName = dbName;

            rows = new List<Row>();
            connections = new List<Connection>();
            fields = new List<Description>();
        }
        
        public bool checkFieldHasSlaveConnection(string fieldName)
        {
            foreach(Connection connection in connections)
            {
                if(connection.Column == fieldName && connection.ConnectionType == "slave")
                {
                    return true;
                }
            }

            return false;
        }

        //savers
        public void save()
        {
            FileManager fm = new FileManager();
            string data = "";

            foreach(Row row in rows)
            {
                data += "{\r\n";
                for(int i = 0; i < row.CountFields; i++)
                {
                    data += row.getFieldByIndex(i) + ":" + row.getFieldContentByIndex(i) + "\r\n";
                }
                data += "}\r\n";
            }

            File.WriteAllText(fm.getPathToTableData(dbName, tableName), data, System.Text.Encoding.GetEncoding(1251));
        }

        //adders
        public void addConnection(Connection connection)
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
        public void removeAllRows()
        {
            rows.Clear();
            rows = new List<Row>();
        }

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

        public void setConnections(List<Connection> connections)
        {
            this.connections = connections;
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

        public List<Connection> getConnections()
        {
            return connections;
        }

        public Connection getConnectionByIndex(int index)
        {
            return connections[index];
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

        public int CountConnections
        {
            get { return connections.Count; }
        }

        public List<string> getDataByColumn(string columnName)
        {
            List<string> columnData = new List<string>();

            foreach(Row row in rows)
            {
                List<string> columnNames= row.getFields();
                List<string> content = row.getContent();

                for(int i = 0; i < columnNames.Count; i++)
                {
                    if(columnNames[i] == columnName)
                    {
                        columnData.Add(content[i]);
                    }
                }
            }

            return columnData;
        } 

        public Connection getConnectionByColumnName(string columnName)
        {
            foreach(Connection connection in connections)
            {
                if(connection.Column == columnName)
                {
                    return connection;
                }
            }

            return null;
        }

        //has
        public bool hasFieldsWithoutForeignKey()
        {
            foreach(Description field in fields)
            {
                bool flag = true;

                if (!field.PrimaryKey)
                {
                    foreach (Connection connection in connections)
                    {
                        if (field.FieldName == connection.Column)
                        {
                            flag = false;
                        }
                    }

                    if (flag)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool hasPK()
        {
            foreach(Description field in fields)
            {
                if (field.PrimaryKey)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
