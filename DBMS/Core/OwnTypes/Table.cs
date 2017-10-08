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
        public void saveRowsInFile()
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

            File.WriteAllText(fm.getPathToTableData(dbName, tableName), data, Encoding.GetEncoding(1251));
        }

        private void saveConnectionsInFile()
        {
            FileManager fm = new FileManager();
            string data = "";

            foreach(Connection connection in connections)
            {
                data += connection.Column + ":"
                    + connection.LindkedColumn + ":"
                    + connection.LinkedTableName + ":"
                    + connection.ConnectionType + ",";
            }

            File.WriteAllText(fm.getPathToTableConnections(dbName, tableName), data, Encoding.GetEncoding(1251));
        }

        private void saveDescriptionInFile()
        {
            FileManager fm = new FileManager();
            string data = "";

            foreach(Description field in fields)
            {
                data += field.FieldName + ":" + field.FieldType + ":";

                if (field.DefaultNULL)
                {
                    data += Description.DEFAULT_NULL;
                }
                else
                {
                    data += Description.NOT_NULL;
                }

                if (field.PrimaryKey)
                {
                    data += ":" + Description.PK;
                }

                if (field.Index)
                {
                    data += ":" + Description.INDEX;
                }

                data += ",";
            }

            File.WriteAllText(fm.getPathToTableDescription(dbName, tableName), data, Encoding.GetEncoding(1251));
        }

        //adders
        public void addConnection(Connection connection)
        {
            this.connections.Add(connection);

            saveConnectionsInFile();
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

        public void addField(string fieldName, string type, bool defaultNull, bool isIndex, bool pk)
        {
            Description newField = new Description(fieldName, type, pk, isIndex);
            newField.setDefaultNull(defaultNull);

            fields.Add(newField);

            foreach(Row row in rows)
            {
                row.addField(newField.FieldName);
                row.addContent("");
            }

            saveDescriptionInFile();
            saveRowsInFile();
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

        public void removeConnection(string columnName, string linkedColumnName, string linkedTableName)
        {

            foreach (Connection connection in connections)
            {
                if(connection.Column == columnName &&
                   connection.LindkedColumn == linkedColumnName &&
                   connection.LinkedTableName == linkedTableName)
                {
                    connections.Remove(connection);
                    break;
                }
            }

            saveConnectionsInFile();
        }

        public void removeField(string fieldName) {

            foreach(Description field in fields)
            {
                if(field.FieldName == fieldName)
                {
                    fields.Remove(field);
                    break;
                }
            }

            foreach(Row row in rows)
            {
                row.removeField(fieldName);
            }

            saveDescriptionInFile();
            saveRowsInFile();
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

        public Description getFieldByName(string name)
        {
            foreach(Description field in fields)
            {
                if(field.FieldName == name)
                {
                    return field;
                }
            }

            throw new Exception("Запрашивается не существующий столбец");
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

            throw new Exception("Связей не найдено");
        }
        
        public List<string> getFieldsWithoutConnections()
        {
            List<string> names = new List<string>();

            foreach (Description field in fields)
            {
                bool flag = true;

                if (!field.PrimaryKey && field.Index)
                {
                    foreach (Connection connection in connections)
                    {
                        if (field.FieldName == connection.Column)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        names.Add(field.FieldName);
                    }
                }
            }

            return names;
        }

        public string getPrimaryKeyName()
        {
            foreach(Description field in fields)
            {
                if (field.PrimaryKey)
                {
                    return field.FieldName;
                }
            }

            throw new Exception("Таблица не имеет первичного ключа");
        }

        public List<string> getSuitableFieldsForConnections()
        {
            List<string> suitableFields = new List<string>();

            foreach(Description field in fields)
            {
                if(field.PrimaryKey || field.Index)
                {
                    suitableFields.Add(field.FieldName);
                }
            }

            return suitableFields;
        }

        //has
        public bool hasFieldsWithoutForeignKey()
        {
            foreach(Description field in fields)
            {
                bool flag = true;

                if (!field.PrimaryKey && field.Index)
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

        public int getCountFieldsWithoutPK()
        {
            if (hasPK())
            {
                return fields.Count - 1;
            }
            else
            {
                return fields.Count;
            }
        }

        public bool fieldHasConnection(string fieldName)
        {
            foreach(Connection connection in connections)
            {
                if(fieldName == connection.Column && connection.hasConnection())
                {
                    return true;
                }
            }

            return false;
        }

        public bool fieldNameFree(string fieldName)
        {
            foreach(Description field in fields)
            {
                if(field.FieldName == fieldName)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
