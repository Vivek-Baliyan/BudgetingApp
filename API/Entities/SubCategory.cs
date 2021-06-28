using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("SubCategories")]
    public class SubCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public MasterCategory MasterCategory { get; set; }
        public int MasterCategoryId { get; set; }
    }
}