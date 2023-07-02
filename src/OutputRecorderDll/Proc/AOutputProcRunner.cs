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

		public abstract void Run(string procName, string procArgs);

		protected virtual void StandardDataReceived(object sender, DataReceivedEventArgs e)
		{
			ReceiveStandardData?.Invoke(this, e);
		}

		protected virtual void ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			ReceiveErrorData?.Invoke(this, e);
		}

		protected virtual void DataReceiveFinished(object sender, EventArgs e)
		{
			ReceiveFinished?.Invoke(this, e);
		}
	}
}
