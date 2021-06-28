using System.Collections.Generic;

namespace API.DTOs
{
    public class MasterCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<SubCategoryDto> SubCategories { get; set; }
    }
}