using System.Windows;
using System.Windows.Controls;
using WpfDemo.CustomControl;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// LifecycleHandle.xaml �Ľ����߼�
    /// </LifecycleHandle>
    public partial class LifecycleHandle : UserControl
    {
        public LifecycleHandle()
        {
            InitializeComponent();
        }

        private CustomControl.CustomControl? _customControl { set; get; }

        private async void AddButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (customPanel.Children.Count > 0)
            {
                await GlobalNotifier.ShowErrorDialog("�Ѵ����Զ���ؼ���", "��ʾ");
                return;
            }
            _customControl = new CustomControl.CustomControl(controlEventRecord);
            customPanel.Children.Add(_customControl);
        }

        private async void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (customPanel.Children.Count <= 0)
            {
                await GlobalNotifier.ShowErrorDialog("�������Զ���ؼ���", "��ʾ");
                return;
            }
            customPanel.Children.Remove(_customControl);
            if (_customControl != null)
            {
                _customControl = null;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            controlEventRecord.Items.Clear();
        }
    }
}
