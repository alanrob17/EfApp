using EfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Repositories
{
    internal interface IStatisticRepository
    {
        Task<Statistic> GetStatistics();
    }
}
