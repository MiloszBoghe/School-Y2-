using System;
using System.ComponentModel.DataAnnotations;

namespace Stage_API.Domain.Classes
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Stagevoorstel Stagevoorstel { get; set; }
        [Required]
        public int StagevoorstelId { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
