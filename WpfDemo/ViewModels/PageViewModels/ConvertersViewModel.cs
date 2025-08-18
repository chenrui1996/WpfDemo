using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class ConvertersViewModel : ViewModelBase
    {
        private readonly ConvertersService _service;

        public ConvertersViewModel(ConvertersService service)
        {
            _service = service;
            // 订阅内部属性变化
            userModel.PropertyChanged += (_, __) => OnPropertyChanged(nameof(UserModel));
        }

        [ObservableProperty]
        private bool isShow;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        private decimal originalPice = 1;

        [ObservableProperty]
        private decimal discount = 0;

        [ObservableProperty]
        private UserModel userModel = new()
        {
            Name = "Seven Chen",
            Age = 18,
            Gender = Gender.Male,
        };
    }
}
