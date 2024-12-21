using EfApp.Models;
using EfApp.Repositories;
using EfApp.Utilities;

namespace EfApp.Services
{
    internal class StatisticService
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticService(IStatisticRepository statisticRepository, AppLogger appLogger)
        {
            _statisticRepository = statisticRepository;
        }
        public async Task<Statistic> GetStatisticsAsync()
        {
            return await _statisticRepository.GetStatistics();
        }
    }
}