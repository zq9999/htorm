using HT.Framework.Orm;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HT.DAL
{
   public static class MappingExtend
    {
        public static string GetMappColumnName(this PropertyInfo propertyInfo) {
            if (propertyInfo.IsDefined(typeof(HTColumnAttribute), true))
            { 
                HTColumnAttribute hTColumnAttribute= propertyInfo.GetCustomAttribute<HTColumnAttribute>(true);
                return   hTColumnAttribute.ColumnName;
            }
            else
            {
                return propertyInfo.Name;
            }
        }
    }
}
