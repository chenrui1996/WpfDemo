using System.Windows.Controls;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : UserControl
    {
        public Index()
        {
            InitializeComponent();
            DataContext = new IndexViewModel();
        }
    }
}
