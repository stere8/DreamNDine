using DreamNDine.BLL.Models;

public class PropertiesViewModel
{
    public int PropertyId { get; set; }
    public string PropertyName { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string DisplayPrice { get; set; }
    public int AvaialableRooms { get; set; }
    public decimal Price { get; set; }
    public string MainPic { get; set; }
    public List<string> PropertyPhotos { get; set; }
    public bool IsAvailable { get; set; }
    public User Owner { get; set; }
}