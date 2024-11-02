using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;

namespace CalculatorViewModels
{
    public class CalculatorViewModel : BindableBase
    {
        #region フィールド・プロパティ
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("電卓アプリ");

        public ReactiveCommand CommandButtonEqual { get; set; } = new ReactiveCommand();
        #endregion

        #region コンストラクタ
        public CalculatorViewModel()
        {
            _ = this.CommandButtonEqual.Subscribe(this.OnCommandButtonEqual);
        }

        #endregion

        #region privateメソッド
        private void OnCommandButtonEqual()
        {
            //MessageBox.Show("\"=\"が押されました");
            this.Test.Value = Guid.NewGuid().ToString();
        }
        #endregion
    }

}
