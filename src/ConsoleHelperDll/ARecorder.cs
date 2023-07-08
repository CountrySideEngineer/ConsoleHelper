using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelperDll
{
	public abstract class ARecorder
	{
		public string RecordPath { get; }


		/// <summary>
		/// Default constructor.
		/// </summary>
		public ARecorder()
		{
			RecordPath = string.Empty;
		}

		/// <summary>
		/// Constructor with argument.
		/// </summary>
		/// <param name="recordPath"></param>
		public ARecorder(string recordPath)
		{
			RecordPath = recordPath;
		}

		/// <summary>
		/// Record application standard output and error.
		/// </summary>
		/// <param name="appPath">Path to application to run and record its standard and error output.</param>
		/// <param name="args">Command line arguments to path to the appl</param>
		public abstract void Record(string appPath, string args = "");
	}
}
