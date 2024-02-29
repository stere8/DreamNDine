using System.ComponentModel.DataAnnotations;

namespace DreamNDine.BLL.Models
{
    public class SystemLog
    {
        [Key]
        public int LogID { get; set; }

        [Required]
        public string LogType { get; set; } // Could be an enum for different log types (e.g., Error, Warning, Information)

        public string LogDetails { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}