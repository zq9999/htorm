using HT.Framework.Orm;
using HT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HT.DAL
{
    public  class SqlBuilder<T> where T : BaseModel
    {
        static string findSql = string.Empty;
        static string insertSql = string.Empty;
        static string updateSql = string.Empty;
        static SqlBuilder() {
            var type = typeof(T);
            var tableName = type.Name;
            var properties = type.GetProperties();
            var colNames = string.Join(",", properties.Select(p => $"[{p.GetMappColumnName()}]"));
            findSql = $"select {colNames} from [{tableName}] where id=" ;

            var propertiesWithoutKey=properties.Where(p => !p.IsDefined(typeof(HTKeyAttribute),true ));
            var insertColNames= string.Join(",", propertiesWithoutKey.Select(p => $"[{p.GetMappColumnName()}]"));
            var paramsColNames = string.Join(",", propertiesWithoutKey.Select(p => $"@{p.GetMappColumnName()}"));
            insertSql = $"insert into {tableName} ({insertColNames}) values ({paramsColNames}) select @@identity";

            var uptColumns=string.Join(",", propertiesWithoutKey.Select(p => $"[{p.GetMappColumnName()}]=@{p.GetMappColumnName()}"));
            updateSql = $"update {tableName} set {uptColumns} where id=";

        }

        public static string FindSql {
            get
            {
                return  findSql;
            }
        }
        public static string InsertSql
        {
            get
            {
                return insertSql;
            }
        }
        public static string UpdateSql
        {
            get
            {
                return updateSql;
            }
        }
    }
}
