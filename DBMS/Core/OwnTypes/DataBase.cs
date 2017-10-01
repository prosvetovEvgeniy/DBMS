using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
