using System.Collections.ObjectModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class StyleViewModel : ViewModelBase
    {
        private StyleService _service;

        public StyleViewModel(StyleService service)
        {
            _service = service;
            CustomCommand = new RelayCommandImplementation(_ => { });
        }

        /// <summary>
        /// ָ��
        /// </summary>
        public RelayCommandImplementation CustomCommand { get; }

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
    }
}
