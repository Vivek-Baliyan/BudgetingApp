namespace API.DTOs
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int MasterCategoryId { get; set; }
    }
}