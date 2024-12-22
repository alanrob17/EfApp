using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Tests
{
    public class StatisticTest
    {
        private readonly StatisticService _statisticService;
        private readonly AppLogger _appLogger;

        public StatisticTest(StatisticService statisticService, AppLogger appLogger)
        {
            _statisticService = statisticService;
            _appLogger = appLogger;
        }

        public async Task RunTestsAsync()
        {
            // Get All Statistics
            Statistic statistics = await _statisticService.GetStatisticsAsync();

            var dtnow = DateTime.Now;
            var date = dtnow.ToLongDateString();

            _appLogger.LogInformation($"Record Database Statistics for {date}\n\n");

            _appLogger.LogInformation($"Number of Discs bought in 2022: {statistics.Discs2022}");
            _appLogger.LogInformation($"Total Amount spent on Discs in 2022: {statistics.Cost2022.ToString("C")}");
            _appLogger.LogInformation($"Average cost of a Disc in 2022: {statistics.Av2022.ToString("C")}");

            _appLogger.LogInformation($"Number of Discs bought in 2021: {statistics.Discs2021}");
            _appLogger.LogInformation($"Total Amount spent on Discs in 2021: {statistics.Cost2021.ToString("C")}");
            _appLogger.LogInformation($"Average cost of a Disc in 2021: {statistics.Av2021.ToString("C")}");

            _appLogger.LogInformation($"Number of Discs bought in 2020: {statistics.Discs2020}");
            _appLogger.LogInformation($"Total Amount spent on Discs in 2020: {statistics.Cost2020.ToString("C")}");
            _appLogger.LogInformation($"Average cost of a Disc in 2020: {statistics.Av2020.ToString("C")}");

            _appLogger.LogInformation($"Number of Discs bought in 2019: {statistics.Discs2019}");
            _appLogger.LogInformation($"Total Amount spent on Discs in 2019: {statistics.Cost2019.ToString("C")}");
            _appLogger.LogInformation($"Average cost of a Disc in 2019: {statistics.Av2019.ToString("C")}");

            _appLogger.LogInformation($"Number of Discs bought in 2018: {statistics.Discs2022}");
            _appLogger.LogInformation($"Total Amount spent on Discs in 2018: {statistics.Cost2018.ToString("C")}");
            _appLogger.LogInformation($"Average cost of a Disc in 2018: {statistics.Av2018.ToString("C")}");

            _appLogger.LogInformation($"Number of Discs bought in 2017: {statistics.Discs2017}");
            _appLogger.LogInformation($"Total Amount spent on Discs in 2017: {statistics.Cost2017.ToString("C")}");
            _appLogger.LogInformation($"Average cost of a Disc in 2017: {statistics.Av2017.ToString("C")}");

            _appLogger.LogInformation($"Total number of CD's: {statistics.TotalCDs}");
            _appLogger.LogInformation($"Total Amount spent on CD's: {statistics.CDCost.ToString("C")}");
            _appLogger.LogInformation($"Average spent on a CD: {statistics.AvCDCost.ToString("C")}");

            _appLogger.LogInformation($"Total number of Records: {statistics.TotalRecords}");
            _appLogger.LogInformation($"Amount spent on Records: {statistics.RecordCost.ToString("C")}");

            _appLogger.LogInformation($"Total amount spent: {statistics.TotalCost.ToString("C")}");

            _appLogger.LogInformation($"Number of Rock albums: {statistics.RockDiscs}");
            _appLogger.LogInformation($"Number of Folk albums: {statistics.FolkDiscs}");
            _appLogger.LogInformation($"Number of Acoustic albums: {statistics.AcousticDiscs}");
            _appLogger.LogInformation($"Number of Jazz/Fusion albums: {statistics.JazzDiscs}");
            _appLogger.LogInformation($"Number of Blues albums: {statistics.BluesDiscs}");
            _appLogger.LogInformation($"Number of Country albums: {statistics.CountryDiscs}");
            _appLogger.LogInformation($"Number of Classical albums: {statistics.ClassicalDiscs}");
            _appLogger.LogInformation($"Number of Soundtrack albums: {statistics.SoundtrackDiscs}");
            _appLogger.LogInformation($"Number of Indispensible albums: {statistics.FourStarDiscs}");
            _appLogger.LogInformation($"Number of Slightly flawed albums: {statistics.ThreeStarDiscs}");
            _appLogger.LogInformation($"Number of Average albums: {statistics.TwoStarDiscs}");
            _appLogger.LogInformation($"Number of Mediocre albums: {statistics.OneStarDiscs}");
        }
    }
}
