using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int BusinessCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


        [NotMapped]
        public virtual int TotalCount { get; set; }
    }
}
