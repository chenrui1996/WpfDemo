using System.Collections.ObjectModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class ControlHandleViewModel : ViewModelBase
    {
        private ControlHandleService _service;
        public ControlHandleViewModel(ControlHandleService service)
        {
            _service = service;
            CustomCommand = new AnotherCommandImplementation(_ =>  { });
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
            get =>   _customProp;
            set =>   SetProperty(ref _customProp, value);
        }

        /// <summary> 
        /// ��Ӧʽ����
        /// </summary> 
        private ObservableCollection<object> _customList = [];

        public ObservableCollection<object> CustomList
        {
            get => _customList;
            set
            {
                SetProperty(ref _customList, value);
            }
        }
    }
}
 
