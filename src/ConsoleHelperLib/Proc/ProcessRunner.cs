using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHelperLib.Proc
{
	internal class ProcessRunner : AOutputProcRunner
	{
		protected ProcessStartInfo _procStartInfo = null;

		protected bool _isContinue = true;
		protected DataReceivedEventHandler _outputDataReceiveEventHandler;
		protected DataReceivedEventHandler _errorDataReceivedEventHandler;
		protected EventHandler _exitEventHandler;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public ProcessRunner() : base()
		{
			_isContinue = false;
		}

		/// <summary>
		/// Run process.
		/// </summary>
		/// <param name="procName">Path to file to execute in process.</param>
		/// <param name="procArgs">Argument to pass to the application.</param>
		/// <exception cref="ArgumentException"></exception>
		public override void Run(string procName, string procArgs)
		{
			if ((string.IsNullOrEmpty(procName)) || (string.IsNullOrWhiteSpace(procName)))
			{
				throw new ArgumentException();
			}

			try
			{
				SetupStartInfo(procName, procArgs);
				Run();
			}
			catch (InvalidOperationException)
			{
				throw new ArgumentException("Requested operation has been invalid.");
			}
			catch (Win32Exception)
			{
				string fileName = System.IO.Path.GetFileName(procName);
				throw new ArgumentException($"{fileName} is not executable.");
			}
		}

		/// <summary>
		/// Setup ProcessStartInfo object to be set to the process.
		/// </summary>
		/// <param name="procName">Path to file to execute in process.</param>
		/// <param name="procArgs">Argument to pass to the application.</param>
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

		/// <summary>
		/// Startup setter.
		/// </summary>
		/// <param name="proc">Process object to set ProcessStartupInformation.</param>
		protected virtual void SetStartInfo(Process proc)
		{
			proc.StartInfo = _procStartInfo;
		}

		/// <summary>
		/// Run the process.
		/// </summary>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="Win32Exception"></exception>
		protected virtual void Run()
		{
			try
			{
				using (var proc = new Process())
				{
					SetStartInfo(proc);

					SetupEventHandler(proc);

					RunProcess(proc);

					ReleaseEventHandler(proc);
				}
			}
			catch (Exception ex)
			when ((ex is InvalidOperationException) || (ex is Win32Exception))
			{
				throw ex;
			}
		}

		/// <summary>
		/// Setup process data.
		/// </summary>
		/// <param name="proc">Process obejct to be setup.</param>
		protected void SetupEventHandler(Process proc)
		{
			_outputDataReceiveEventHandler = new DataReceivedEventHandler(StandardDataReceived);
			_errorDataReceivedEventHandler = new DataReceivedEventHandler(ErrorDataReceived);
			_exitEventHandler = new EventHandler(DataReceiveFinished);

			proc.OutputDataReceived += _outputDataReceiveEventHandler;
			proc.ErrorDataReceived += _errorDataReceivedEventHandler;
			proc.EnableRaisingEvents = true;
			proc.Exited += _exitEventHandler;
		}

		/// <summary>
		/// Run process.
		/// </summary>
		/// <param name="proc">Process data to run.</param>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="Win32Exception"></exception>
		protected virtual void RunProcess(Process proc)
		{
			try
			{
				_isContinue = true;

				proc.Start();
				proc.BeginOutputReadLine();
				proc.BeginErrorReadLine();
				using (var stdin = proc.StandardInput)
				{
					do
					{
						if (Console.KeyAvailable)
						{
							var inputData = Console.ReadKey();
							stdin.Write(inputData.KeyChar);
							if (inputData.KeyChar.Equals('\r'))
							{
								stdin.Write('\n');
							}
						}
						else
						{
							Thread.Sleep(100);
						}
					} while (true == _isContinue);
				}
				proc.WaitForExit();
			}
			catch (Exception ex)
			when ((ex is InvalidOperationException) || (ex is Win32Exception))
			{
				throw ex;
			}
		}

		/// <summary>
		/// Release process event handler.
		/// </summary>
		/// <param name="proc">Process object to release.</param>
		protected void ReleaseEventHandler(Process proc)
		{
			proc.OutputDataReceived -= _outputDataReceiveEventHandler;
			proc.ErrorDataReceived -= _errorDataReceivedEventHandler;
			proc.Exited -= _exitEventHandler;
		}

		/// <summary>
		/// Event handler to receive and handle exit event the process raised.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event argument.</param>
		protected override void DataReceiveFinished(object sender, EventArgs e)
		{
			base.DataReceiveFinished(sender, e);
			
			_isContinue = false;
		}
	}
}
