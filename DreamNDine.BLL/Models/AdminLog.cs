using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class AdminLog
    {
        [Key]
        public int LogID { get; set; }

        public int AdminID { get; set; }
        public User Admin { get; set; } // Navigation properties

        public string Action { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}