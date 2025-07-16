using System.Collections.ObjectModel;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class TestViewModel : ViewModelBase
    {
        public TestViewModel()
        {
            CustomCommand = new AnotherCommandImplementation(_ =>  { });
        }

        /// <summary> 
        /// ָ��
        /// </summary> 
        public AnotherCommandImplementation CustomCommand { get; }

        /// <summary> 
        /// ��Ӧʽ����
        /// </summary> 
        private int _customProp;
        public int CustomProp
        {
            get =>   _customProp;
            set =>   SetProperty(ref _customProp, value);
        }

        /// <summary> 
        /// ��Ӧʽ����
        /// </summary> 
        private ObservableCollection<object>  _customList = [];

        public ObservableCollection<object>  CustomList
        {
            get =>  _customList;
            set
            {
                SetProperty(ref _customList, value);
            }
        }
    }
}
 
