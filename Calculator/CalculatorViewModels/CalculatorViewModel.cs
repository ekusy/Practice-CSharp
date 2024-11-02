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
        #region �t�B�[���h�E�v���p�e�B
        private readonly CompositeDisposable disposables = new CompositeDisposable();

        private bool isReset = false;

        public ReactivePropertySlim<string> Display { get; set; } = new ReactivePropertySlim<string>("0");
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("�d��A�v��");

        public ReactiveCommand CommandButtonEqual { get; set; } = new ReactiveCommand();
        public ReactiveCommand<string> CommandButtonNum { get; set; } = new ReactiveCommand<string>();
        public ReactiveCommand<string> CommandButtonMathSymbol { get; set; } = new ReactiveCommand<string>();
        public ReactiveCommand CommandButtonClear { get; set; } = new ReactiveCommand();

        #endregion

        #region �R���X�g���N�^
        public CalculatorViewModel()
        {
            _ = this.CommandButtonEqual.Subscribe(this.OnCommandButtonEqual).AddTo(this.disposables);
            _ = this.CommandButtonNum.Subscribe(this.OnCommandButtonNum).AddTo(this.disposables);
            _ = this.CommandButtonMathSymbol.Subscribe(this.OnCommandButtonMathSymbol).AddTo(this.disposables);
            _ = this.CommandButtonClear.Subscribe(this.OnCommandButtonClear).AddTo(this.disposables);
        }

        #endregion

        #region private���\�b�h
        private void OnCommandButtonEqual()
        {
            try
            {
                this.Display.Value = Caluculate.Execute(this.Display.Value);

                // �v�Z��ɐ��l����͂�������͗������Z�b�g������
                this.isReset = true;
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

            var displayText = this.Display.Value;

            // �V�K�̓��͂��m�肵���珉�����t���O�𗎂Ƃ�
            if (this.isReset)
            {
                displayText = "0";
                this.isReset = false;
            }

            if (displayText.Equals("0"))
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
            if (!MathDefine.MathSymbols.Contains(mathSymbol))
            {
                return;
            }

            // ���łɌv�Z�L�������݂��Ă���ꍇ�͉������Ȃ�(����2�̌v�Z��O��)
            if (MathDefine.MathSymbols.Any(m => this.Display.Value.Contains(m)))
            {
                MessageBox.Show("�܂�����2�̌v�Z�����ł��܂���","������������",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            // �V�K�̓��͂��m�肵���珉�����t���O�𗎂Ƃ�
            this.isReset = false;

            var displayText = this.Display.Value;
            // �v�Z�L�����d�Ȃ�Ȃ��悤�ɁA�������v�Z�L���̏ꍇ�͏㏑������
            if (MathDefine.MathSymbols.Contains(displayText.Last()))
            {
                displayText = displayText.TrimEnd(MathDefine.MathSymbols) + mathSymbol;
                this.Display.Value = displayText;
                return;
            }

            // �����Ɍv�Z�L����ǉ�����
            this.Display.Value += mathSymbol;
        }

        private void OnCommandButtonClear()
        {
            // �N���A���͏�ɏ������t���O�𗎂Ƃ�
            this.isReset = false;

            this.Display.Value = "0";
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
