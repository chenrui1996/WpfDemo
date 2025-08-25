using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class Test3ViewModel(Test3Service service) : ViewModelBase
    {
         private readonly Test3Service _service = service;

         [RelayCommand]
         private void YourMethod(string? para)
         {
             //TODO
         }

         [ObservableProperty]
         private string? customProp;

         [ObservableProperty]
         private ObservableCollection<object> customList = [];
    }
}
 
