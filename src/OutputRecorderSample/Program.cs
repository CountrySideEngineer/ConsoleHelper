using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using OutputRecorder;

namespace OutputRecorderSample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string appPath = @"E:\development\OutputRecorder\SampleConsole\SampleConsole\bin\Debug\SampleConsole.exe";
			string fileNameWithouExt = System.IO.Path.GetFileNameWithoutExtension(appPath);
			string dirPath = System.IO.Directory.GetParent(appPath).FullName;
			string outputPath = $@"{dirPath}\{fileNameWithouExt}.log";
			Recorder recorder = new Recorder();
			recorder.OutputFilePath = outputPath;
			recorder.Record(appPath);
		}
	}
}
