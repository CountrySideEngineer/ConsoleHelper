using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutputRecorder;

namespace OutputRecorderSample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string appPath = @"E:\development\OutputRecorder\SampleConsole\SampleConsole\bin\Debug\SampleConsole.exe";
			Recorder recorder = new Recorder();
			recorder.Record(appPath);
		}
	}
}
