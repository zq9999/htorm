using HT.Framework;
using HT.Framework.Orm;
using HT.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HT.DAL
{
    public class SqlHelper
    {
        public T Find<T>(int id)   where T : BaseModel
        { 
            var type = typeof(T);
            var properties= type.GetProperties();
            string findSql = SqlBuilder<T>.FindSql+id;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigrationManager.ConnectionString))
            {
                using (SqlCommand sqlcommand = new SqlCommand(findSql, sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader sdr = sqlcommand.ExecuteReader();
                    if (sdr.Read())
                    {
                        T t = (T)Activator.CreateInstance(type);
                        foreach (var property in properties)
                        {
                            var columnName= property.GetMappColumnName();
                            property.SetValue(t, sdr[columnName] is DBNull ? null : sdr[columnName]);
                        }
                        return t;
                    }
                    return default(T);
                }
            } 
        }

        public bool Insert<T>(T t) where T : BaseModel
        {
            var type = typeof(T);
            var properties = type.GetProperties().Where(p=>!p.IsDefined(typeof(HTKeyAttribute),true));
            string sql = SqlBuilder<T>.InsertSql;
            var parameters= properties
                .Select(p => new SqlParameter(p.GetMappColumnName(), p.GetValue(t) ?? DBNull.Value)).ToArray();
            
            using (SqlConnection sqlConnection=new SqlConnection(ConfigrationManager.ConnectionString))
            {
                using (SqlCommand sqlCommand=new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parameters);
                    sqlConnection.Open();
                    var resultObj= sqlCommand.ExecuteScalar();
                    var id= int.TryParse(resultObj?.ToString(), out int result)? result:-1;
                    if (id == -1)
                    {
                        t = default(T);
                        return false;
                    }
                    else {
                        t.Id = id;
                        return true;
                    }
                }
            }

        }

        public bool Update<T>(T t) where T:BaseModel,new()
        {
            var type=t.GetType();
            var properties = type.GetProperties().Where(p => !p.IsDefined(typeof(HTKeyAttribute), true));
            string sql = SqlBuilder<T>.UpdateSql + t.Id;
            var parameters = properties
                .Select(p => new SqlParameter(p.GetMappColumnName(), p.GetValue(t) ?? DBNull.Value)).ToArray();
             
            using (SqlConnection sqlConnection = new SqlConnection(ConfigrationManager.ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(parameters);
                    sqlConnection.Open();
                    var offectRows = sqlCommand.ExecuteNonQuery();
                    return offectRows > 0; 
                }
            }
        }


        //public bool Update<T>(string json, int id) where T : BaseModel, new()
        //{
        //    var t = typeof(T);
        //    var model=JsonSerializer.Deserialize(json, t);
        //    var properties=t.GetProperties().Where(p => json.Contains($"{p.GetMappColumnName()}:")
        //    || json.Contains($"'{p.GetMappColumnName()}':")
        //    || json.Contains($"\"{p.GetMappColumnName()}\":"));

        //    var uptColumns =string.Join(",", properties.Select(p => $"{p.GetMappColumnName()}=@{p.GetMappColumnName()}"));
        //    string sql = $"update {t.Name} set {uptColumns}  where id=" + id;
        //    var parameters = properties
        //        .Select(p => new SqlParameter(p.GetMappColumnName(), p.GetValue(t) ?? DBNull.Value)).ToArray();

        //    using (SqlConnection sqlConnection = new SqlConnection(ConfigrationManager.ConnectionString))
        //    {
        //        using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
        //        {
        //            sqlCommand.Parameters.AddRange(parameters);
        //            sqlConnection.Open();
        //            var offectRows = sqlCommand.ExecuteNonQuery();
        //            return offectRows > 0;
        //        }
        //    }
        //}
    }
}
