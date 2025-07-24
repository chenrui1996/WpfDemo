using System.Collections.ObjectModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class TemplateViewModel : ViewModelBase
    {
        private TemplateService _service;

        public TemplateViewModel(TemplateService service)
        {
            _service = service;
            CustomCommand = new AnotherCommandImplementation(_ => { });

            People =
            [
                new Person
                {
                    Name = "Alice",
                    Age = 25,
                    AvatarUrl = "https://picsum.photos/100/100",
                },
                new Person
                {
                    Name = "Bob",
                    Age = 30,
                    AvatarUrl = "https://picsum.photos/101/101",
                },
                new Person
                {
                    Name = "Charlie",
                    Age = 22,
                    AvatarUrl = "https://picsum.photos/102/102",
                },
                new Person
                {
                    Name = "Seven",
                    Age = 22,
                    AvatarUrl = "pack://application:,,,/Asset/logo.png",
                },
            ];
        }

        /// <summary>
        /// 指令
        /// </summary>
        public AnotherCommandImplementation CustomCommand { get; }

        /// <summary>
        /// 响应式属性
        /// </summary>
        private string? _customProp;
        public string? CustomProp
        {
            get => _customProp;
            set => SetProperty(ref _customProp, value);
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

        private ObservableCollection<Person>? _people;
        public ObservableCollection<Person>? People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }
    }
}
