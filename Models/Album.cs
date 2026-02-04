namespace MusicStore.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? AlbumArtUrl { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rating { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}