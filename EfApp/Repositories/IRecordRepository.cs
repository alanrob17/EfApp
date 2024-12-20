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
        Task<IEnumerable<Record>> GetRecordsByYearAsync(int year);
        Task UpdateAsync(Record record);
        Task<int> GetTotalNumberOfCDsAsync();
        Task<int>? GetTotalNumberOfDiscsAsync();
        Task<int>? GetTotalNumberOfRecordsAsync();
        Task<int>? GetTotalNumberOfBluraysAsync();
        Task<int>? GetTotalNumberOfDVDsAsync();
        Task<int>? GetArtistNumberOfRecordsAsync(int artistId);
        Task<int>? GetTotalDiscsByYearAsync(int year);
        Task<int>? GetTotalDiscsByBoughtYearAsync(int year);
        Task<int>? GetNoRecordReviewCountAsync();
        Task<IEnumerable<Record>> GetAllNoReviewRecordsAsync();
    }
}
