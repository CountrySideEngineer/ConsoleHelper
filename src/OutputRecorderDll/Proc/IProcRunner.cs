﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutputRecorder.Proc
{
	internal interface IProcRunner
	{
		void Run(string procName, string procArgs = "");
	}
}