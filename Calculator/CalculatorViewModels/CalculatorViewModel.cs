using System;
using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;

namespace CalculatorViewModels
{
    public class CalculatorViewModel : BindableBase, IDisposable
    {
        #region フィールド・プロパティ
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        public ReactivePropertySlim<string> Display { get; set; } = new ReactivePropertySlim<string>("0");
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("電卓アプリ");

        public ReactiveCommand CommandButtonEqual { get; set; } = new ReactiveCommand();
        public ReactiveCommand<string> CommandButtonNum { get; set; } = new ReactiveCommand<string>();
        public ReactiveCommand<string> CommandButtonMathSymbol { get; set; } = new ReactiveCommand<string>();
        public ReactiveCommand CommandButtonClear { get; set; } = new ReactiveCommand();

        #endregion

        #region コンストラクタ
        public CalculatorViewModel()
        {
            _ = this.CommandButtonEqual.Subscribe(this.OnCommandButtonEqual).AddTo(this.disposables);
            _ = this.CommandButtonNum.Subscribe(this.OnCommandButtonNum).AddTo(this.disposables);
            _ = this.CommandButtonMathSymbol.Subscribe(this.OnCommandButtonMathSymbol).AddTo(this.disposables);
            _ = this.CommandButtonClear.Subscribe(this.OnCommandButtonClear).AddTo(this.disposables);
        }

        #endregion

        #region privateメソッド
        private void OnCommandButtonEqual()
        {
            try
            {
                this.Display.Value = Caluculate.Execute(this.Display.Value);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnCommandButtonNum(object param)
        {
            if ((param is not string num))
            {
                return;
            }

            if (this.Display.Value.Equals("0"))
            {
                this.Display.Value = num;
                return;
            }

            this.Display.Value += num;
        }

        private void OnCommandButtonMathSymbol(object param)
        {
            // 不正なパラメータの場合は何もしない
            if ((param is not string mathSymbolStr))
            {
                return;
            }

            var mathSymbol = mathSymbolStr.ToCharArray().FirstOrDefault();

            // 対象外の計算記号の場合は何もしない
            if (!MathDefine.MathSymbols.Contains(mathSymbol))
            {
                return;
            }

            // すでに計算記号が存在している場合は何もしない(数字2つの計算を前提)
            if (MathDefine.MathSymbols.Any(m => this.Display.Value.Contains(m)))
            {
                return;
            }

            var displayText = this.Display.Value;
            // 計算記号が重ならないように、末尾が計算記号の場合は上書きする
            if (MathDefine.MathSymbols.Contains(displayText.Last()))
            {
                displayText = displayText.TrimEnd(MathDefine.MathSymbols) + mathSymbol;
                this.Display.Value = displayText;
                return;
            }

            // 末尾に計算記号を追加する
            this.Display.Value += mathSymbol;
        }

        private void OnCommandButtonClear()
        {
            this.Display.Value = "0";
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
