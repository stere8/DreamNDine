public class PropertyUpdateRequest
{
    public string PropertyName { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public decimal? Price { get; set; } // Optional 
    public int? AvaialableRooms { get; set; }
    public List<string> PropertyPhotos { get; set; }
}