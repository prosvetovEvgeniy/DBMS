using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBMS.Core.OwnTypes;
using System.IO;
using System.Text.RegularExpressions;

namespace DBMS.Core.Parser
{
    class DataParser : BaseParser
    {
        public DataParser(string filePath) : base(filePath)
        {

        }

        public List<Line> getTableData()
        {
            List<Line> tableData = new List<Line>();
            string data = File.ReadAllText(this.filePath + DATA_FILE_NAME, Encoding.Default);
            string pattern = @"({\r\n)|(\r\n})";

            List<string> lines = Regex.Split(data, pattern).ToList();

            getLines(ref lines);
            
            for(int i = 0; i < lines.Count; i++)
            {
                tableData.Add(new Line());

                string[] fields = lines[i].Split(new string[] { "\r\n" }, StringSplitOptions.None);
                for(int j = 0; j < fields.Length; j++)
                {
                    string[] keyValue = fields[j].Split(':');
                    tableData[i].addField(keyValue[0]);
                    tableData[i].addContent(keyValue[1]);
                }
            }

            return tableData;
        }

        private void getLines(ref List<string> lines)
        {
            int i = 0;
            while (i < lines.Count)
            {
                if (lines[i] == "" || lines[i] == "{\r\n" || lines[i] == "\r\n}" || lines[i] == "\r\n")
                {
                    lines.RemoveAt(i);
                    i = 0;
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
