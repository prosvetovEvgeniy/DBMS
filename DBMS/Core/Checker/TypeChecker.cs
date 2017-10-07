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
                    Description field = table.getFieldByIndex(i);

                    if(cell == "")
                    {
                        if (field.NotNULL)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (field.FieldType == Description.INTEGER_TYPE)
                        {
                            if (!(Regex.IsMatch(cell, INTEGER_PATTERN)))
                            {
                                return false;
                            }
                        }
                        else if (field.FieldType == Description.STRING_TYPE)
                        {
                            if (!(Regex.IsMatch(cell, STRING_PATTERN)))
                            {
                                return false;
                            }
                        }
                    }

                    i++;
                }
            }
            return true;
        }
    }
}
