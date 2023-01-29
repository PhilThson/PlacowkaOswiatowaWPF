using System.Windows;
using System.Windows.Controls;

namespace PlacowkaOswiatowa.Views.Common
{
    public class OneToManyViewBase : UserControl
    {
        static OneToManyViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(OneToManyViewBase), new FrameworkPropertyMetadata(typeof(OneToManyViewBase)));
        }
    }
}
