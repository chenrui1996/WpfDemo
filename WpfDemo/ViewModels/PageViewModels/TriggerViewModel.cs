using System.Collections.ObjectModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class TriggerViewModel : ViewModelBase
    {
        private TriggerService _service;

        public TriggerViewModel(TriggerService service)
        {
            _service = service;
            CustomCommand = new AnotherCommandImplementation(_ => { });
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

        /// <summary>
        /// 响应式属性
        /// </summary>
        private bool _isOnline;
        public bool IsOnline
        {
            get => _isOnline;
            set => SetProperty(ref _isOnline, value);
        }

        /// <summary>
        /// 响应式属性
        /// </summary>
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private bool _buttonEnabled;

        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set => SetProperty(ref _buttonEnabled, value);
        }
    }
}
