using EfApp.Models;
using EfApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Services
{
    internal class RecordService
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

        internal async Task AddRecordAsync(Record record)
        {
            await Task.Run(() => _recordRepository.AddRecordAsync(record));
        }

        internal async Task<IEnumerable<Record>> GetArtistRecordsAsync(int artistId)
        {
            return await Task.Run(() => _recordRepository.GetArtistRecordsAsync(artistId));
        }

        public async Task UpdateRecordAsync(Record record)
        {
            await Task.Run(() => _recordRepository.UpdateAsync(record));
        }

        internal async Task DeleteRecordAsync(Record record)
        {
            await Task.Run(() => _recordRepository.DeleteAsync(record));
        }

        internal async Task<IEnumerable<Record>> GetRecordByNameAsync(string name)
        {
            return await Task.Run(() => _recordRepository.GetRecordByNameAsync(name));
        }

        internal async Task<IEnumerable<Record>> GetRecordsByYearAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetRecordsByYearAsync(year));
        }

        internal async Task<int> GetTotalNumberOfCDsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfCDsAsync());
        }

        internal async Task<int> GetTotalNumberOfDiscsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfDiscsAsync());
        }

        internal async Task<int> GetTotalNumberOfRecordsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfRecordsAsync());
        }

        internal async Task<int> GetTotalNumberOfBluraysAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfBluraysAsync());
        }

        internal async Task<int> GetTotalNumberOfDVDsAsync()
        {
            return await Task.Run(() => _recordRepository.GetTotalNumberOfDVDsAsync());
        }

        internal async Task<int> GetArtistNumberOfRecordsAsync(int artistId)
        {
            return await Task.Run(() => _recordRepository.GetArtistNumberOfRecordsAsync(artistId));
        }

        internal async Task<int> GetTotalDiscsByYearAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetTotalDiscsByYearAsync(year));
        }

        internal async Task<int> GetTotalDiscsByBoughtYearAsync(int year)
        {
            return await Task.Run(() => _recordRepository.GetTotalDiscsByBoughtYearAsync(year));
        }

        internal async Task<int> GetNoRecordReviewCountAsync()
        {
            return await Task.Run(() => _recordRepository.GetNoRecordReviewCountAsync());
        }

        internal async Task<IEnumerable<Record>> GetAllNoReviewRecordsAsync()
        {
            return await Task.Run(() => _recordRepository.GetAllNoReviewRecordsAsync());
        }
    }
}
