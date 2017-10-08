using System.Collections.Generic;

namespace DBMS.Core.OwnTypes
{
    class Database
    {
        private List<Table> tables;
        private string dbName;

        public Database()
        {
            tables = new List<Table>();
        }

        //checkers
        public string checkConnectionsOnDelete(string tableName, int numRow)
        {
            Table table = this.getTableByName(tableName);
            List<Connection> connections = table.getConnections();

            if (connections == null)
            {
                return "";
            }

            foreach (Connection connection in connections)
            {
                Table linkedTable = this.getTableByName(connection.LinkedTableName);

                List<string> dataByColumn = linkedTable.getDataByColumn(connection.LindkedColumn);
                string valueForSearch = table.getRowByIndex(numRow).getContentByFieldName(connection.Column);

                if(connection.ConnectionType == "master")
                {
                    if (dataByColumn.Contains(valueForSearch))
                    {
                        string message = "Cannot delete a parent row: a foreign key constraint fails" + 
                            "`" + dbName + "`." + connection.LinkedTableName +
                            " has FOREIGN KEY `" + connection.LindkedColumn + "` REFERENCES `" +
                            connection.TableName + "` (`" + connection.Column + "`)"; 

                        return message;
                    }
                }
            }

            return "";
        }
        
        public string checkConnectionsOnAdd(string tableName, List<List<string>> rows)
        {
            Table table = this.getTableByName(tableName);
            List<Connection> connections = table.getConnections();

            if (connections == null)
            {
                return "";
            }

            foreach (Connection connection in connections)
            {
                Table linkedTable = this.getTableByName(connection.LinkedTableName);

                List<string> rowsByColumn = table.getDataByColumn(connection.Column);
                List<string> rowsByColumnLinkedTable = linkedTable.getDataByColumn(connection.LindkedColumn);

                foreach(string rowByColumn in rowsByColumn)
                {
                    bool flag = false;
                    foreach(string rowByColumnLinkedTable in rowsByColumnLinkedTable)
                    {
                        if(rowByColumn == rowByColumnLinkedTable)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        string message = "Cannot add a parent row: table `" + connection.LinkedTableName + "` " +
                            "has not does not have this value: '" + rowByColumn + "'";
                        return message;
                    }
                }
            }

            return "";
        }

        public bool checkTableHasPK(string tableName)
        {
            Table table = getTableByName(tableName);

            List<Description> fields = table.getFields();

            foreach(Description field in fields)
            {
                if (field.PrimaryKey)
                {
                    return true;
                }
            }

            return false;
        }
        //setters
        public void setDbName(string dbName)
        {
            this.dbName = dbName;
        }
        
        //clears
        public void clearTables()
        {
            tables = new List<Table>();
        }
        
        public void clearTableByName(string name)
        {
            for(int i = 0; i < tables.Count; i++)
            {
                if(tables[i].TableName == name)
                {
                    tables.RemoveAt(i);
                    break;
                }
            }
        }

        public void clearTableByIndex(int index)
        {
            tables.RemoveAt(index);
        }

        //adders
        public void addTable(Table table)
        {
            tables.Add(table);
        }

        //getters
        public Table getTableByIndex(int index)
        {
            return tables[index];
        }

        public List<string> getTableNamesWithPK()
        {
            List<string> tableNames = new List<string>();

            foreach (Table table in tables)
            {
                if (table.hasPK())
                {
                    tableNames.Add(table.TableName);
                }
            }

            return tableNames;
        }

        public Table getTableByName(string name)
        {
            foreach(Table table in tables)
            {
                if(table.TableName == name)
                {
                    return table;
                }
            }
            return new Table("undefined", dbName);
        }

        public List<string> getTableNames()
        {
            List<string> tableNames = new List<string>();

            foreach(Table table in tables)
            {
                tableNames.Add(table.TableName);
            }

            return tableNames;
        }

        public string Name {
            get { return this.dbName; }
        }

        public int CountTables
        {
            get { return tables.Count; }
        }

        public List<string> getDataByColumnFromMasterTable(string tableName, string columnName)
        {
            List<string> dataByColumn = new List<string>();

            foreach(Table table in tables)
            {
                if(table.TableName == tableName)
                {
                    dataByColumn = table.getDataByColumn(columnName);
                    break;
                }
            }

            return dataByColumn;
        }

        //others

        public bool verifyСonnectivity(string columnName, string tableName, string linkedColumnName, string linkedTableName)
        {
            Table table = this.getTableByName(tableName);
            Table linkedTable = this.getTableByName(linkedTableName);

            List<string> dataByColumn = table.getDataByColumn(columnName);
            List<string> linkedDataByColumn = linkedTable.getDataByColumn(linkedColumnName);

            Description field = table.getFieldByName(columnName);
            Description linkedField = linkedTable.getFieldByName(linkedColumnName);

            //если slave таблица не имеет значение, то можно создать связь
            if(dataByColumn.Count == 0)
            {
                return true;
            }

            if(dataByColumn.Count != 0 && linkedDataByColumn.Count == 0)
            {
                if (field.DefaultNULL)
                {
                    foreach(string cell in dataByColumn)
                    {
                        if(cell != "")
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }

                return true;
            }

            foreach(string cell in dataByColumn)
            {
                bool flag = false;
                if(cell != "")
                {
                    foreach (string linkedCell in linkedDataByColumn)
                    {
                        if (cell == linkedCell)
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void connectTables(string columnName, string tableName, string linkedColumnName, string linkedTableName)
        {
            Table table = this.getTableByName(tableName);
            Table linkedTable = this.getTableByName(linkedTableName);

            Connection slaveConnection = new Connection(columnName, tableName, linkedColumnName, linkedTableName, Connection.SLAVE_CONNECTION);
            Connection masterConnection = new Connection(linkedColumnName, linkedTableName, columnName, tableName, Connection.MASTER_CONNECTION);

            table.addConnection(slaveConnection);
            linkedTable.addConnection(masterConnection);
        }

        public void deleteConnection(string columnName, string tableName, string linkedColumnName, string linkedTableName)
        {
            Table table = this.getTableByName(tableName);
            Table linkedTable = this.getTableByName(linkedTableName);

            table.removeConnection(columnName, linkedColumnName, linkedTableName);
            linkedTable.removeConnection(linkedColumnName, columnName, tableName);
        }
    }
}
