using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorViewModels
{
	internal class Symbol
	{
		internal const string plus = "+";
		internal const string subtract = "-";
		internal const string multiply = "×";
		internal const string divide = "÷";

		public static bool IsSymbol(string word)
		{
			if (word == plus) return true;
			if (word == subtract) return true;
			if (word == multiply) return true;
			if (word == divide) return true;

			return false;
		}
	}
}
