using System.Windows;
using System.Windows.Controls;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// ExtendedControlDemo.xaml �Ľ����߼�
    /// </ExtendedControlDemo>
    public partial class ExtendedControlDemo : UserControl
    {
        public ExtendedControlDemo()
        {
            InitializeComponent();
        }

        private async void MyCustomControl_MyClick(object sender, RoutedEventArgs e)
        {
            await GlobalNotifier.ShowInfoDialog("�Ҽ�����", "��ť�¼�");
        }

        private async void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            await GlobalNotifier.ShowInfoDialog("�������", "��ť�¼�");
        }
    }
}
