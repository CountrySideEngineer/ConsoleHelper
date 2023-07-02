using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder.Proc
{
	internal class ProcessRunner : AOutputProcRunner
	{
		protected ProcessStartInfo _procStartInfo = null;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ProcessRunner() : base() { }

		public override void Run(string procName, string procArgs)
		{
			SetupStartInfo(procName, procArgs);
			Run();
		}

		protected virtual void SetupStartInfo(string procName, string procArgs)
		{
			_procStartInfo = new ProcessStartInfo()
			{
				FileName = procName,
				CreateNoWindow = true,
				UseShellExecute = false,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			};
		}

		protected virtual void Run()
		{
			using (var proc = new Process())
			{
				proc.StartInfo = _procStartInfo;
				proc.OutputDataReceived += new DataReceivedEventHandler(StandardDataReceived);
				proc.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataReceived);
				proc.Exited += new EventHandler(DataReceiveFinished);
				proc.EnableRaisingEvents = true;
				proc.Start();
				proc.BeginOutputReadLine();
				proc.BeginErrorReadLine();
				proc.WaitForExit();

				Console.WriteLine("Exited");
			}
		}
	}
}
