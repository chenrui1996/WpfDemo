using System.Windows;
using System.Windows.Controls;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// Resources.xaml �Ľ����߼�
    /// </Resources>
    public partial class Resources : UserControl
    {
        public Resources()
        {
            InitializeComponent();
        }

        private void SwitchTheme(string theme)
        {
            var dict = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/Themes/{theme}.xaml", UriKind.Absolute),
            };

            // �滻��Դ�ֵ�
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(dict);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchTheme(CustonThemeSwitch.IsChecked ?? false ? "CustomDark" : "CustomLight");
        }
    }
}
