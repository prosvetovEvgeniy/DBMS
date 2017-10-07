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
            string data = File.ReadAllText(this.filePath, Encoding.GetEncoding(1251));

            List<Description> fields = new List<Description>();

            string[] separatedFields = data.Split(',');

            for (int i = 0; i < separatedFields.Length; i++)
            {
                string[] parsedData = separatedFields[i].Split(':');

                if (parsedData.Length == 4)
                {
                    if (parsedData[3] == Description.PK)
                    {
                        Description field = new Description(parsedData[0], parsedData[1], true, false);

                        if (parsedData[2] == Description.NOT_NULL)
                        {
                            field.setDefaultNull(false);
                            field.setNotNull(true);
                        }
                        else
                        {
                            throw new Exception("PK должен быть NOT_NULL");
                        }

                        fields.Add(field);
                    }
                    else if (parsedData[3] == Description.INDEX)
                    {
                        Description field = new Description(parsedData[0], parsedData[1], false, true);

                        if(parsedData[2] == Description.DEFAULT_NULL)
                        {
                            field.setDefaultNull(true);
                            field.setNotNull(false);
                        }
                        else if(parsedData[2] == Description.NOT_NULL)
                        {
                            field.setDefaultNull(false);
                            field.setNotNull(true);
                        }
                        else
                        {
                            throw new Exception("Получено не правильное значение");
                        }

                        fields.Add(field);
                    }
                }
                else if(parsedData.Length == 3)
                {
                    if(parsedData[1] == Description.INTEGER_TYPE || parsedData[1] == Description.STRING_TYPE)
                    {
                        Description field = new Description(parsedData[0], parsedData[1]);

                        if (parsedData[2] == Description.DEFAULT_NULL)
                        {
                            field.setDefaultNull(true);
                            field.setNotNull(false);
                        }
                        else if (parsedData[2] == Description.NOT_NULL)
                        {
                            field.setDefaultNull(false);
                            field.setNotNull(true);
                        }
                        else
                        {
                            throw new Exception("Получено не правильное значение");
                        }

                        fields.Add(field);
                    }
                    else
                    {
                        throw new Exception("Получен не опознанный тип данных");
                    }
                }
            }

            return fields;
        }
    }
}
