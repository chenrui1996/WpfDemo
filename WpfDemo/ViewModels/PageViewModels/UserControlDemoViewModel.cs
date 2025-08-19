using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class UserControlDemoViewModel(UserControlDemoService service) : ViewModelBase
    {
        private readonly UserControlDemoService _service = service;

        [ObservableProperty]
        private ObservableCollection<UserModel> userModels =
        [
            new UserModel
            {
                Name = "����",
                Age = 16,
                Gender = Gender.Male,
            },
            new UserModel
            {
                Name = "����",
                Age = 17,
                Gender = Gender.Female,
            },
            new UserModel
            {
                Name = "����",
                Age = 18,
                Gender = Gender.Male,
            },
            new UserModel
            {
                Name = "����",
                Age = 19,
                Gender = Gender.Female,
            },
        ];

        [ObservableProperty]
        private ObservableCollection<UserModel> selectedUsers = [];
    }
}
