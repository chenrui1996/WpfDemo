using System.Windows;
using System.Windows.Controls;
using WpfDemo.CustomControl;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;
using WpfDemo.Views.CustomPages;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// LifecycleHandle.xaml 的交互逻辑
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
                await GlobalNotifier.ShowErrorDialog("已存在自定义控件！", "提示");
                return;
            }
            _customControl = new CustomControl.CustomControl(controlEventRecord);
            _customControl.Loaded += (s, e) =>
            {
                controlEventRecord.Items.Add("Loaded(Event)");
            };
            _customControl.Unloaded += (s, e) =>
            {
                controlEventRecord.Items.Add("Unloaded(Event)");
            };
            customPanel.Children.Add(_customControl);
        }

        private async void RemoveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (customPanel.Children.Count <= 0)
            {
                await GlobalNotifier.ShowErrorDialog("不存在自定义控件！", "提示");
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

        private void ShowCustomWinButton_Click(object sender, RoutedEventArgs e)
        {
            new CustomWindow().Show();
        }
    }
}
