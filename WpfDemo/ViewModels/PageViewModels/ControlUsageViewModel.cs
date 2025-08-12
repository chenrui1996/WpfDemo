using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class Person : ObservableObject
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? AvatarUrl { get; set; }
    }

    public class ControlUsageViewModel : ViewModelBase
    {
        private ControlUsageService _service;

        public ControlUsageViewModel(ControlUsageService service)
        {
            _service = service;
            CustomCommand = new RelayCommandImplementation(_ => { });

            People =
            [
                new Person { Name = "张三", Age = 30 },
                new Person { Name = "李四", Age = 25 },
            ];
        }

        /// <summary>
        /// 指令
        /// </summary>
        public RelayCommandImplementation CustomCommand { get; }

        /// <summary>
        /// 响应式属性
        /// </summary>
        private ObservableCollection<Person>? _people;
        public ObservableCollection<Person>? People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        /// <summary>
        /// 响应式集合
        /// </summary>
        private ObservableCollection<object> _customList = [];

        public ObservableCollection<object> CustomList
        {
            get => _customList;
            set { SetProperty(ref _customList, value); }
        }
    }
}
