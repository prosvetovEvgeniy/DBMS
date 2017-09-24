using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.OwnTypes;
using System.Text.RegularExpressions;

namespace DBMS.Core.Checker
{
    static class TypeChecker
    {
        private const string INTEGER_PATTERN = @"^[0-9]+$";
        private const string FLOAT_PATTERN = @"\-?\d+(\.\d{0,})?";
        private const string STRING_PATTERN = @"^[а-яА-ЯёЁa-zA-Z0-9 .,<>']+$";

        public static bool checkTypes(List<List<string>> rows, Table table)
        {
            foreach(List<string> row in rows)
            {
                int i = 0;
                foreach(string cell in row)
                {
                    if(table.getFieldByIndex(i).FieldType == "integer")
                    {
                        if(!(Regex.IsMatch(cell, INTEGER_PATTERN)))
                        {
                            return false;
                        }
                    }
                    else if(table.getFieldByIndex(i).FieldType == "float")
                    {
                        if (!(Regex.IsMatch(cell, FLOAT_PATTERN)))
                        {
                            return false;
                        }
                    }
                    else if(table.getFieldByIndex(i).FieldType == "string")
                    {
                        if (!(Regex.IsMatch(cell, STRING_PATTERN)))
                        {
                            return false;
                        }
                    }
                    i++;
                }
            }
            return true;
        }
    }
}
