﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHelperLib
{
	internal interface IProcessRecorder
	{
		void Record(string path, string args);
	}
}
