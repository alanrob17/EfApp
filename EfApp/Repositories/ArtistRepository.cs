using System.Collections.Generic;
using System.Linq;
using EfApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EfApp.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddArtistAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _context.Artists.  ToListAsync();
        }

        public async Task UpdateAsync(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Artist artist)
        {
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
        }

        // public async Task<Artist?> GetByIdAsync(int artistId) => await _context.Artists.FindAsync(artistId);
        public async Task<Artist?> GetByArtistIdAsync(int artistId)
        {
            return await _context.Artists
                .Include(a => a.Records)
                .FirstOrDefaultAsync(a => a.ArtistId == artistId);
        }

        public async Task<Artist?> GetByNameAsync(string name) => await _context.Artists.FirstOrDefaultAsync(a => a.Name == name);

        public async Task<int> GetArtistIdAsync(string firstName, string lastName)
        {
            var artist = await _context.Artists.FirstOrDefaultAsync(a => a.FirstName == firstName && a.LastName == lastName);

            return artist?.ArtistId ?? 0;
        }

        public async Task<string> GetBiographyByIdAsync(int artistId)
        {
            Artist? artist = await _context.Artists.FindAsync(artistId);

            return artist?.Biography ?? string.Empty;
        }

        public async Task<IEnumerable<string?>> GetArtistsWithNoBioAsync()
        {
            return await _context.Artists
                .Where(a => string.IsNullOrEmpty(a.Biography))
                .OrderBy(a => a.LastName)
                .Select(a => a.Name)
                .ToListAsync();
        }

        public async Task<int> GetNoBiographyTotal()
        {
            return await _context.Artists
                .CountAsync(a => string.IsNullOrEmpty(a.Biography));
        }
    }
}