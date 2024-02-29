using System.ComponentModel.DataAnnotations;
using DreamNDine.BLL.Models;

public class Conversation
{
    [Key]
    public int ConversationID { get; set; }

    public int Participant1ID { get; set; }
    public User Participant1 { get; set; }

    public int Participant2ID { get; set; }
    public User Participant2 { get; set; }

    public ICollection<Message> Messages { get; set; }
}
