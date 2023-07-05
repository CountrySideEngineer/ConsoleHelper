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
		internal StandardOutputData _stdOutput;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Recorder() : base()
		{
			_procRunner = new ProcessRunner();
			_recorder = new OutputDataRecorder();
			_stdOutput = new StandardOutputData();
		}

		public override void Record(string appPath, string args = "")
		{
			_recorder.OutputFilePath = OutputFilePath;

			_procRunner.ReceiveStandardData += _recorder.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData += _recorder.DataReceivedEventHandler;
			_procRunner.ReceiveFinished += _recorder.DataReceiveFinishedEventHandler;

			_procRunner.ReceiveStandardData += _stdOutput.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData += _stdOutput.ErrorReceivedEventHandler;
			_procRunner.ReceiveFinished += _stdOutput.DataReceivedFinishedEventHandler;

			_procRunner.Run(appPath, args);

			_procRunner.ReceiveStandardData -= _recorder.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData -= _recorder.DataReceivedEventHandler;
			_procRunner.ReceiveFinished -= _recorder.DataReceiveFinishedEventHandler;

			_procRunner.ReceiveStandardData -= _stdOutput.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData -= _stdOutput.ErrorReceivedEventHandler;
			_procRunner.ReceiveFinished -= _stdOutput.DataReceivedFinishedEventHandler;

		}
	}
}
