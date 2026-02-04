using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        // GET: StoreController
        private readonly MusicStoreContext _context;

        public StoreController(MusicStoreContext context)
        {
            _context = context;
        }

        public IActionResult Browse(string? artist, string? genre, string? search)
        {
            var albums = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .AsQueryable();

            if (!string.IsNullOrEmpty(artist))
            {
                albums = albums.Where(a => a.Artist.Name == artist);
            }

            if (!string.IsNullOrEmpty(genre))
            {
                albums = albums.Where(a => a.Genre.Name == genre);
            }

            if (!string.IsNullOrEmpty(search))
            {
                albums = albums.Where(a => a.Title.Contains(search));
            }

            ViewBag.Title = "All Albums";

            if (!string.IsNullOrEmpty(artist) && string.IsNullOrEmpty(genre))
            {
                ViewBag.Title = $"Albums by {artist}";
            }

            if (!string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(artist))
            {
                ViewBag.Title = $"{genre} Albums";
            }

            if (!string.IsNullOrEmpty(artist) && !string.IsNullOrEmpty(genre))
            {
                ViewBag.Title = $"Albums by {artist}, {genre} Albums";
            }

            ViewBag.Artists = _context.Artists.ToList();
            ViewBag.Genres = _context.Genres.ToList();

            return View(albums.ToList());
        }
        public ActionResult Details(int id)
        {
            var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.Tracks)
                .FirstOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

    }
}
