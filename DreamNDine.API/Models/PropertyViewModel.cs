using DreamNDine.BLL.Models;

public class PropertyViewModel
{
    public int PropertyId { get; set; }
    public string PropertyName { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string DisplayPrice { get; set; }
    public int AvaialableRooms { get; set; }
    public List<string> PropertyPhotos { get; set; }
    public bool IsAvailable { get; set; }
    public User Owner { get; set; }
}