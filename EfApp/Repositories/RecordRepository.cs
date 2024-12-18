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

        public async Task<IEnumerable<Record>> GetAllRecordsAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<IEnumerable<Record>>? GetArtistRecordsAsync(int artistId)
        {
            return await _context.Records.Where(r => r.ArtistId == artistId).OrderByDescending(r => r.Recorded).ToListAsync();
        }

        public async Task<Record?> GetRecordByIdAsync(int recordId)
        {
            return await _context.Records
                .Include(r => r.ArtistAsset)
                .FirstOrDefaultAsync(r => r.RecordId == recordId);
        }

        //public async Task AddRecordAsync(Record record)
        //{
        //    _context.Records.Add(record);
        //    await _context.SaveChangesAsync();
        //}
    }
}
