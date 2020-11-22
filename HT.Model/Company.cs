using HT.Framework.Orm;
using System; 

namespace HT.Model
{
    public class Company  : BaseModel 
    {
        
        [HTColumn("Name")]
        public string CompanyName { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public Nullable<int> LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}