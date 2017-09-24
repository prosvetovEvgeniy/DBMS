using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Row
    {
        private List<string> fields;
        private List<string> fieldsContent;

        public Row()
        {
            fields = new List<string>();
            fieldsContent = new List<string>();
        }

        public void addField(string field)
        {
            fields.Add(field);
        }

        public void addContent(string content)
        {
            fieldsContent.Add(content);
        }

        public void setFields(List<string> fields)
        {
            this.fields = fields;
        }
        
        public void setContent(List<string> fieldsContent)
        {
            this.fieldsContent = fieldsContent;
        }
       
        public List<string> getContent()
        {
            return this.fieldsContent;
        }

        public List<string> getFields()
        {
            return this.fields;
        }

    }
}
