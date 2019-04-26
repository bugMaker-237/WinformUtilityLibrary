using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinFormUtilityLibrary.ClassUtilities
{
    /// <summary>
    /// Creates logFiles
    /// </summary>
    public static class LogTrace
    {
        /// <summary>
        /// The default name of the log file
        /// </summary>
        public static string logFile { get; set; } = Directory.GetCurrentDirectory() + "/log.lt";
        /// <summary>
        /// This method Write in log file
        /// </summary>
        /// <param name="action">The action to be saved to the log file</param>
        /// <param name="filename">The name of the log file</param>
        /// <param name="categorie">The type of log to be writtin in the file</param>
        /// <remarks>By default the file is saved in the executing directory</remarks>
        public static Results Log(string action, string categorie = "Information", string filename = null)
        {
            try
            {
                if (filename != null) logFile = filename;

                if (logFile != null) logFile = Environment.CurrentDirectory + "\\.log";

                if (!File.Exists(logFile))
                {
                    var fs = File.Create(logFile);
                    fs.Close();
                }
                action += $" \t {DateTime.Now}  \t  [{categorie}] \r";
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(logFile, true))
                {
                    sw.WriteLine(action);
                    sw.Flush();
                    sw.Close();
                }
                return Results.SUCCES;
            }
            catch (Exception exception)
            {
                return Results.FAILURE;
                throw exception;
            }
        }
        /// <summary>
        /// Initialise a new log or clean's an existing log
        /// </summary>
        /// <param name="filename">The name of the log file</param>
        public static Results InitialiseLog(string filename = null)
        {
            try
            {
                File.WriteAllText(filename, null);
                return Results.SUCCES;
            }
            catch (Exception exception)
            {
                return Results.FAILURE;
                throw exception;
            }
        }
    }
}
