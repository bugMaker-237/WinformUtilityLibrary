using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinFormUtilityLibrary.DataUtilities
{
    public enum LogLevel
    {
        Notice = 0,
        Warning = 1,
        Error = 2,
        MuteLog = 99
    }

    public interface ILoggerOutput
    {
        void WriteLine(String log, LogLevel level, bool tag = false);
        void WriteLine(String log);
        void Write(String log, LogLevel level, bool tag = false);
        void Write(String log);
    }

    public class ConsoleLoggerOutput : ILoggerOutput
    {
        public void WriteLine(String log, LogLevel level, bool tag = false)
        {
            Console.WriteLine(log);
        }

        public void WriteLine(String log)
        {
            Console.WriteLine(log);
        }

        public void Write(String log, LogLevel level, bool tag = false)
        {
            Console.Write(log);
        }

        public void Write(String log)
        {
            Console.Write(log);
        }
    }
    public class FileLoggerOutput : ILoggerOutput
    {
        private string _pathToLog;

        public FileLoggerOutput(string pathToLog)
        {
            if (pathToLog == null) throw new ArgumentNullException();
            this._pathToLog = pathToLog;

            if (!Directory.Exists(Path.GetPathRoot(this._pathToLog))) throw new IOException();

            using (var fs = File.Open(pathToLog, FileMode.OpenOrCreate))
                fs.Close();
        }
        public void Write(string log)
        {
            using (var sw = File.AppendText(_pathToLog))
            {
                sw.Write(log.Trim());
                sw.Flush();

                sw.Close();
            }
        }

        public void Write(string log, LogLevel level, bool tag = false)
        {
            using (var sw = File.AppendText(_pathToLog))
            {
                var txt = tag ? ("[Level : " + level + "] => " + log?.Trim()) : log?.Trim();

                sw.Write(txt);
                sw.Flush();

                sw.Close();
            }
        }

        public void WriteLine(string log)
        {
            using (var sw = File.AppendText(_pathToLog))
            {
                sw.WriteLine(sw.NewLine + log?.Trim() + sw.NewLine);
                sw.Flush();

                sw.Close();
            }
        }

        public void WriteLine(string log, LogLevel level, bool tag = false)
        {
            using (var sw = File.AppendText(_pathToLog))
            {
                var txt = sw.NewLine + (tag ? ("[Level : " + level + "] => " + log?.Trim()) : log?.Trim()) + sw.NewLine;

                sw.WriteLine(txt);
                sw.Flush();

                sw.Close();
            }
        }
    }

    public class FolderLoggerOutput : ILoggerOutput
    {
        private FileLoggerOutput fileLogger;
        private string _pathToLogFolder;
        public string fileName { get; private set; }

        public FolderLoggerOutput(string pathToLogFolder)
        {
            if (pathToLogFolder == null) throw new ArgumentNullException();
            this._pathToLogFolder = pathToLogFolder;

            if (!Directory.Exists(Path.GetPathRoot(this._pathToLogFolder))) throw new IOException();

            fileName = GetTodaysFileName(this._pathToLogFolder);
            using (var fs = File.Open(fileName, FileMode.OpenOrCreate))
                fs.Close();

            fileLogger = new FileLoggerOutput(fileName);

        }

        private string GetTodaysFileName(string pathToLogFolder)
        {
            try
            {
                Directory.CreateDirectory(pathToLogFolder);
                var file = DateTime.Now.ToString("yyyyMMdd-");

                var all = Directory.EnumerateFiles(pathToLogFolder, file)
                         .OrderBy(s => s.Split('-')[1])
                         .ToList();
                if (all.Count == 0)
                {
                    file += "1";
                    return Path.Combine(pathToLogFolder, file);
                }
                else
                {
                    FileInfo fi = new FileInfo(all.Last());
                    if (fi.Length > 50000000)
                    {
                        file += (long.Parse(all.Last().Split('-')[1]) + 1);
                    }
                    else
                    {
                        file = all.Last();
                    }
                }
                return file;
            }
            catch (Exception)
            {
                return Path.Combine(pathToLogFolder, DateTime.Now.ToString("yyyyMMdd-1"));
            }

        }

        public void Write(string log)
        {
            fileLogger.Write(log);
        }

        public void Write(string log, LogLevel level, bool tag = false)
        {
            fileLogger.Write(log, level, tag);
        }

        public void WriteLine(string log)
        {
            fileLogger.WriteLine(log);
        }

        public void WriteLine(string log, LogLevel level, bool tag = false)
        {
            fileLogger.WriteLine(log, level, tag);
        }
    }

    public class LoggerOutputList : ILoggerOutput
    {
        private IList<ILoggerOutput> _list = new List<ILoggerOutput>();
        public IList<ILoggerOutput> List { get { return _list; } }

        public void WriteLine(String log, LogLevel level, bool tag = false)
        {
            foreach (ILoggerOutput logger in _list) logger.WriteLine(log, level,tag);
        }

        public void WriteLine(String log)
        {
            WriteLine(log, LogLevel.Notice);
        }

        public void Write(String log, LogLevel level, bool tag = false)
        {
            foreach (ILoggerOutput logger in _list) logger.Write(log, level, tag);
        }

        public void Write(String log)
        {
            Write(log, LogLevel.Notice);
        }
    }

    public class Logger
    {
        private ILoggerOutput _output;
        private static Logger instance;


        public LogLevel Level { get; set; }
        public bool IsStackTraceShown { get; set; }

        private Logger(ILoggerOutput output, LogLevel level, bool isStackTraceShown)
        {
            this._output = output;
            this.Level = level;
            this.IsStackTraceShown = isStackTraceShown;
        }

        public static Logger GetInstance(ILoggerOutput output, LogLevel level, bool isStackTraceShown)
        {
            return (instance = new Logger(output, level, isStackTraceShown));
        }


        private void WriteTime(LogLevel level, bool tag = false)
        {
            _output.WriteLine(DateTime.Now + "   ", level, tag);
        }

        public void WriteLine(String log, LogLevel level, bool tag = false)
        {
            if (Level > level || level == LogLevel.MuteLog) return;
            lock (instance)
            {
                WriteTime(level, tag);
                _output.WriteLine("\t\t" + log, level);
            }
        }

        public void WriteLine(String log, bool tag = false)
        {
            WriteLine(log, LogLevel.Notice, tag);
        }

        public void Write(String log, LogLevel level, bool tag = false)
        {
            if (Level > level || level == LogLevel.MuteLog) return;
            lock (instance)
            {
                WriteTime(level, tag);
                _output.Write("\t\t" + log, level);
            }
        }

        public void Write(String log, bool tag = false)
        {
            Write(log, LogLevel.Notice, tag);
        }

        public void Write(Exception ex, String log)
        {
            if (Level == LogLevel.MuteLog) return;
            LogLevel level = LogLevel.Error;
            lock (instance)
            {
                WriteTime(level, true);
                if (!String.IsNullOrEmpty(log)) _output.WriteLine("\t\t" + log, level);
                _output.WriteLine("\t\tError Message: " + ex.Message, level);
                _output.WriteLine("\t\tThrown: " + ex.GetType().Name, level);
                if (IsStackTraceShown)
                {
                    _output.WriteLine("\t\tSource: " + ex.Source, level);
                    if (ex.InnerException != null) _output.WriteLine("\t\tInner Source: " + ex, level);
                    _output.WriteLine("\t\tStackTrace:", level);
                    _output.WriteLine(ex.StackTrace, level);
                    if (ex.InnerException != null)
                    {
                        _output.WriteLine("\t\tInner StackTrace:", level);
                        _output.WriteLine(ex.InnerException.StackTrace, level);
                    }
                    _output.WriteLine("\t\tTargetSite: " + ex.TargetSite, level);
                }
                _output.WriteLine("", level);
            }
        }

        public void Write(Exception ex)
        {
            Write(ex, null);
        }

        public void AddOutput(ILoggerOutput output)
        {
            if (output == null) throw new ArgumentNullException();

            lock (instance)
            {
                if (_output is LoggerOutputList)
                {
                    ((LoggerOutputList)_output).List.Add(output);
                }
                else
                {
                    LoggerOutputList outList = new LoggerOutputList();
                    if (_output != null) outList.List.Add(_output);
                    outList.List.Add(output);
                    _output = outList;
                }
            }
        }

        public void ReplaceOutput(ILoggerOutput output)
        {
            if (output == null) throw new ArgumentNullException();
            lock (instance)
            {
                _output = output;
            }
        }

        public ILoggerOutput Output { get { return _output; } }
    }
}
