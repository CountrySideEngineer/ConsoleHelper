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

		/// <summary>
		/// Record the application standard output content into log output file.
		/// </summary>
		/// <param name="appPath">Path to file to execute as process.</param>
		/// <param name="args">Argument of the application.</param>
		public override void Record(string appPath, string args = "")
		{
			_recorder.OutputFilePath = OutputFilePath;

			SetUpOutputReceiver(ref _recorder);
			SetUpStdOutputRecorder(ref _stdOutput);

			_procRunner.Run(appPath, args);

			ReleaseOutputReceiver(ref _recorder);
			ReleaseStdOutputRecorder(ref _stdOutput);
		}

		internal virtual void SetUpOutputReceiver(ref OutputDataRecorder recorder)
		{
			_procRunner.ReceiveStandardData += recorder.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData += recorder.DataReceivedEventHandler;
			_procRunner.ReceiveFinished += recorder.DataReceiveFinishedEventHandler;
		}

		internal virtual void SetUpStdOutputRecorder(ref StandardOutputData stdOutput)
		{
			_procRunner.ReceiveStandardData += stdOutput.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData += stdOutput.ErrorReceivedEventHandler;
			_procRunner.ReceiveFinished += stdOutput.DataReceivedFinishedEventHandler;
		}

		internal virtual void ReleaseOutputReceiver(ref OutputDataRecorder recorder)
		{
			_procRunner.ReceiveStandardData -= recorder.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData -= recorder.DataReceivedEventHandler;
			_procRunner.ReceiveFinished -= recorder.DataReceiveFinishedEventHandler;
		}

		internal virtual void ReleaseStdOutputRecorder(ref StandardOutputData stdOutput)
		{
			_procRunner.ReceiveStandardData -= stdOutput.DataReceivedEventHandler;
			_procRunner.ReceiveErrorData -= stdOutput.ErrorReceivedEventHandler;
			_procRunner.ReceiveFinished -= stdOutput.DataReceivedFinishedEventHandler;
		}
	}
}
