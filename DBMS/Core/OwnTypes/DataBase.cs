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

        public Database()
        {
            tables = new List<Table>();
        }

        public void clearTables()
        {
            tables = new List<Table>();
        }

        public void addTable(Table table)
        {
            tables.Add(table);
        }

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

        public int CountTables
        {
            get { return tables.Count; }
        }
    }
}
