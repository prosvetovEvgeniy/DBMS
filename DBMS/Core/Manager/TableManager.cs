﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.Parser;
using DBMS.Core.OwnTypes;

namespace DBMS.Core.Manager
{
    class TableManager : FileManager
    {
        public TableManager()
        {

        }

        public List<Description> getFields(string dbName, string tableName)
        {
            string pathToFile = base.getPathToTableDescription(dbName, tableName);
            DescriptionParser descriptionParser = new DescriptionParser(pathToFile);

            return descriptionParser.getFields();
        }

        public List<Row> getTableData(string dbName, string tableName)
        {
            string pathToFile = base.getPathToTableData(dbName, tableName);
            DataParser dataParser = new DataParser(pathToFile);

            return dataParser.getTableData();
        }
        
        public List<Connection> getTableConnections(string dbName, string tableName)
        {
            string pathToFile = base.getPathToTableConnections(dbName, tableName);
            ConnectionsParser connectionParser = new ConnectionsParser(pathToFile);

            return connectionParser.getTableConnections(tableName);
        }
    }
}
