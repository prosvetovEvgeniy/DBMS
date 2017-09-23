using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.Parser
{
    class BaseParser
    {
        protected string filePath;

        protected const string DESCRIPTION_FILE_NAME = "description.txt";
        protected const string DATA_FILE_NAME = "data.txt";
        protected const string CONNECTIONS_FILE_NAME = "connections.txt";

        public BaseParser(string filePath)
        {
            this.filePath = filePath;
        }
    }
}
