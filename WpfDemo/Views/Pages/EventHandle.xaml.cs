using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// EventHandle.xaml �Ľ����߼�
    /// </EventHandle>
    public partial class EventHandle : UserControl
    {
        public EventHandle()
        {
            InitializeComponent();
        }

        // Button ����� Click �¼�
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "��ť������ Click �¼�",
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
                    Content = "���ؼ������� Click �¼�",
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
                    Content = "�游�ؼ������� Click �¼�",
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
                    Content = "��ť������ PreviewMouseDown �¼�",
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
                    Content = "���ؼ������� PreviewMouseDown �¼�",
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
                    Content = "�游�ؼ������� PreviewMouseDown �¼�",
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
            OutputTextBlock.Text = "���룺" + MyTextBox.Text;
        }

        private void StackPanel_Custom_Parent_ButtonClick(object sender, RoutedEventArgs e)
        {
            customClickEventRecord.Items.Add(
                new ListViewItem
                {
                    Content = "���ؼ������� CustomClick �¼�",
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
                    Content = "�Զ��尴ť���� CustomClick �¼�",
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
