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
            return new Table("undefined");
        }

        public string Name {
            get { return this.dbName; }
        }

        public int CountTables
        {
            get { return tables.Count; }
        }
    }
}
