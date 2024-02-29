using System.ComponentModel.DataAnnotations;
using DreamNDine.BLL.Models;

public class Message
{
    [Key]
    public int MessageID { get; set; }

    public int ConversationID { get; set; }
    public Conversation Conversation { get; set; }

    public int SenderID { get; set; }
    public User Sender { get; set; }

    public string MessageText { get; set; }

    public DateTime Timestamp { get; set; }
}