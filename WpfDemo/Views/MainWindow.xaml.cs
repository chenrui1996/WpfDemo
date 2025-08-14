using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Autofac;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using WpfDemo.CustomControl;
using WpfDemo.ViewModels;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Container?.Resolve<MainViewModel>();
        }

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e) =>
            ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (
                MessageBox.Show("确认退出？", "退出", MessageBoxButton.OKCancel)
                == MessageBoxResult.OK
            )
            {
                Application.Current.Shutdown();
            }
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainScrollViewer.ScrollToHome();
        }

        private void TreeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var treeListView = (TreeListView)sender;
            if (treeListView.SelectedItem != null)
                MenuToggleButton.IsChecked = false;
        }

        // RoutedCommand 逻辑

        private void SimpleCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("RoutedCommand 执行了！");
        }

        private void SimpleCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // 启用命令
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (
                MessageBox.Show("确认退出？", "退出", MessageBoxButton.OKCancel)
                == MessageBoxResult.OK
            )
            {
                Application.Current.Shutdown();
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // 启用命令
        }

        // RoutedUICommand 逻辑
        private void ShowMessageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("RoutedUICommand 执行了！（可用 Ctrl+M 快捷键）");
        }

        private void ShowMessageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true; // 启用命令
        }

        //Win32 API 注册全局热键
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const uint MOD_ALT = 0x0001; // Alt
        const uint MOD_CONTROL = 0x0002; // Ctrl
        const uint MOD_SHIFT = 0x0004; // SHIFT
        const int HOTKEY_ID = 19000;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            RegisterHotKey(
                helper.Handle,
                HOTKEY_ID,
                MOD_CONTROL | MOD_ALT,
                (uint)System.Windows.Forms.Keys.G
            ); // Ctrl+Alt+G

            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            source.AddHook(HwndHook);
        }

        private IntPtr HwndHook(
            IntPtr hwnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam,
            ref bool handled
        )
        {
            const int WM_HOTKEY = 0x0312;
            if (msg == WM_HOTKEY && wParam.ToInt32() == HOTKEY_ID)
            {
                MessageBox.Show("全局热键触发，窗口即使最小化也能执行");
                handled = true;
            }
            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
            base.OnClosed(e);
        }
    }
}
