using EfApp.Models;
using EfApp.Services;
using EfApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfApp.Tests
{
    internal class RecordTest
    {
        private readonly RecordService _recordService;
        private readonly AppLogger _appLogger;

        public RecordTest(RecordService recordService, AppLogger appLogger)
        {
            _recordService = recordService;
            _appLogger = appLogger;
        }

        public async Task RunTestsAsync()
        {
            Record record = new();

            //// Get All Records
            var records = await _recordService.GetAllRecordsAsync();
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }

            //// Add new Record
            _appLogger.LogInformation("Creating a new record.");
            var artistId = 864;
            string dateString = "2022-01-16";
            DateTime boughtDate = DateTime.Parse(dateString);

            Record newRecord = new()
            {
                ArtistId = artistId,
                Name = "Rockin' the Bass",
                Field = "Rock",
                Recorded = 2019,
                Label = "Wibble",
                Pressing = "Aus",
                Rating = "****",
                Discs = 1,
                Media = "CD",
                Bought = boughtDate,
                Cost = 29.95m,
                Review = "This is James's first album."
            };

            await _recordService.AddRecordAsync(newRecord);

            //// Get the Record dropdown list for an Artist
            var recordDictionary = new Dictionary<int, string>();
            artistId = 114;
            records = await _recordService.GetArtistRecordsAsync(artistId);
            recordDictionary = GetRecordDictionary(recordDictionary, records);

            if (recordDictionary != null)
            {
                foreach (var item in recordDictionary)
                {
                    _appLogger.LogInformation($"{item.Key} - {item.Value}");
                }
            }

            //// Updates a record by RecordId
            var recordId = 5286;
            var updateRecord = await _recordService.GetRecordByIdAsync(recordId);
            if (updateRecord != null)
            {
                _appLogger.LogInformation(updateRecord.ToString());

                dateString = "2022-01-21";
                boughtDate = DateTime.Parse(dateString);

                updateRecord.Name = "A Lot Of Fun Aloud";
                updateRecord.Field = "Jazz";
                updateRecord.Recorded = 2020;
                updateRecord.Label = "Woppo";
                updateRecord.Pressing = "Jap";
                updateRecord.Rating = "***";
                updateRecord.Discs = 1;
                updateRecord.Media = "CD";
                updateRecord.Bought = boughtDate;
                updateRecord.Cost = 23.99m;
                updateRecord.CoverName = null;
                updateRecord.Review = "This is James' second album";

                await _recordService.UpdateRecordAsync(updateRecord);
                _appLogger.LogInformation("Record updated.");
            }

            //// Delete a Record by RecordId
            recordId = 5285;
            record = await _recordService.GetRecordByIdAsync(recordId);

            if (record != null)
            {
                await _recordService.DeleteRecordAsync(record);
                _appLogger.LogInformation("Record deleted.");
            }

            //// Get a Record by its Name
            records = await _recordService.GetRecordByNameAsync("blonde on Blonde");
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }

            //// Get Records by year e.g. 1974
            var year = 1974;
            records = await _recordService.GetRecordsByYearAsync(year);
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }

            //// Get Total Number of CD's in my collection
            int total = await _recordService.GetTotalNumberOfCDsAsync();
            _appLogger.LogInformation($"Total number of CD's: {total}.");

            //// Get the Total Number Of Discs. This includes all media types.
            total = await _recordService.GetTotalNumberOfDiscsAsync();
            _appLogger.LogInformation($"Total number of Disc's: {total}.");

            //// Get the Total Number Of Records. Media type is "R"
            total = await _recordService.GetTotalNumberOfRecordsAsync();
            _appLogger.LogInformation($"Total number of Records: {total}.");

            //// Get the Total Number Of Blurays where Media type contains "Blu-ray"
            total = await _recordService.GetTotalNumberOfBluraysAsync();
            _appLogger.LogInformation($"Total number of Blu-rays: {total}.");

            //// Get the number of DVD's where Media type is "DVD"
            total = await _recordService.GetTotalNumberOfDVDsAsync();
            _appLogger.LogInformation($"Total number of DVD's: {total}.");

            ////// Get the sum Disc Count For a particular Year e.g. 1974
            recordId = 1974;
            total = await _recordService.GetTotalDiscsByYearAsync(year);
            _appLogger.LogInformation($"Total number of discs for {year}: {total}.");

            //// Get the Disc Count where Records were bought for a particular Year e.g. 2000
            total = 2015;
            total = await _recordService.GetTotalDiscsByYearBoughtAsync(year);
            _appLogger.LogInformation($"Total number of discs bought in {year}: {total} discs.");

            //// Get the total number of Record.Review where the Review is not null or empty
            total = await _recordService.GetNoRecordReviewCountAsync();
            _appLogger.LogInformation($"Total number of Records with no Review: {total}.");

            //// Get a List of Record with null or empty Review
            records = await _recordService.GetAllNoReviewRecordsAsync();
            _appLogger.LogInformation("Records with no Review:");
            foreach (var currentRecord in records)
            {
                _appLogger.LogInformation(currentRecord.ToString());
            }

            //// Get the total Cost spent for a Year bought e.g. 1974
            year = 1974;
            decimal totalCost = await _recordService.GetTotalCostByYearBoughtAsync(year);
            _appLogger.LogInformation($"Total cost for {year}: {totalCost:C}.");
        }

        private static Dictionary<int, string>? GetRecordDictionary(Dictionary<int, string> recordDictionary, IEnumerable<Record> records)
        {
            recordDictionary.Clear();

            recordDictionary.Add(0, "Select a record");

            var orderedRecords = records.OrderByDescending(r => r.Recorded);

            foreach (var record in orderedRecords)
            {
                if (!recordDictionary.ContainsKey(record.RecordId))
                {
                    recordDictionary.Add(record.RecordId, record.Name);
                }
            }

            return recordDictionary;
        }
    }
}
