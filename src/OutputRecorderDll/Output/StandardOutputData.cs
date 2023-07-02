using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder.Output
{
	internal class StandardOutputData
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public StandardOutputData() { }

		public void DataReceivedEventHandler(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data);
		}

		public void ErrorReceivedEventHandler(object sender, DataReceivedEventArgs e)
		{
			Console.Error.WriteLine(e.Data);
		}

		public void DataReceivedFinishedEventHandler(object sender, EventArgs e) { }
	}
}
