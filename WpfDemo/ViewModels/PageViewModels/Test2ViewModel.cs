using System.Collections.ObjectModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class Test2ViewModel : ViewModelBase
    {
        private Test2Service _service;

        public Test2ViewModel(Test2Service service)
        {
            _service = service;
            CustomCommand = new RelayCommandImplementation(_ => { });
        }

        /// <summary>
        /// 指令
        /// </summary>
        public RelayCommandImplementation CustomCommand { get; }

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
    }
}
