using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleHelperSampleCS
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Check action when waiting for the input with ReadLine method.
			Console.WriteLine("This is sample application about ConsoleHelper.");
			Console.WriteLine();
			Console.WriteLine("Press enter key to continue the process.");
			var readLine = Console.ReadLine();
			Console.WriteLine($"len = {readLine.Length}");
			Console.WriteLine($"readLine = {readLine}");

			//Check action when waiting for the input with Read method.
			Console.WriteLine("Press enter key to continue the process one more.");
			var read = Console.Read();
			Console.WriteLine($"read = {read}(0x{read:X})");

			//Check action of ReadLine method after Read one.
			Console.WriteLine("Press enter key to exit the process.");
			readLine = Console.ReadLine();
			Console.WriteLine($"len = {readLine.Length}");
			Console.WriteLine($"readLine = {readLine}");
		}
	}
}
