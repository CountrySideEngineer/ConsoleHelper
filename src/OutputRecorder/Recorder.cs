using OutputRecorder.Output;
using OutputRecorder.Proc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder
{
	public class Recorder : ARecorder
	{
		public string OutputFilePath { get; set; }

		internal AOutputProcRunner _procRunner;
		internal OutputDataRecorder _recorder;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Recorder() : base()
		{
			_procRunner = new ProcessRunner();
			_recorder = new OutputDataRecorder();
		}

		public override void Record(string appPath, string args = "")
		{
			_recorder.OutputFilePath = OutputFilePath;

			_procRunner.ReceiveStandardData += _recorder.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData += _recorder.DataReceivedEventHandler;
			_procRunner.ReceiveFinished += _recorder.DataReceiveFinishedEventHandler;

			_procRunner.Run(appPath, args);
		}
	}
}
