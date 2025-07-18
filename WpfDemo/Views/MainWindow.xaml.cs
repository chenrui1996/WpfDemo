using Autofac;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDemo.CustomControl;
using WpfDemo.ViewModels;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IContainer? _container;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Container?.Resolve<MainViewModel>();
        }

        public void GetMainViewModel(IServiceProvider serviceProvide)
        {
            var mainViewModel = serviceProvide.GetService<MainViewModel>();
            DataContext = mainViewModel;
        }

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
            => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }

        private async void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            var messageDialog = new CustomMessageDialog
            {
                Message = { Text = ((ButtonBase)sender).Content.ToString() },
            };

            object? result = await DialogHost.Show(messageDialog, "RootDialog");
        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MainScrollViewer.ScrollToHome();
        }

        private void TreeListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var treeListView = (TreeListView)sender;
            if(treeListView.SelectedItem != null)
                MenuToggleButton.IsChecked = false;
        }
    }
}