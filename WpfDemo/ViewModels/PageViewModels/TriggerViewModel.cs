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
        /// ָ��
        /// </summary>
        public AnotherCommandImplementation CustomCommand { get; }

        /// <summary>
        /// ��Ӧʽ����
        /// </summary>
        private string? _customProp;
        public string? CustomProp
        {
            get => _customProp;
            set => SetProperty(ref _customProp, value);
        }

        /// <summary>
        /// ��Ӧʽ����
        /// </summary>
        private ObservableCollection<object> _customList = [];

        public ObservableCollection<object> CustomList
        {
            get => _customList;
            set { SetProperty(ref _customList, value); }
        }

        /// <summary>
        /// ��Ӧʽ����
        /// </summary>
        private bool _isOnline;
        public bool IsOnline
        {
            get => _isOnline;
            set => SetProperty(ref _isOnline, value);
        }

        /// <summary>
        /// ��Ӧʽ����
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
