using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Framework.Orm
{
    [AttributeUsage(AttributeTargets.Property)]
    public   class HTColumnAttribute: Attribute
    {
        string _column;
        public HTColumnAttribute(string  column) {
            _column = column;
        }

        public string ColumnName {
            get {
                return _column;
            }
        }
    }
}
