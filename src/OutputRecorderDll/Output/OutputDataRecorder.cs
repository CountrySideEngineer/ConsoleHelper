using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder.Output
{
	internal class OutputDataRecorder
	{
		protected List<string> _receivedDataCollection;

		public string OutputFilePath { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public OutputDataRecorder()
		{
			OutputFilePath = string.Empty;
			_receivedDataCollection = new List<string>();
		}

		/// <summary>
		/// Data receiving event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DataReceivedEventHandler(object sender, DataReceivedEventArgs e)
		{
			string receiveData = e.Data;
			_receivedDataCollection.Add(receiveData);
		}

		/// <summary>
		/// Data finished event handler.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DataReceiveFinishedEventHandler(object sender, EventArgs e)
		{
			Flush();
		}

		/// <summary>
		/// Output receiving data into a file.
		/// </summary>
		public void Flush()
		{
			try
			{
				using (var writer = new StreamWriter(OutputFilePath, false))
				{
					Flush(writer);
				}
			}
			catch (Exception ex)
			when ((ex is UnauthorizedAccessException) ||
				(ex is ArgumentException) ||
				(ex is ArgumentNullException) ||
				(ex is DirectoryNotFoundException) ||
				(ex is IOException) ||
				(ex is PathTooLongException) ||
				(ex is SecurityException))
			{
				//Ignore the exception.
			}
		}

		/// <summary>
		/// Output receiving data into a stream.
		/// </summary>
		/// <param name="outputStream"></param>
		public void Flush(StreamWriter outputStream)
		{
			try
			{
				foreach (var item in _receivedDataCollection)
				{
					if (null != item)
					{
						outputStream.WriteLine(item);
					}
				}
			}
			catch (IOException)
			{
				//Skip the exception handling.
			}
		}
	}
}
