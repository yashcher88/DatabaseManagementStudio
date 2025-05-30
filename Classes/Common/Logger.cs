using Avalonia.Controls;
using Avalonia.Threading;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DatabaseManagementStudio.Classes
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error
    }

    public class Logger
    {
        private readonly object _lock = new();
        private readonly string _logFilePath;
        public LogLevel MinimumLevel { get; set; } = LogLevel.Info;
        public bool LogToConsole { get; set; } = true;

        public Logger()
        {
            
            _logFilePath = Path.Combine(AppContext.BaseDirectory, "log", DateTime.Now.ToString("yyyy-MM-dd")+".log") ;

            // Создать директорию при необходимости
            var directory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (level < MinimumLevel)
                return;

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var fullMessage = $"[{timestamp}] [{level}] {message}";

            lock (_lock)
            {
                try
                {
                    File.AppendAllText(_logFilePath, fullMessage + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Logger failed to write to file: " + ex.Message);
                }

                if (LogToConsole)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = level switch
                    {
                        LogLevel.Debug => ConsoleColor.Gray,
                        LogLevel.Info => ConsoleColor.White,
                        LogLevel.Warning => ConsoleColor.Yellow,
                        LogLevel.Error => ConsoleColor.Red,
                        _ => originalColor
                    };

                    Console.WriteLine(fullMessage);
                    Console.ForegroundColor = originalColor;
                }
            }
        }

        public Task<ButtonResult> ShowMessage(string msg)
        {
            var messageBox = MessageBoxManager.GetMessageBoxStandard(
                        Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown",
                        msg,
                        ButtonEnum.Ok
                    );
            return messageBox.ShowWindowAsync();
        }
        public Task<ButtonResult> ShowError(string msg)
        {
            var messageBox = MessageBoxManager.GetMessageBoxStandard(
                        Assembly.GetEntryAssembly()?.GetName().Name ?? "Unknown",
                        msg,
                        ButtonEnum.Ok,
                        Icon.Error
                    );
            return messageBox.ShowWindowAsync();
        }

        public void Debug(string message) => Log(message, LogLevel.Debug);
        public void Info(string message) => Log(message, LogLevel.Info);
        public void Warn(string message) => Log(message, LogLevel.Warning);
        async public void Error(string message)
        {
            Log(message, LogLevel.Error);
            await ShowError(message);
        }
    }
}