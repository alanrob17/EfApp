using EfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Repositories
{
    public interface IRecordRepository
    {
        Task AddRecordAsync(Record record);
        void DeleteAsync(Record record);
        Task<IEnumerable<Record>> GetAllRecordsAsync();
        Task<IEnumerable<Record>>? GetArtistRecordsAsync(int artistId);
        Task<Record>? GetRecordByIdAsync(int recordId);
        Task<IEnumerable<Record>>? GetRecordByNameAsync(string name);
        Task UpdateAsync(Record record);
    }
}
