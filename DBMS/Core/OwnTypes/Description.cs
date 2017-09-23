using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Description
    {
        private string fieldName;
        private string fieldType;
        private bool nullState;

        public Description(string fieldName, string fieldType, bool nullState = true)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.nullState = nullState;
        }

        public string getFieldName()
        {
            return fieldName;
        }

        public string getFieldType()
        {
            return fieldType;
        }

        public bool getNullState()
        {
            return nullState;
        }
    }
}
