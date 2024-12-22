using EfApp.Models;
using EfApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Services
{
    public class RecordService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<IEnumerable<Record>> GetAllRecordsAsync()
        {
            return await Task.Run(() => _recordRepository.GetAllRecordsAsync());
        }

        public async Task<Record> GetRecordByIdAsync(int recordId)
        {
            return await Task.Run(() => _recordRepository.GetRecordByIdAsync(recordId));
        }

        public async Task AddRecordAsync(Record record)
        {
            await Task.Run(() => _recordRepository.AddRecordAsync(record));
        }

        public async Task<IEnumerable<Record>> GetArtistRecordsAsync(int artistId)
        {
            return await Task.Run(() => _recordRepository.GetArtistRecordsAsync(artistId));
        }

        public async Task UpdateRecordAsync(Record record)
        {
            await Task.Run(() => _recordRepository.UpdateAsync(record));
        }

        public async Task DeleteRecordAsync(Record record)
        {
            await Task.Run(() => _recordRepository.DeleteAsync(record));
        }

        public async Task<IEnumerable<Record>> GetRecordByNameAsync(string name)
        {
            return await Task.Run(() => _recordRepository.GetRecordByNameAsync(name));
        }

        public async Task<IEnumerable<Record>> GetRecordsByYearAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetRecordsByYearAsync(year));
        }

        public async Task<int> GetTotalNumberOfCDsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfCDsAsync());
        }

        public async Task<int> GetTotalNumberOfDiscsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfDiscsAsync());
        }

        public async Task<int> GetTotalNumberOfRecordsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfRecordsAsync());
        }

        public async Task<int> GetTotalNumberOfBluraysAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfBluraysAsync());
        }

        public async Task<int> GetTotalNumberOfDVDsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfDVDsAsync());
        }

        public async Task<int> GetArtistNumberOfRecordsAsync(int artistId)
        {
            return await Task.Run(() => _recordRepository.GetArtistNumberOfRecordsAsync(artistId));
        }

        public async Task<int> GetTotalDiscsByYearAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetTotalDiscsByYearAsync(year));
        }

        public async Task<int> GetTotalDiscsByYearBoughtAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetTotalDiscsByYearBoughtAsync(year));
        }

        public async Task<int> GetNoRecordReviewCountAsync()
        {
            return await Task.Run(() => _recordRepository.GetNoRecordReviewCountAsync());
        }

        public async Task<IEnumerable<Record>> GetAllNoReviewRecordsAsync()
        {
            return await Task.Run(() => _recordRepository.GetAllNoReviewRecordsAsync());
        }

        public async Task<decimal> GetTotalCostByYearBoughtAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetTotalCostByYearBoughtAsync(year));
        }
    }
}
