using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillslabAssignment.Common.Logger
{

    public class Logger : ILogger
    {
        private readonly string filePath;
        public Logger(string filePath)
        {
            this.filePath = filePath;
            EnsureLogFileExists();
        }

        public async Task LogAsync(string message)
        {
            await LogInternalAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - INFO: {message}");
        }

        public async Task LogAsync(string message, Exception exception)
        {
            await LogInternalAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {message}\nException: {exception}");
        }

        public async Task LogAsync(Exception exception)
        {
            await LogInternalAsync($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: Exception: {exception}");
        }

        private async Task LogInternalAsync(string logEntry)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    await writer.WriteLineAsync(logEntry);
                    await writer.WriteLineAsync(new string('-', 80));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        private void EnsureLogFileExists()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("Log file created on: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        writer.WriteLine(new string('-', 80));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating log file: {ex.Message}");
            }
        }
    }

}
