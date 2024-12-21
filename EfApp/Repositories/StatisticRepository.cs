using EfApp.Models;
using EfApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Repositories
{
    internal class StatisticRepository : IStatisticRepository
    {
        private readonly RecordService _recordService;
        private readonly AppDbContext _context;

        public StatisticRepository(AppDbContext context, RecordService recordService)
        {
            _context = context;
            _recordService = recordService;
        }

        public async Task<Statistic> GetStatistics()
        {
            Statistic statistics = new Statistic();

            statistics.RockDiscs = _context.Records.Where(r => r.Field == "Rock").Sum(r => r.Discs);
            statistics.FolkDiscs = _context.Records.Where(r => r.Field == "Folk").Sum(r => r.Discs);
            statistics.AcousticDiscs = _context.Records.Where(r => r.Field == "Acoustic").Sum(r => r.Discs);
            statistics.JazzDiscs = _context.Records.Where(r => r.Field == "Jazz" || r.Field == "Fusion").Sum(r => r.Discs);
            statistics.BluesDiscs = _context.Records.Where(r => r.Field == "Blues").Sum(r => r.Discs);
            statistics.CountryDiscs = _context.Records.Where(r => r.Field == "Country").Sum(r => r.Discs);
            statistics.ClassicalDiscs = _context.Records.Where(r => r.Field == "Classical").Sum(r => r.Discs);
            statistics.SoundtrackDiscs = _context.Records.Where(r => r.Field == "Soundtrack").Sum(r => r.Discs);
            statistics.FourStarDiscs = _context.Records.Where(r => r.Rating == "****").Count();
            statistics.ThreeStarDiscs = _context.Records.Where(r => r.Rating == "***").Count();
            statistics.TwoStarDiscs = _context.Records.Where(r => r.Rating == "**").Count();
            statistics.OneStarDiscs = _context.Records.Where(r => r.Rating == "*").Count();
            statistics.RecordCost = _context.Records.Where(r => r.Media == "R").Sum(r => (decimal?)r.Cost) ?? 0;
            statistics.TotalRecords = _context.Records.Where(r => r.Media == "R").Sum(r => r.Discs);
            statistics.CDCost = _context.Records.Where(r => r.Media == "CD").Sum(r => (decimal?)r.Cost) ?? 0;
            statistics.TotalCDs = _context.Records.Where(r => r.Media == "CD").Sum(r => r.Discs);
            statistics.AvCDCost = _context.Records.Where(r => r.Media == "CD").Average(r => (decimal?)r.Cost) ?? 0;
            statistics.TotalCost = _context.Records.Sum(r => (decimal?)r.Cost) ?? 0;
            statistics.Cost2022 = await _recordService.GetTotalCostByYearBoughtAsync(2022);
            statistics.Discs2022 = await _recordService.GetTotalDiscsByBoughtYearAsync(2022);
            statistics.Av2022 = GetAverageCostForYearBought(statistics.Cost2022, statistics.Discs2022);
            statistics.Cost2021 = await _recordService.GetTotalCostByYearBoughtAsync(2021);
            statistics.Discs2021 = await _recordService.GetTotalDiscsByBoughtYearAsync(2021);
            statistics.Av2021 = GetAverageCostForYearBought(statistics.Cost2021, statistics.Discs2021);
            statistics.Cost2020 = await _recordService.GetTotalCostByYearBoughtAsync(2020);
            statistics.Discs2020 = await _recordService.GetTotalDiscsByBoughtYearAsync(2020);
            statistics.Av2020 = GetAverageCostForYearBought(statistics.Cost2020, statistics.Discs2020);
            statistics.Cost2019 = await _recordService.GetTotalCostByYearBoughtAsync(2019);
            statistics.Discs2019 = await _recordService.GetTotalDiscsByBoughtYearAsync(2019);
            statistics.Av2019 = GetAverageCostForYearBought(statistics.Cost2019, statistics.Discs2019);
            statistics.Cost2018 = await _recordService.GetTotalCostByYearBoughtAsync(2018);
            statistics.Discs2018 = await _recordService.GetTotalDiscsByBoughtYearAsync(2018);
            statistics.Av2018 = GetAverageCostForYearBought(statistics.Cost2018, statistics.Discs2018);
            statistics.Cost2017 = await _recordService.GetTotalCostByYearBoughtAsync(2017);
            statistics.Discs2017 = await _recordService.GetTotalDiscsByBoughtYearAsync(2017);
            statistics.Av2017 = GetAverageCostForYearBought(statistics.Cost2017, statistics.Discs2017);

            return statistics;
        }

        private static decimal GetAverageCostForYearBought(decimal totalCost, int discs)
        {
            decimal averageCost = 0.00m;
            // this is to stop a divide by zero error if nothing has been bought
            if (totalCost > 1)
            {
                averageCost = totalCost / (decimal)discs;
                averageCost = Math.Round(averageCost, 2);
            }
            else
            {
                averageCost = 0.00m;
            }

            return averageCost;
        }
    }
}
