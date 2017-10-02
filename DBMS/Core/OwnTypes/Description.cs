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
        private bool primaryKey;
        //private bool nullState;

        public Description(string fieldName, string fieldType, bool primaryKey = false)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.primaryKey = primaryKey;
            //this.nullState = nullState;
        }

        public string FieldName {
            get { return fieldName; }
        }

        public string FieldType
        {
            get { return fieldType; }
        }

        public bool PrimaryKey
        {
            get { return primaryKey; }
        }
        /*public bool FieldNullState
        {
            get { return nullState; }
        }*/
    }
}
