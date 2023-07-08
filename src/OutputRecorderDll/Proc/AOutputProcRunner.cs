using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder.Proc
{
	public abstract class AOutputProcRunner : IProcRunner
	{
		public event DataReceivedEventHandler ReceiveStandardData;
		public event DataReceivedEventHandler ReceiveErrorData;

		public delegate void DataReceiveFinishedEventHandler(object sender, EventArgs e);
		public event EventHandler ReceiveFinished;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public AOutputProcRunner() { }

		/// <summary>
		/// Abstract method to run process.
		/// </summary>
		/// <param name="procName"></param>
		/// <param name="procArgs"></param>
		public abstract void Run(string procName, string procArgs);

		/// <summary>
		/// Event handler to handle receiving standard output.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void StandardDataReceived(object sender, DataReceivedEventArgs e)
		{
			ReceiveStandardData?.Invoke(this, e);
		}

		/// <summary>
		/// Event handler to handle receiving error output.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			ReceiveErrorData?.Invoke(this, e);
		}

		/// <summary>
		/// Event handler to receive the process finished.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DataReceiveFinished(object sender, EventArgs e)
		{
			ReceiveFinished?.Invoke(this, e);
		}
	}
}
