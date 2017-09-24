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

        public string FieldName {
            get { return fieldName; }
        }

        public string FieldType
        {
            get { return fieldType; }
        }

        public bool FieldNullState
        {
            get { return nullState; }
        }
    }
}
