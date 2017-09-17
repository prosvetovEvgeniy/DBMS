using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.Parser;

namespace DBMS.Core.Manager
{
    class TableManager : BaseManager
    {
        public TableManager()
        {

        }

        public List<string> getFields(string dbName, string tableName)
        {
            List<string> fields = new List<string>();

            //DescriptionParser dParser = new DescriptionParser(pathToFile);

            return fields;
        }
    }
}
