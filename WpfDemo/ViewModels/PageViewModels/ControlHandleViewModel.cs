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
        /// 指令
        /// </summary> 
        public AnotherCommandImplementation CustomCommand { get; }

        /// <summary> 
        /// 响应式属性
        /// </summary> 
        private string? _customProp;
        public string? CustomProp
        {
            get =>   _customProp;
            set =>   SetProperty(ref _customProp, value);
        }

        /// <summary> 
        /// 响应式集合
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
 
