namespace Dumptruck_v4.Models {
    public class Note {
        public int Id { get; set; }
        public string NoteContent { get; set; } = String.Empty;
        public string? UserId { get; set; } = String.Empty;
        public DateTime? TimeStamp { get; set; }
        public int? Score { get; set; } = 0;
    }
    
}
