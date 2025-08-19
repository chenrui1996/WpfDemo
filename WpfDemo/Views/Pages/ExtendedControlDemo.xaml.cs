using System.Windows;
using System.Windows.Controls;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// ExtendedControlDemo.xaml 的交互逻辑
    /// </ExtendedControlDemo>
    public partial class ExtendedControlDemo : UserControl
    {
        public ExtendedControlDemo()
        {
            InitializeComponent();
        }

        private async void MyCustomControl_MyClick(object sender, RoutedEventArgs e)
        {
            await GlobalNotifier.ShowInfoDialog("右键触发", "按钮事件");
        }

        private async void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            await GlobalNotifier.ShowInfoDialog("左键触发", "按钮事件");
        }
    }
}
