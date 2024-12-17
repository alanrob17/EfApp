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
        // Task AddRecordAsync(Record record);
        Task<IEnumerable<Record>> GetAllRecordsAsync();
        Task<Record>? GetRecordByIdAsync(int recordId);
    }
}
