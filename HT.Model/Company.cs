using System; 

namespace HT.Model
{
    
    public class CompanyModel : BaseModel
    {
        
        public string CompanyName { get; set; }

        public DateTime CreateTime { get; set; }

        public int CreatorId { get; set; }

        public Nullable<int> LastModifierId { get; set; }

        public DateTime? LastModifyTime { get; set; }
    }
}