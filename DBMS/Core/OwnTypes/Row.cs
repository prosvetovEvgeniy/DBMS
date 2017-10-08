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

        //adders
        public void addField(string field)
        {
            fields.Add(field);
        }

        public void addContent(string content)
        {
            fieldsContent.Add(content);
        }

        //removers
        public void removeField(string fieldName)
        {
            int index = 0;

            for (int i = 0; i < fields.Count; i++) {
                if(fields[i] == fieldName)
                {
                    index = i;
                    break;
                }
            }

            fields.Remove(fieldName);
            fieldsContent.RemoveAt(index);
        }

        //setters
        public void setFields(List<string> fields)
        {
            this.fields = fields;
        }
        
        public void setContent(List<string> fieldsContent)
        {
            this.fieldsContent = fieldsContent;
        }

        //getters
        public string getFieldByIndex(int index)
        {
            return fields[index];
        }

        public string getFieldContentByIndex(int index)
        {
            return fieldsContent[index];
        }

        public string getContentByFieldName(string name)
        {
            for(int i = 0; i < fields.Count; i++)
            {
                if(fields[i] == name)
                {
                    return fieldsContent[i];
                }
            }

            throw new Exception("Столбца с таким именем не существует");
        }

        public int CountFields
        {
            get { return fields.Count; }
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
