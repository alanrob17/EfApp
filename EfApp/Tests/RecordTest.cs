﻿using EfApp.Models;
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

            //foreach (var currentRecord in records)
            //{
            //    _appLogger.LogInformation(currentRecord.ToString());
            //}

            //// Add new Record
            //_appLogger.LogInformation("Creating a new record.");
            //var artistId = 864;
            //string dateString = "2022-01-16";
            //DateTime boughtDate = DateTime.Parse(dateString);

            //Record newRecord = new()
            //{
            //    ArtistId = artistId,
            //    Name = "Rockin' the Bass",
            //    Field = "Rock",
            //    Recorded = 2019,
            //    Label = "Wibble",
            //    Pressing = "Aus",
            //    Rating = "****",
            //    Discs = 1,
            //    Media = "CD",
            //    Bought = boughtDate,
            //    Cost = 29.95m,
            //    Review = "This is James's first album."
            //};

            //await _recordService.AddRecordAsync(newRecord);

            //// Get Record by Id
            //record = await _recordService.GetRecordByIdAsync(5286);
            //if (record != null)
            //{
            //    if (record.ArtistAsset != null)
            //    {
            //        _appLogger.LogInformation($"Artist: {record.ArtistAsset.Name}");
            //        _appLogger.LogInformation(record.ToString());
            //    }
            //    else
            //    {
            //        _appLogger.LogInformation("No Record found for this RecordId.");
            //    }
            //}

            //// Get an Artist and their Records by ArtistId
            //var artistRecords = await _recordService.GetArtistRecordsAsync(114);
            //foreach (var currentRecord in artistRecords)
            //{
            //    _appLogger.LogInformation(currentRecord.ToString());
            //}

            //// Get the Record dropdown list for an Artist
            //var recordDictionary = new Dictionary<int, string>();
            //var artistId = 114;
            //records = await _recordService.GetArtistRecordsAsync(artistId);
            //recordDictionary = GetRecordDictionary(recordDictionary, records);

            //record = records.First();
            //_appLogger.LogInformation(record.ArtistAsset.ToString());

            //if (recordDictionary != null)
            //{
            //    foreach (var item in recordDictionary)
            //    {
            //        _appLogger.LogInformation($"{item.Key} - {item.Value}");
            //    }
            //}

            //// Updates a record by RecordId
            //var recordId = 5286;
            //var updateRecord = await _recordService.GetRecordByIdAsync(recordId);
            //if (updateRecord != null)
            //{
            //    _appLogger.LogInformation(updateRecord.ToString());

            //    string dateString = "2022-01-21";
            //    DateTime boughtDate = DateTime.Parse(dateString);

            //    updateRecord.Name = "A Lot Of Fun Aloud";
            //    updateRecord.Field = "Jazz";
            //    updateRecord.Recorded = 2020;
            //    updateRecord.Label = "Woppo";
            //    updateRecord.Pressing = "Jap";
            //    updateRecord.Rating = "***";
            //    updateRecord.Discs = 1;
            //    updateRecord.Media = "CD";
            //    updateRecord.Bought = boughtDate;
            //    updateRecord.Cost = 23.99m;
            //    updateRecord.CoverName = null;
            //    updateRecord.Review = "This is James' second album";

            //    await _recordService.UpdateRecordAsync(updateRecord);
            //    _appLogger.LogInformation("Record updated.");
            //}

            //// Delete a Record by RecordId
            //var recordId = 5285;
            //record = await _recordService.GetRecordByIdAsync(recordId);

            //if (record != null)
            //{
            //    await _recordService.DeleteRecordAsync(record);
            //    _appLogger.LogInformation("Record deleted.");
            //}

            //// Get a Record by its Name
            //records = await _recordService.GetRecordByNameAsync("blonde on Blonde");
            //foreach (var currentRecord in records)
            //{
            //    _appLogger.LogInformation(currentRecord.ToString());
            //}

            //// Get Records by year e.g. 1974
            //var year = 1974;
            //records = await _recordService.GetRecordsByYearAsync(year);
            //foreach (var currentRecord in records)
            //{
            //    _appLogger.LogInformation(currentRecord.ToString());
            //}

            //// Get Total Number of CD's in my collection
            int total = await _recordService.GetTotalNumberOfCDsAsync();
            _appLogger.LogInformation($"Total number of CD's: {total}.");  // 1477
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
