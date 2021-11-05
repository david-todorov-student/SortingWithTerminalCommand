using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingCSVdotNET
{
    public class Program
    {
        static void DownloadFile(string url, string outputName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            // The File to be downloaded must not have a header line, otherwise,
            // the sorting will result in the header line being placed somewhere in the middle of the sorted file.
            process.StandardInput.WriteLine("curl {0} -o {1}", url, outputName);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.WriteLine(process.StandardError.ReadToEnd());
        }

        static void SortFile(string fileName, string outputName)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.StandardInput.WriteLine("sort {0} /o {1}", fileName, outputName); 
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.WriteLine(process.StandardError.ReadToEnd());
        }

        static void Main(string[] args) 
        {
            Console.WriteLine("Enter url for the csv file to be downloaded:");
            string fileUrl = Console.ReadLine();
            Console.WriteLine("Enter the name you wish the file to be stored as: (add the .csv extension)");
            string outputName = Console.ReadLine();
            DownloadFile(fileUrl, outputName);
            if (File.Exists(outputName))
            {
                Console.WriteLine("File downloaded successfully");
                Console.WriteLine("Enter name for the sorted file to be stored as.");
                string storedOutputName = Console.ReadLine();
                SortFile(outputName, storedOutputName);
                if (File.Exists(storedOutputName))
                {
                    Console.WriteLine("File sorted successfully");
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            Console.Read();
        }
    }
}
