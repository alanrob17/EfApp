using EfApp.Models;
using EfApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Repositories
{
    public class StatisticRepository : IStatisticRepository
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
            var statistics = new Statistic
            {
                RockDiscs = await GetTotalDiscsByFieldAsync("Rock"),
                FolkDiscs = await GetTotalDiscsByFieldAsync("Folk"),
                AcousticDiscs = await GetTotalDiscsByFieldAsync("Acoustic"),
                JazzDiscs = await GetTotalDiscsByFieldsAsync(new[] { "Jazz", "Fusion" }),
                BluesDiscs = await GetTotalDiscsByFieldAsync("Blues"),
                CountryDiscs = await GetTotalDiscsByFieldAsync("Country"),
                ClassicalDiscs = await GetTotalDiscsByFieldAsync("Classical"),
                SoundtrackDiscs = await GetTotalDiscsByFieldAsync("Soundtrack"),
                FourStarDiscs = await GetCountByRatingAsync("****"),
                ThreeStarDiscs = await GetCountByRatingAsync("***"),
                TwoStarDiscs = await GetCountByRatingAsync("**"),
                OneStarDiscs = await GetCountByRatingAsync("*"),
                RecordCost = await GetTotalCostByMediaAsync("R"),
                TotalRecords = await GetTotalDiscsByMediaAsync("R"),
                CDCost = await GetTotalCostByMediaAsync("CD"),
                TotalCDs = await GetTotalDiscsByMediaAsync("CD"),
                AvCDCost = await GetAverageCostByMediaAsync("CD"),
                TotalCost = (await _context.Records.SumAsync(r => (decimal?)r.Cost)) ?? 0
            };

            await SetYearlyStatistics(statistics, 2017, 2022);

            return statistics;
        }

        private async Task<int> GetTotalDiscsByFieldAsync(string field)
        {
            return await _context.Records.Where(r => r.Field == field).SumAsync(r => r.Discs);
        }

        private async Task<int> GetTotalDiscsByFieldsAsync(string[] fields)
        {
            return await _context.Records.Where(r => fields.Contains(r.Field)).SumAsync(r => r.Discs);
        }

        private async Task<int> GetCountByRatingAsync(string rating)
        {
            return await _context.Records.CountAsync(r => r.Rating == rating);
        }

        private async Task<decimal> GetTotalCostByMediaAsync(string media)
        {
            return (await _context.Records.Where(r => r.Media == media).SumAsync(r => (decimal?)r.Cost)) ?? 0;
        }

        private async Task<int> GetTotalDiscsByMediaAsync(string media)
        {
            return await _context.Records.Where(r => r.Media == media).SumAsync(r => r.Discs);
        }

        private async Task<decimal> GetAverageCostByMediaAsync(string media)
        {
            return (await _context.Records.Where(r => r.Media == media).AverageAsync(r => (decimal?)r.Cost)) ?? 0;
        }

        private async Task SetYearlyStatistics(Statistic statistics, int startYear, int endYear)
        {
            for (int year = startYear; year <= endYear; year++)
            {
                var cost = await _recordService.GetTotalCostByYearBoughtAsync(year);
                var discsCount = await _recordService.GetTotalDiscsByYearBoughtAsync(year);
                var averageCost = GetAverageCostForYearBought(cost, discsCount);

                switch (year)
                {
                    case 2022:
                        statistics.Cost2022 = cost;
                        statistics.Discs2022 = discsCount;
                        statistics.Av2022 = averageCost;
                        break;
                    case 2021:
                        statistics.Cost2021 = cost;
                        statistics.Discs2021 = discsCount;
                        statistics.Av2021 = averageCost;
                        break;
                    case 2020:
                        statistics.Cost2020 = cost;
                        statistics.Discs2020 = discsCount;
                        statistics.Av2020 = averageCost;
                        break;
                    case 2019:
                        statistics.Cost2019 = cost;
                        statistics.Discs2019 = discsCount;
                        statistics.Av2019 = averageCost;
                        break;
                    case 2018:
                        statistics.Cost2018 = cost;
                        statistics.Discs2018 = discsCount;
                        statistics.Av2018 = averageCost;
                        break;
                    case 2017:
                        statistics.Cost2017 = cost;
                        statistics.Discs2017 = discsCount;
                        statistics.Av2017 = averageCost;
                        break;
                }
            }
        }

        private static decimal GetAverageCostForYearBought(decimal totalCost, int discs)
        {
            if (discs == 0) return 0.00m; // Avoid divide by zero
            decimal averageCost = totalCost / discs;
            return Math.Round(averageCost, 2);
        }
    }
}
