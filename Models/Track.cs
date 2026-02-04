namespace MusicStore.Models
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public int TrackNumber { get; set; }
        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}