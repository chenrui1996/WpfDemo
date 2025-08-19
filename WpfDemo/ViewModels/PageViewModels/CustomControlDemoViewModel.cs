using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfDemo.Models;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class CustomControlDemoViewModel(CustomControlDemoService service)
        : ViewModelBase
    {
        private readonly CustomControlDemoService _service = service;

        [ObservableProperty]
        private ObservableCollection<BreadcrumbItem> _breadcrumb =
        [
            new BreadcrumbItem { Label = "标题1" },
            new BreadcrumbItem { Label = "标题2" },
            new BreadcrumbItem { Label = "标题3" },
            new BreadcrumbItem { Label = "标题4" },
            new BreadcrumbItem { Label = "标题5" },
        ];
    }
}
