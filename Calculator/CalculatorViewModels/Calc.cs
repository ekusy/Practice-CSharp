using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorViewModels
{
	public class Calc
	{
		/// <summary>
		/// 計算
		/// </summary>
		/// <param name="model">計算機ビューモデル</param>
		/// <returns>計算結果</returns>
		public static string Calculation(CalculatorViewModel model)
		{
			string formula = model.Result.Value;
			double num1;
			double num2;
			double answer = 0;
			if (formula.Contains(Symbol.plus))
			{
				string[] chars = formula.Split(Symbol.plus);
				double.TryParse(chars[0], out num1);
				double.TryParse(chars[1], out num2);
				answer = num1 + num2;
			}
			else if (formula.Contains(Symbol.subtract))
			{
				string[] chars = formula.Split(Symbol.subtract);
				double.TryParse(chars[0], out num1);
				double.TryParse(chars[1], out num2);
				answer = num1 - num2;
			}
			else if (formula.Contains(Symbol.multiply))
			{
				string[] chars = formula.Split(Symbol.multiply);
				double.TryParse(chars[0], out num1);
				double.TryParse(chars[1], out num2);
				answer = num1 * num2;
			}
			else if (formula.Contains(Symbol.divide))
			{
				string[] chars = formula.Split(Symbol.divide);
				double.TryParse(chars[0], out num1);
				double.TryParse(chars[1], out num2);
				answer = num1 / num2;
			}

			return answer.ToString("N0");
		}
	}
}
