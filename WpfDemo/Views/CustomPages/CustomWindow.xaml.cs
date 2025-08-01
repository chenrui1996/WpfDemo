using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDemo.CustomControl;

namespace WpfDemo.Views.CustomPages
{
    /// <summary>
    /// CustomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CustomWindow : Window
    {
        private Page? _customPage1 { set; get; }
        private Page? _customPage2 { set; get; }

        public CustomWindow()
        {
            InitializeComponent();
            customFrame.Navigating += NavigatingHandle;
            //用于网络资源下载时.导航到页面不触发
            customFrame.NavigationProgress += NavigationProgressHandle;
            customFrame.Navigated += NavigatedHandle;
            CreatePages();
        }

        private void CreatePages()
        {
            _customPage1 = new CustomPage1(pageEventRecord);
            //构造方法执行结束后Initialized已经触发，在这里注册不生效。推荐使用OnInitialized
            _customPage1.Initialized += (s, e) =>
            {
                pageEventRecord.Items.Add("[CustomPage1]: Initialized(Event)");
            };
            _customPage1.Loaded += (s, e) =>
            {
                pageEventRecord.Items.Add("[CustomPage1]: Loaded(Event)");
            };
            _customPage1.Unloaded += (s, e) =>
            {
                pageEventRecord.Items.Add("[CustomPage1]: Unloaded(Event)");
            };
            _customPage2 = new CustomPage2(pageEventRecord);
            _customPage2.Initialized += (s, e) =>
            {
                pageEventRecord.Items.Add("[CustomPage2]: Initialized(Event)");
            };
            _customPage2.Loaded += (s, e) =>
            {
                pageEventRecord.Items.Add("[CustomPage2]: Loaded(Event)");
            };
            _customPage2.Unloaded += (s, e) =>
            {
                pageEventRecord.Items.Add("[CustomPage2]: Unloaded(Event)");
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            customFrame.Navigate(_customPage1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            customFrame.Navigate(_customPage2);
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            customFrame.NavigationService.RemoveBackEntry();
            customFrame.Content = null;
            _customPage1 = null;
            _customPage2 = null;
            CreatePages();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            pageEventRecord.Items.Clear();
        }

        private void NavigatingHandle(object sender, NavigatingCancelEventArgs args)
        {
            pageEventRecord?.Items.Add($"[CustomFrame]: Navigating");
        }

        private void NavigationProgressHandle(object sender, NavigationProgressEventArgs args)
        {
            if (args.MaxBytes != 0)
            {
                double percent = (double)args.BytesRead / args.MaxBytes * 100;
                pageEventRecord?.Items.Add($"[CustomFrame]: NavigationProgress, {percent:F0}%");
            }
        }

        private void NavigatedHandle(object sender, NavigationEventArgs args)
        {
            pageEventRecord?.Items.Add($"[CustomFrame]: Navigated");
        }
    }
}
