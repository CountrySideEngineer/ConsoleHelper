using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder.Output
{
	internal class OutputDataRecorder
	{
		protected List<string> _receivedDataCollection;

		public OutputDataRecorder()
		{
			_receivedDataCollection = new List<string>();
		}


		public void DataReceivedEventHandler(object sender, DataReceivedEventArgs e)
		{
			string receiveData = e.Data;
			_receivedDataCollection.Add(receiveData);
		}

		public void DataReceiveFinishedEventHandler(object sender, EventArgs e)
		{
			Flush();
		}

		public void Flush()
		{
			Console.WriteLine("sample output");

		}

		public void Flush(StreamWriter outputStream)
		{
			foreach (var item in _receivedDataCollection)
			{
				outputStream.WriteLine(item);
			}
		}
	}
}
