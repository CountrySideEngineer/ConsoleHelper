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

		protected bool _isContinue = true;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ProcessRunner() : base()
		{
			_isContinue = false;
		}

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
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			};
		}

		protected virtual void Run()
		{
			_isContinue = true;

			using (var proc = new Process())
			{
				proc.StartInfo = _procStartInfo;
				proc.OutputDataReceived += new DataReceivedEventHandler(StandardDataReceived);
				proc.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataReceived);
				proc.EnableRaisingEvents = true;
				proc.Exited += new EventHandler(DataReceiveFinished);
				proc.Start();
				proc.BeginOutputReadLine();
				proc.BeginErrorReadLine();				
				using (var stdin = proc.StandardInput)
				{
					string inputText = string.Empty;
					do
					{
						ConsoleKeyInfo keyInfo = Console.ReadKey();
						stdin.WriteLine(keyInfo.KeyChar);
					} while ((inputText != null) && (true == _isContinue));
				}
				proc.WaitForExit();
			}
		}

		protected override void DataReceiveFinished(object sender, EventArgs e)
		{
			base.DataReceiveFinished(sender, e);

			_isContinue = false;

			Console.WriteLine("Push enter key to end program.");
		}
	}
}
