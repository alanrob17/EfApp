using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfApp.Models; // Add this line to specify which 'Record' to use

namespace EfApp.Utilities 
{ 
    public class AppLogger
    {
        private readonly ILogger<AppLogger> _logger;

        public AppLogger(ILogger<AppLogger> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message)
        {
            _logger.LogError(message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogTrace(string message)
        {
            _logger.LogTrace(message);
        }
    }
}
