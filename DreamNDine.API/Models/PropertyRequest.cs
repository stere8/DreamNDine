using System.ComponentModel.DataAnnotations;

public class PropertyRequest
{
    [Required]
    public string PropertyName { get; set; }

    public string Description { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int AvaialableRooms { get; set; }

    public List<string> PropertyPhotos { get; set; } = new List<string>(); // Assuming URLs
}