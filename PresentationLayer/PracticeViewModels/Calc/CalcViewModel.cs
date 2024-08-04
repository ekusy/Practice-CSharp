using System.Reactive.Disposables;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PracticeViewModels.Calc
{
    public class CalcViewModel : BindableBase, IDisposable
    {
        #region フィールド・プロパティ

        #region 定数
        #endregion

        private readonly CompositeDisposable disposables = [];

        #region ReactiveProperty
        /// <summary>単価</summary>
        public ReactivePropertySlim<string> UnitPrice { get; set; } = new ReactivePropertySlim<string>("1000");
        /// <summary>数量</summary>
        public ReactivePropertySlim<string> Piece { get; set; } = new ReactivePropertySlim<string>("10");
        /// <summary>割引率</summary>
        public ReactivePropertySlim<string> DiscountRate { get; set; } = new ReactivePropertySlim<string>("0");
        /// <summary>実行中かどうか</summary>
        public ReactivePropertySlim<bool> IsWorking { get; set; } = new ReactivePropertySlim<bool>();

        /// <summary>計算開始</summary>
        public AsyncReactiveCommand CommandExecute { get; set; } = new AsyncReactiveCommand();

        #endregion

        #endregion

        #region 公開メソッド
        public CalcViewModel()
        {
            _ = this.CommandExecute.Subscribe(this.OnCommandExecute).AddTo(this.disposables);
        }

        public void Dispose()
        {
            this.disposables.Clear();
        }
        #endregion

        #region コールバック

        private async Task OnCommandExecute()
        {
            // asyncに警告を出さないためのおまじない
            await Task.Delay(0);
        }

        #endregion

        #region 非公開メソッド

        private async Task Calc()
        {
            
        }

        #endregion


    }
}
