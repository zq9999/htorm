

using HT.Framework.Orm;

namespace HT.Model
{
    /// <summary>
    /// 数据库BaseModel
    /// </summary>
    public class BaseModel
    {
        [HTKey]
        public int Id { set; get; }
    }
}
