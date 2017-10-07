using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMS.Core.OwnTypes
{
    class Description
    {
        public const string INTEGER_TYPE = "integer";
        public const string STRING_TYPE = "string";
        public const string PK = "PRIMARY_KEY";
        public const string INDEX = "INDEX";
        public const string NOT_NULL = "NOT_NULL";
        public const string DEFAULT_NULL = "DEFAULT_NULL";

        private string fieldName;
        private string fieldType;
        private bool primaryKey;
        private bool index;
        private bool defaultNull;
        private bool notNull;

        public Description(string fieldName, string fieldType, bool primaryKey = false, bool index = false)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.primaryKey = primaryKey;
            this.index = index;
        }

        public void setDefaultNull(bool defaultNull)
        {
            this.defaultNull = defaultNull;
        }

        public void setNotNull(bool notNull)
        {
            this.notNull = notNull;
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

        public bool Index
        {
            get { return index; }
        }

        public bool DefaultNULL
        {
            get { return defaultNull; }
        }

        public bool NotNULL
        {
            get { return notNull; }
        }
    }
}
