using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// EventHandle.xaml 的交互逻辑
    /// </EventHandle>
    public partial class EventHandle : UserControl
    {
        public EventHandle()
        {
            InitializeComponent();
        }

        // Button 自身的 Click 事件
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "按钮触发了 Click 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary.Dark") as Brush,
                    Foreground = Brushes.White,
                }
            );
            if (ButtonCheckBox.IsChecked ?? false)
            {
                e.Handled = true;
            }
        }

        private void StackPanel_Parent_ButtonClick(object sender, RoutedEventArgs e)
        {
            clickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "父控件触发了 Click 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary") as Brush,
                    Foreground = Brushes.White,
                }
            );
            if (ParentCheckBox.IsChecked ?? false)
            {
                e.Handled = true;
            }
        }

        private void StackPanel_GrandParent_ButtonClick(object sender, RoutedEventArgs e)
        {
            clickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "祖父控件触发了 Click 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary.Light") as Brush,
                    Foreground = Brushes.White,
                }
            );
            if (GrandParentCheckBox.IsChecked ?? false)
            {
                e.Handled = true;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            clickEventRecord.Items.Clear();
        }

        private void Button_Tunneling_Click(object sender, RoutedEventArgs e)
        {
            clickTunnelingEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "按钮触发了 PreviewMouseDown 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary.Dark") as Brush,
                    Foreground = Brushes.White,
                }
            );
            if (ButtonTunnelingCheckBox.IsChecked ?? false)
            {
                e.Handled = true;
            }
        }

        private void StackPanel_Tunneling_Parent_ButtonClick(object sender, RoutedEventArgs e)
        {
            clickTunnelingEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "父控件触发了 PreviewMouseDown 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary") as Brush,
                    Foreground = Brushes.White,
                }
            );
            if (ParentTunnelingCheckBox.IsChecked ?? false)
            {
                e.Handled = true;
            }
        }

        private void StackPanel_Tunneling_GrandParent_ButtonClick(object sender, RoutedEventArgs e)
        {
            clickTunnelingEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "祖父控件触发了 PreviewMouseDown 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary.Light") as Brush,
                    Foreground = Brushes.White,
                }
            );
            if (GrandParentTunnelingCheckBox.IsChecked ?? false)
            {
                e.Handled = true;
            }
        }

        private void ClearButton_Tunneling_Click(object sender, RoutedEventArgs e)
        {
            clickTunnelingEventRecord.Items.Clear();
        }

        private void MyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OutputTextBlock.Text = "输入：" + MyTextBox.Text;
        }

        private void StackPanel_Custom_Parent_ButtonClick(object sender, RoutedEventArgs e)
        {
            customClickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "父控件触发了 CustomClick 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary") as Brush,
                    Foreground = Brushes.White,
                }
            );
        }

        private void MyCustomControl_MyClick(object sender, RoutedEventArgs e)
        {
            customClickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "自定义按钮触发 CustomClick 事件",
                    Background = FindResource("MaterialDesign.Brush.Primary.Dark") as Brush,
                    Foreground = Brushes.White,
                }
            );
        }

        private void ClearButton_CustomClick(object sender, RoutedEventArgs e)
        {
            customClickEventRecord.Items.Clear();
        }
    }
}
