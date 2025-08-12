using System.Collections.ObjectModel;
using Microsoft.Msagl.Drawing;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class ControlSummaryViewModel : ViewModelBase
    {
        public ControlSummaryViewModel()
        {
            CustomCommand = new RelayCommandImplementation(_ => { });
        }

        /// <summary>
        /// 指令
        /// </summary>
        public RelayCommandImplementation CustomCommand { get; }

        /// <summary>
        /// 响应式属性
        /// </summary>
        private int? _customProp;
        public int? CustomProp
        {
            get => _customProp;
            set => SetProperty(ref _customProp, value);
        }

        /// <summary>
        /// 响应式集合
        /// </summary>
        private ObservableCollection<object>? _customList = [];

        public ObservableCollection<object>? CustomList
        {
            get => _customList;
            set { SetProperty(ref _customList, value); }
        }
    }
}
