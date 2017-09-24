using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DBMS.Core.OwnTypes;

namespace DBMS.Core.Parser
{
    class DescriptionParser : BaseParser
    {
        public DescriptionParser(string filePath) : base(filePath)
        {
            
        }
        // парсит поля таблицы
        public List<Description> getFields()
        {
            string data = File.ReadAllText(this.filePath + DESCRIPTION_FILE_NAME, Encoding.Default);

            List<Description> fields = new List<Description>();

            string[] separatedFields = data.Split(',');

            for (int i = 0; i < separatedFields.Length; i++)
            {
                string[] parsedData = separatedFields[i].Split(':');

                if (parsedData.Length == 3)
                {
                    bool nullState = false;
                    if(parsedData[2] == "DEFAULT_NULL")
                    {
                        nullState = true;
                    }

                    fields.Add(new Description(parsedData[0], parsedData[1], nullState));
                }
                else if(parsedData.Length == 2)
                {
                    fields.Add(new Description(parsedData[0], parsedData[1]));
                }
            }

            return fields;
        }
    }
}
