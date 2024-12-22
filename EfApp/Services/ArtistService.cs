using EfApp.Models;
using EfApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Services
{
    public class ArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task AddArtistAsync(Artist artist)
        {
            await Task.Run(() => _artistRepository.AddArtistAsync(artist));
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await Task.Run(() => _artistRepository.GetAllArtistsAsync());
        }

        public async Task<Artist> GetArtistByIdAsync(int artistId)
        {
            return await Task.Run(() => _artistRepository.GetByArtistIdAsync(artistId));
        }

        public async Task<Artist> GetArtistByNameAsync(string name)
        {
            return await Task.Run(() => _artistRepository.GetByNameAsync(name));
        }

        public async Task UpdateArtistAsync(Artist artist)
        {
            await Task.Run(() => _artistRepository.UpdateAsync(artist));
        }

        public async Task DeleteArtistAsync(Artist artist)
        {
            await Task.Run(() => _artistRepository.DeleteAsync(artist));
        }

        public async Task<int> GetArtistIdAsync(string firstName, string lastName)
        {
            return await Task.Run(() => _artistRepository.GetArtistIdAsync(firstName, lastName));
        }

        public async Task<string> GetBiographyByIdAsync(int artistId)
        {
            return await Task.Run(() => _artistRepository.GetBiographyByIdAsync(artistId));
        }

        public async Task<IEnumerable<string>> GetArtistsWithNoBioAsync()
        {
            return await Task.Run(() => _artistRepository.GetArtistsWithNoBioAsync());
        }

        public async Task<int> GetNoBiographyTotal()
        {
            return await Task.Run(() => _artistRepository.GetNoBiographyTotal());
        }
    }
}
