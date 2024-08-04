using Reactive.Bindings;

namespace PracticeViewModels.Calc
{
    public class DropDownCalcViewModel : CalcViewModel
    {
        #region フィールド・プロパティ

        #region ReactiveProperty
        public ReactiveCollection<string> UnitPriceList { get; set; } = new ReactiveCollection<string>()
        {
            "0",
            "1"
        };
        #endregion

        #endregion

        public DropDownCalcViewModel() : base()
        {

        }

        protected override void Calc()
        {
            base.Calc();

            this.UnitPriceList.Add((this.UnitPriceList.Count + 1).ToString());

            if (this.UnitPriceList.Count > 5)
            {
                this.UnitPriceList.Clear();
                this.UnitPriceList.AddRangeOnScheduler(new string[] { "0", "1" });
            }
        }
    }
}
