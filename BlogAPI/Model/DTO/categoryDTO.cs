namespace BlogAPI.Model.DTO
{
    public class CategoryDTO
    {
        public Guid category_id { get; set; }
        public string category_name { get; set; }
    }
    public class CategoryWithIdDTO
    {
        public string category_name { get; set; }
    }
}
