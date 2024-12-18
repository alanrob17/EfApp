using EfApp.Models;

namespace EfApp.Repositories
{
    public interface IArtistRepository
    {
        Task AddArtistAsync(Artist artist);
        Task<Artist> GetByArtistIdAsync(int artistId);
        Task<Artist> GetByNameAsync(string Name);
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
        Task UpdateAsync(Artist artist);
        Task DeleteAsync(Artist artist);
        Task<int> GetArtistIdAsync(string firstName, string lastName);
        Task<int> GetNoBiographyTotal();
        Task<string> GetBiographyByIdAsync(int artistId);
        Task<IEnumerable<string>> GetArtistsWithNoBioAsync();
    }
}