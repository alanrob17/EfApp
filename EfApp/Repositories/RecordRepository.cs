using EfApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly AppDbContext _context;

        public RecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRecordAsync(Record record)
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(Record record)
        {
            if (record != null)
            {
                _context.Records.Remove(record);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Record>> GetAllRecordsAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<IEnumerable<Record>>? GetArtistRecordsAsync(int artistId)
        {
            return await _context.Records.Where(r => r.ArtistId == artistId)
                .OrderByDescending(r => r.Recorded)
                .ToListAsync();
        }

        public async Task<Record?> GetRecordByIdAsync(int recordId)
        {
            return await _context.Records
                .FirstOrDefaultAsync(r => r.RecordId == recordId);
        }

        public async Task<IEnumerable<Record>> GetRecordByNameAsync(string name)
        {
            return await _context.Records
                .Where(r => EF.Functions.Like(r.Name.ToLower(), $"%{name.ToLower()}%"))
                .OrderBy(r => r.RecordId).ToListAsync();
        }

        public async Task UpdateAsync(Record record)
        {
            _context.Records.Update(record);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Record>> GetRecordsByYearAsync(int year)
        {
            return await _context.Records
                .Where(r => r.Recorded == year)
                .ToListAsync();
        }
        public async Task<int> GetTotalNumberOfCDsAsync()
        {
            try
            {
                return await _context.Records
                    .Where(r => r.Media == "CD")
                    .SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetTotalNumberOfDiscsAsync()
        {
            try
            {
                return await _context.Records.SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetTotalNumberOfRecordsAsync()
        {
            try
            {
                return await _context.Records
                    .Where(r => r.Media == "R")
                    .SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetTotalNumberOfBluraysAsync()
        {
            try
            {
                return await _context.Records
                    .Where(r => r.Media.Contains("Blu-ray"))
                    .SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetTotalNumberOfDVDsAsync()
        {
            try
            {
                return await _context.Records
                    .Where(r => r.Media.Contains("DVD"))
                    .SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetArtistNumberOfRecordsAsync(int artistId)
        {
            return await _context.Records
                .Where(r => r.ArtistId == artistId)
                .SumAsync(r => r.Discs);
        }
        public async Task<int> GetTotalDiscsByYearAsync(int year)
        {
            try
            {
                return await _context.Records
                    .Where(r => r.Recorded == year)
                    .SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetTotalDiscsByYearBoughtAsync(int year)
        {
            try
            {
                var previousYearString = $"{year - 1}-12-31";
                DateTime previousDate = DateTime.Parse(previousYearString);
                var endYearString = $"{year + 1}-01-01";
                DateTime endDate = DateTime.Parse(endYearString);
                return await _context.Records
                    .Where(r => r.Bought > previousDate && r.Bought < endDate)
                    .SumAsync(r => r.Discs);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public async Task<int> GetNoRecordReviewCountAsync()
        {
            try
            {
                return await _context.Records
                    .Where(r => string.IsNullOrEmpty(r.Review))
                    .CountAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Record>> GetAllNoReviewRecordsAsync()
        {
            return await _context.Records
                .Where(r => string.IsNullOrEmpty(r.Review))
                .ToListAsync();
        }

        public async Task<decimal> GetTotalCostByYearBoughtAsync(int year)
        {
            try
            {
                var previousYearString = $"{year - 1}-12-31";
                DateTime previousDate = DateTime.Parse(previousYearString);
                var endYearString = $"{year + 1}-01-01";
                DateTime endDate = DateTime.Parse(endYearString);

                return (decimal)await _context.Records
                    .Where(r => r.Bought > previousDate && r.Bought < endDate)
                    .SumAsync(r => r.Cost);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}


/*
 select sum(cost) from record where bought > '31-Dec-2021' and bought < '01-Jan-2023'
  var previousYearString = $"{year - 1}-12-31";
                DateTime previousDate = DateTime.Parse(previousYearString);
                var endYearString = $"{year + 1}-01-01";
                DateTime endDate = DateTime.Parse(endYearString);
                return await _context.Records
                    .Where(r => r.Bought > previousDate && r.Bought < endDate)
                    .SumAsync(r => r.Discs);
 */