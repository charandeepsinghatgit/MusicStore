namespace MusicStore.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string? Biography { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}