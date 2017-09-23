using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.Parser;
using DBMS.Core.OwnTypes;

namespace DBMS.Core.Manager
{
    class TableManager : BaseManager
    {
        public TableManager()
        {

        }

        public List<Description> getFields(string dbName, string tableName)
        {
            string pathToFile = base.getPathToTableDescription(dbName, tableName);
            DescriptionParser dParser = new DescriptionParser(pathToFile);

            return dParser.getFields();
        }
    }
}
