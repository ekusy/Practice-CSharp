using Prism.Mvvm;
using Reactive.Bindings;

namespace CalculatorViewModels
{
    public class CalculatorViewModel : BindableBase
    {
        public ReactivePropertySlim<string> Test { get; set; } = new ReactivePropertySlim<string>("“d‘ìƒAƒvƒŠ");
    }

}
