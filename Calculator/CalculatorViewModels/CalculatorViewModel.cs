using System;
using System.Text.RegularExpressions;
using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;

namespace CalculatorViewModels
{
    public class CalculatorViewModel : BindableBase , IDisposable
    {
        #region �t�B�[���h�E�v���p�e�B
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        private static readonly char[] mathSymbols = new char[] { '+', '-', '�~', '��' };

        public ReactivePropertySlim<string> Display { get; set; } = new ReactivePropertySlim<string>("0");
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("�d��A�v��");

        public ReactiveCommand CommandButtonEqual { get; set; } = new ReactiveCommand();
        public ReactiveCommand<string> CommandButtonNum { get; set; } = new ReactiveCommand<string>();
        public ReactiveCommand<string> CommandButtonMathSymbol { get; set; } = new ReactiveCommand<string>();

        #endregion

        #region �R���X�g���N�^
        public CalculatorViewModel()
        {
            _ = this.CommandButtonEqual.Subscribe(this.OnCommandButtonEqual).AddTo(this.disposables);
            _ = this.CommandButtonNum.Subscribe(this.OnCommandButtonNum).AddTo(this.disposables);
            _ = this.CommandButtonMathSymbol.Subscribe(this.OnCommandButtonMathSymbol).AddTo(this.disposables);
        }

        #endregion

        #region private���\�b�h
        private void OnCommandButtonEqual()
        {
            //MessageBox.Show("\"=\"��������܂���");
            this.Test.Value = Guid.NewGuid().ToString();
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
            // �s���ȃp�����[�^�̏ꍇ�͉������Ȃ�
            if ((param is not string mathSymbolStr))
            {
                return;
            }

            var mathSymbol = mathSymbolStr.ToCharArray().FirstOrDefault();

            // �ΏۊO�̌v�Z�L���̏ꍇ�͉������Ȃ�
            if (!mathSymbols.Contains(mathSymbol))
            {
                return;
            }

            // ���łɌv�Z�L�������݂��Ă���ꍇ�͉������Ȃ�(����2�̌v�Z��O��)
            if(mathSymbols.Any(m => this.Display.Value.Contains(m)))
            {
                return;
            }

            var displayText = this.Display.Value;

            // �v�Z�L�����d�Ȃ�Ȃ��悤�ɁA�������v�Z�L���̏ꍇ�͏㏑������
            if (mathSymbols.Contains(displayText.Last()))
            {
                displayText = displayText.TrimEnd(mathSymbols) + mathSymbol;
                this.Display.Value = displayText;
                return;
            }

            // �����Ɍv�Z�L����ǉ�����
            this.Display.Value += mathSymbol;
        }
        

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
