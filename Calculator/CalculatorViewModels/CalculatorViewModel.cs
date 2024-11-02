using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;

namespace CalculatorViewModels
{
    public class CalculatorViewModel : BindableBase
    {
        #region �t�B�[���h�E�v���p�e�B
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("�d��A�v��");

        public ReactiveCommand CommandButtonEqual { get; set; } = new ReactiveCommand();
        #endregion

        #region �R���X�g���N�^
        public CalculatorViewModel()
        {
            _ = this.CommandButtonEqual.Subscribe(this.OnCommandButtonEqual);
        }

        #endregion

        #region private���\�b�h
        private void OnCommandButtonEqual()
        {
            //MessageBox.Show("\"=\"��������܂���");
            this.Test.Value = Guid.NewGuid().ToString();
        }
        #endregion
    }

}
