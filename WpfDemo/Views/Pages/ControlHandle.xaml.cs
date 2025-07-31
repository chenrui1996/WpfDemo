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
    /// ControlHandle.xaml �Ľ����߼�
    /// </ControlHandle>
    public partial class ControlHandle : UserControl
    {
        public ControlHandle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ͨ��ʵ��(Name)����
        ///
        /// ���Ͱ�ȫ���Զ�ʶ��ؼ����ͣ�
        /// ����ʱ���
        /// �Ƽ����ڴ󲿷־�̬�ؼ�
        /// </summary>
        private void FindControlWithName(object sender, RoutedEventArgs e)
        {
            MyTextBlock.Text = "Updated With Name!";
            MyTextBlock.Background = Brushes.DarkRed;
        }

        /// <summary>
        /// ͨ��FindName��������(��̬����)
        ///
        /// FindName ֻ����Ҹ� XAML �ĵ�ǰ NameScope��ͨ���� Window �� UserControl��
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
        /// ͨ���߼���(LogicalTreeHelper)����
        ///
        /// �����������ؼ����� StackPanel��Grid���еݹ���ҿؼ���
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
        /// ͨ���Ӿ���(VisualTreeHelper)����
        ///
        /// ���ڲ���Ƕ�׽���Ŀؼ����� Template��DataTemplate �еĿؼ���
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
        /// ���ظ����͵ĵ�һ��
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
        /// ͨ���¼�����(sender)��ȡ�ؼ�
        /// </summary>
        private void FindControlWithSender(object sender, RoutedEventArgs e)
        {
            if (sender is not Button clickedButton)
            {
                return;
            }
            try
            {
                rwLock.EnterWriteLock(); // ����д��
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
                            clickedButton.Content = $"Updated With Sender! {i} ���ָ�";
                        });
                    }
                    Dispatcher.Invoke(() =>
                    {
                        clickedButton.Content = temp.Content;
                        clickedButton.Background = temp.Background;
                        rwLock.ExitWriteLock(); // �ͷ�д��
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
            // ���� Label
            Label label = new()
            {
                Name = $"DynamicLabel_{counter}",
                Margin = new Thickness(0, 10, 0, 10),
                Content = $"��̬��ǩ {counter}",
                FontSize = 18,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Black,
            };

            // ���� TextBox
            TextBox textBox = new()
            {
                Margin = new Thickness(0, 5, 0, 5),
                Name = $"TextBox_{counter}",
                Style = FindResource("MaterialDesignOutlinedTextBox") as System.Windows.Style,
            };

            // ���� Button
            Button button = new()
            {
                Content = "�ύ",
                Tag = textBox, // �� TextBox �󶨵� Button �� Tag �������
            };

            button.Click += DynamicButton_Click;

            // ��ӵ� StackPanel
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

                await GlobalNotifier.ShowInfoDialog(txt.Text, $"������({tipName})");
            }
        }
    }
}
