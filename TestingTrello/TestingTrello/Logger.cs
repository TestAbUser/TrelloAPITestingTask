using System;
using System.IO;


namespace TestingTrello
{
    public static class Logger
    {
        private static FileStream fileStream;
        private static StreamWriter streamWriter;

        public static void SaveResultsToLog(string results)
        {

            try
            {
                fileStream = new FileStream(@"C:\Users\Rashid_Isayev\source\Localrepos_GIT\TestingTrello\TestingTrello\Log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(results);

            }
            catch (IOException e)
            {
                Console.WriteLine("Cannot open File.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
            }
        }
    }
}
