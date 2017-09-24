using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Data
    {
        private List<string> fields;
        private List<string> fieldContent;

        public Data()
        {
            fields = new List<string>();
            fieldContent = new List<string>();
        }

        public void addField(string field)
        {
            fields.Add(field);
        }
        
        public void addContent(string content)
        {
            fieldContent.Add(content);
        }

        public List<string> getContent()
        {
            return this.fieldContent;
        }

        public List<string> getFields()
        {
            return this.fields;
        }

    }
}
