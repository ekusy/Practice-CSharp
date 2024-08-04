using System.Windows.Controls;

namespace PracticeViews.DropDownCalc
{
    /// <summary>
    /// DropDownCalcView.xaml の相互作用ロジック
    /// </summary>
    public partial class DropDownCalcView : UserControl
    {
        public DropDownCalcView()
        {
            this.InitializeComponent();

            for (var i = 0; i <= 10; i++)
            {
                //this.UnitPriceComboBox.Items.Add(i.ToString());
                this.AmountComboBox.Items.Add(i.ToString());
            }
        }
    }
}
