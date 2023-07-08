using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelperLib.Output
{
	internal class StandardOutputData
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public StandardOutputData() { }

		/// <summary>
		/// Standard output receiving event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DataReceivedEventHandler(object sender, DataReceivedEventArgs e)
		{
			Console.WriteLine(e.Data);
		}

		/// <summary>
		/// Error output receiving event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ErrorReceivedEventHandler(object sender, DataReceivedEventArgs e)
		{
			Console.Error.WriteLine(e.Data);
		}

		/// <summary>
		/// Process finished event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DataReceivedFinishedEventHandler(object sender, EventArgs e) { }
	}
}
