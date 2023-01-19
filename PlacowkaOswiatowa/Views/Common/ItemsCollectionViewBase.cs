using System.Windows;
using System.Windows.Controls;

namespace PlacowkaOswiatowa.Views.Common
{
    public class ItemsCollectionViewBase : UserControl
    {
        static ItemsCollectionViewBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsCollectionViewBase), new FrameworkPropertyMetadata(typeof(ItemsCollectionViewBase)));
        }
    }
}
