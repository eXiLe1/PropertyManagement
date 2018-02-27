using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Property_Management_System
{
    class Log
    {
        //Name of the file, set here to allow expansion in setting of different files in future.
        private static string FileName = "Log.txt";

        /// <summary>
        /// Creates a new log with a title and clears the old log.
        /// </summary>
        public static void NewLog()
        {
            var LogFile = File.CreateText(FileName);
            var Time = DateTime.Now.ToString("hh:mm:ss tt");
            LogFile.WriteLine("--------------------------------------");
            LogFile.WriteLine("New Log Started");
            LogFile.WriteLine("--------------------------------------");
            LogFile.Flush();
            LogFile.Close();
        }

        /// <summary>
        /// Appends text to the log with the time.
        /// </summary>
        /// <param name="data">The string value that is to be added to the log.</param>
        public static void Commit(string data)
        {
            var LogFile = File.AppendText(FileName);
            //Get the time.
            var time = DateTime.Now.ToString("hh:mm:ss tt");
            //Write the text.
            LogFile.WriteLine("{0}: {1}", time, data);
            //Flush and close the log. 
            LogFile.Flush();
            LogFile.Close();
        }
    }
}
