using System.Diagnostics.Metrics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using WpfDemo.CustomControl;
using WpfDemo.Services;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// ControlHandle.xaml 的交互逻辑
    /// </ControlHandle>
    public partial class ControlHandle : UserControl
    {
        public ControlHandle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 通过实例(Name)访问
        ///
        /// 类型安全（自动识别控件类型）
        /// 编译时检查
        /// 推荐用于大部分静态控件
        /// </summary>
        private void FindControlWithName(object sender, RoutedEventArgs e)
        {
            MyTextBlock.Text = "Updated With Name!";
            MyTextBlock.Background = Brushes.DarkRed;
        }

        /// <summary>
        /// 通过FindName方法访问(动态查找)
        ///
        /// FindName 只会查找该 XAML 的当前 NameScope（通常是 Window 或 UserControl）
        /// </summary>
        private void FindControlWithFindName(object sender, RoutedEventArgs e)
        {
            var textBlock = (TextBlock)FindName("MyTextBlock");
            if (textBlock != null)
            {
                textBlock.Text = "Updated With FindName!";
                textBlock.Background = Brushes.DarkOrange;
            }
        }

        /// <summary>
        /// 通过逻辑树(LogicalTreeHelper)查找
        ///
        /// 用于在容器控件（如 StackPanel、Grid）中递归查找控件。
        /// </summary>
        private void FindControlWithLogicalTreeHelper(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton)
            {
                return;
            }

            if (clickedButton?.Parent is not FrameworkElement parent)
            {
                return;
            }

            if (parent.Parent is not FrameworkElement grandParent)
            {
                return;
            }

            var textBlock = FindLogicalChild<TextBlock>(grandParent, "MyTextBlock");
            if (textBlock != null)
            {
                MyTextBlock.Text = "Updated With LogicalTreeHelper!";
                MyTextBlock.Background = Brushes.DarkGreen;
            }
        }

        public static T? FindLogicalChild<T>(DependencyObject parent, string childName)
            where T : FrameworkElement
        {
            foreach (var child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is DependencyObject depChild)
                {
                    if (depChild is T frameworkElement && frameworkElement.Name == childName)
                        return frameworkElement;

                    var result = FindLogicalChild<T>(depChild, childName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        public static T? FindLogicalChildMatchWithName<T>(DependencyObject parent, string childName)
            where T : FrameworkElement
        {
            foreach (var child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is DependencyObject depChild)
                {
                    if (depChild is T frameworkElement && frameworkElement.Name.Contains(childName))
                        return frameworkElement;

                    var result = FindLogicalChild<T>(depChild, childName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 通过视觉树(VisualTreeHelper)查找
        ///
        /// 用于查找嵌套较深的控件，如 Template、DataTemplate 中的控件。
        /// </summary>
        private void FindControlWithVisualTreeHelper(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton)
            {
                return;
            }

            if (clickedButton?.Parent is not FrameworkElement parent)
            {
                return;
            }

            if (parent.Parent is not FrameworkElement grandParent)
            {
                return;
            }

            var textBlock = FindVisualChild<TextBlock>(grandParent);
            if (textBlock != null)
            {
                MyTextBlock.Text = "Updated With VisualTreeHelper!";
                MyTextBlock.Background = Brushes.DarkSlateBlue;
            }
        }

        /// <summary>
        /// 返回该类型的第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T? FindVisualChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;

                var result = FindVisualChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private readonly ReaderWriterLockSlim rwLock = new();

        /// <summary>
        /// 通过事件参数(sender)获取控件
        /// </summary>
        private void FindControlWithSender(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton)
            {
                return;
            }
            try
            {
                rwLock.EnterWriteLock(); // 进入写锁
                var temp = (clickedButton.Content, clickedButton.Background);

                clickedButton.Content = "Updated With Sender!";
                clickedButton.Background = Brushes.DarkSeaGreen;
                Task.Run(async () =>
                {
                    for (int i = 8; i >= 0; i--)
                    {
                        await Task.Delay(1000);
                        Dispatcher.Invoke(() =>
                        {
                            clickedButton.Content = $"Updated With Sender! {i} 秒后恢复";
                        });
                    }
                    Dispatcher.Invoke(() =>
                    {
                        clickedButton.Content = temp.Content;
                        clickedButton.Background = temp.Background;
                        rwLock.ExitWriteLock(); // 释放写锁
                    });
                });
            }
            catch (LockRecursionException) { }
            catch (Exception)
            {
                if (rwLock.IsWriteLockHeld)
                    rwLock.ExitWriteLock();
            }
        }

        private int counter = 1;

        private void AddControls_Click(object sender, RoutedEventArgs e)
        {
            // 创建 Label
            Label label = new()
            {
                Name = $"DynamicLabel_{counter}",
                Margin = new Thickness(0, 10, 0, 10),
                Content = $"动态标签 {counter}",
                FontSize = 18,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Black,
            };

            // 创建 TextBox
            TextBox textBox = new()
            {
                Margin = new Thickness(0, 5, 0, 5),
                Name = $"TextBox_{counter}",
                Style = FindResource("MaterialDesignOutlinedTextBox") as System.Windows.Style,
            };

            // 创建 Button
            Button button = new()
            {
                Content = "提交",
                Tag = textBox, // 将 TextBox 绑定到 Button 的 Tag 方便访问
            };

            button.Click += DynamicButton_Click;

            // 添加到 StackPanel
            MainStackPanel.Children.Add(label);
            MainStackPanel.Children.Add(textBox);
            MainStackPanel.Children.Add(button);

            counter++;
        }

        private async void DynamicButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is TextBox txt)
            {
                var tipName = string.Empty;
                if (btn.Parent != null)
                {
                    tipName = FindLogicalChildMatchWithName<Label>(btn.Parent, "DynamicLabel_")
                        ?.Content.ToString();
                }

                await GlobalNotifier.ShowInfoDialog(txt.Text, $"已输入({tipName})");
            }
        }
    }
}
