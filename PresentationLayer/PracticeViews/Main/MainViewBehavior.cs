using System.Windows;

namespace PracticeViews.Main
{
    internal class MainViewBehavior
    {
        /// <summary>
        /// 実行中かどうか
        /// </summary>
        public static DependencyProperty IsWorkingProperty = DependencyProperty.RegisterAttached("IsWorking", typeof(bool), typeof(MainViewBehavior), new PropertyMetadata(OnIsWorkingChanged));

        private static void OnIsWorkingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
