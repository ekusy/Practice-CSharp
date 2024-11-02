using System.Windows;
using System.Windows.Automation;
using Prism.Mvvm;
using Reactive.Bindings;

namespace CalculatorViewModels
{
    public class CalculatorViewModel : BindableBase
    {
        #region �t�B�[���h�E�v���p�e�B
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("�d��A�v��");
        public ReactivePropertySlim<string> Result { get; set; } = new ReactivePropertySlim<string>("0");

        public ReactiveCommand CommandButtonNumber { get; set; } = new ReactiveCommand();
        public ReactiveCommand CommandButtonSymbol { get; set; } = new ReactiveCommand();
        public ReactiveCommand CommandButtonEqual { get; set; } = new ReactiveCommand();
        public ReactiveCommand CommandButtonClear { get; set; } = new ReactiveCommand();
        private bool isReset;
        #endregion

        #region �R���X�g���N�^
        public CalculatorViewModel()
        {
            _ = this.CommandButtonNumber.Subscribe(this.OnCommandButtonNumber);
            _ = this.CommandButtonSymbol.Subscribe(this.OnCommandButtonSymbol);
            _ = this.CommandButtonEqual.Subscribe(this.OnCommandButtonEqual);
            _ = this.CommandButtonClear.Subscribe(this.OnCommandButtonClear);
        }

        #endregion

        #region private���\�b�h
        
        /// <summary>
        /// ���l�{�^�����N���b�N�C�x���g
        /// </summary>
        /// <param name="value"></param>
        private void OnCommandButtonNumber(object? value)
        {
            if (this.isReset)
            {
                ClearResult();
				this.isReset = false;
            }
            if (this.Result.Value == "0") this.Result.Value = value.ToString();
            else this.Result.Value += value.ToString();
        }

        /// <summary>
        /// �L���{�^�����N���b�N�C�x���g
        /// </summary>
        /// <param name="value"></param>
        private void OnCommandButtonSymbol(object? value)
        {
            isReset = false;
            if (this.Result.Value == "0") return;
            if (Symbol.IsSymbol(this.Result.Value.Substring(this.Result.Value.Length - 1)))
            {
                this.Result.Value = this.Result.Value[..^1];
			} 
            this.Result.Value += value.ToString();
        }

        /// <summary>
        /// =�{�^�����N���b�N�C�x���g
        /// </summary>
        private void OnCommandButtonEqual()
        {
            string formula = this.Result.Value;
            this.Result.Value = Calc.Calculation(this);
            this.isReset = true;
        }

        /// <summary>
        /// �N���A�{�^�����N���b�N�C�x���g
        /// </summary>
        private void OnCommandButtonClear()
        {
            ClearResult();
            this.isReset = false;

		}

        /// <summary>
        /// ���ʃN���A
        /// </summary>
        private void ClearResult()
        {
            this.Result.Value = "0";
        }
		#endregion
	}
}
