using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ViewModels.CustomControlViewModels
{
    public class CustomDataGridViewModel : ViewModelBase
    {
        public ObservableCollection<object>? Items;
        public ObservableCollection<object>? SlectedItems;
    }
}
