using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OutputRecorder;

namespace OutputRecorder
{
	internal class Program
	{
		/// <summary>
		/// The main function of OutputRecorder applicatoin.
		/// </summary>
		/// <param name="args">
		/// The 1st argument is the path of the application to run.
		/// And the 2nd and subsequence arguments specify command line arguments to be passed to the application to be executed.
		/// </param>
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Specify the path to the application to run.");
			}
			else
			{
				string appPath = args[0];
				string arguments = string.Empty;
				if (1 < args.Length)
				{
					string[] argArray = args.Skip(1).ToArray();
					arguments = String.Join(" ", argArray);
				}

				string outputPath = GetOutputLogFilePath(appPath);
				Recorder recorder = new Recorder()
				{
					OutputFilePath = outputPath
				};
				recorder.Record(appPath, arguments);
			}
		}

		static string GetOutputLogFilePath(string appPath)
		{
			string fileNameWithouExt = System.IO.Path.GetFileNameWithoutExtension(appPath);
			string dirPath = System.IO.Directory.GetParent(appPath).FullName;
			string outputPath = $@"{dirPath}\{fileNameWithouExt}.log";

			return outputPath;
		}
	}
}
