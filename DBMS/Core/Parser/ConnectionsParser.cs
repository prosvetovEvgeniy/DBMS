using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.OwnTypes;
using System.IO;

namespace DBMS.Core.Parser
{
    class ConnectionsParser : BaseParser
    {
        public ConnectionsParser(string filePath) : base(filePath)
        {
            
        }

        public List<Connection> getTableConnections(string tableName)
        {
            string data = File.ReadAllText(this.filePath, Encoding.GetEncoding(1251));
            List<Connection> connections = new List<Connection>();

            string[] separatedFields = data.Split(',');

            for (int i = 0; i < separatedFields.Length; i++)
            {
                try
                {
                    string[] parsedData = separatedFields[i].Split(':');
                    connections.Add(new Connection(parsedData[0], tableName, parsedData[1], parsedData[2], parsedData[3]));
                }
                catch {
                    
                }
            }

            return connections;
        }
    }
}
