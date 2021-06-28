using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("MasterCategories")]
    public class MasterCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}