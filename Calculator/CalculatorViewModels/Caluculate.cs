using System.Text.RegularExpressions;

namespace CalculatorViewModels
{
    internal class Caluculate
    {
        private static readonly Regex regFormula = new Regex(@"(\d+(?:.\d+)?)[\+\-×÷](\d+(?:.\d+)?)+", RegexOptions.Compiled);

        public static string Execute(string formula)
        {
            // 式が不正な場合はArgumentExceptionを出す
            // ここで不正な文字列を弾くので、以降の文字チェックは簡素にしても良いことにする
            CheckFomula(formula);

            // 計算式の中に存在する記号を抽出する
            var mathSymbol = MathDefine.MathSymbols.Where(formula.Contains).FirstOrDefault();

            switch (mathSymbol)
            {
                case MathDefine.add:
                    return Add(formula);
                case MathDefine.sub:
                    return Sub(formula);
                case MathDefine.mul:
                    return Mul(formula);
                case MathDefine.div:
                    return Div(formula);
                default:
                    throw new ArgumentNullException($"{formula}は不正な式です");
            }

        }

        private static void CheckFomula(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                throw new ArgumentNullException($"式が空白です");
            }

            if (!MathDefine.MathSymbols.Any(formula.Contains))
            {
                throw new ArgumentNullException($"計算記号が無い式は不正です");
            }

            if (1 < MathDefine.MathSymbols.Where(formula.Contains).Count())
            {
                throw new ArgumentNullException($"計算記号が複数存在する式は不正です");
            }

            if (!regFormula.IsMatch(formula))
            {
                throw new ArgumentNullException($"{formula}は不正な式です");
            }
        }

        private static string Add(string formula)
        {
            var elems = formula.Split(MathDefine.add);
            _ = double.TryParse(elems[0], out var val1);
            _ = double.TryParse(elems[1], out var val2);
            return (val1 + val2).ToString();
        }
        private static string Sub(string formula)
        {
            var elems = formula.Split(MathDefine.sub);
            _ = double.TryParse(elems[0], out var val1);
            _ = double.TryParse(elems[1], out var val2);
            return (val1 - val2).ToString();
        }
        private static string Mul(string formula)
        {
            var elems = formula.Split(MathDefine.mul);
            _ = double.TryParse(elems[0], out var val1);
            _ = double.TryParse(elems[1], out var val2);
            return (val1 * val2).ToString();
        }
        private static string Div(string formula)
        {
            var elems = formula.Split(MathDefine.div);
            _ = double.TryParse(elems[0], out var val1);
            _ = double.TryParse(elems[1], out var val2);

            if (double.Epsilon > Math.Abs(val2))
            {
                return "ゼロで割ることはできません";
            }

            return (val1 / val2).ToString();
        }
    }
}
