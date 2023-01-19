using System.Windows;
using System.Windows.Controls;

namespace PlacowkaOswiatowa.Views.Common
{
    public class SingleItemViewBase : UserControl
    {
        static SingleItemViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SingleItemViewBase), new FrameworkPropertyMetadata(typeof(SingleItemViewBase)));
        }
    }
}
