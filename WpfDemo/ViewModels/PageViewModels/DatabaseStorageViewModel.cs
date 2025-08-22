using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.ApplicationServices;
using WpfDemo.Services;
using WpfDemo.SqlLite;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class DatabaseStorageViewModel : ViewModelBase
    {
        private readonly DatabaseStorageService _service;

        public DatabaseStorageViewModel(DatabaseStorageService service)
        {
            _service = service;
            Refresh();
        }

        [ObservableProperty]
        private ObservableCollection<UserModel> userModels = [];

        private void Refresh()
        {
            UserModels =
                SqliteHelper
                    .Instance.ExecuteQuery("SELECT * FROM Users;")
                    .ToEntityList<UserModel>()
                    .Select(user =>
                    {
                        user.PropertyChanged += User_PropertyChanged;
                        return user;
                    })
                    .ToObservableCollection() ?? [];
        }

        private void User_PropertyChanged(
            object? sender,
            System.ComponentModel.PropertyChangedEventArgs e
        )
        {
            if (sender is UserModel user)
            {
                SqliteHelper.Instance.ExecuteQuery(
                    $"UPDATE Users SET Name = {user.Name}, Age = {user.Age}, Gender = {user.Gender}"
                );
            }
        }

        [ObservableProperty]
        private ObservableCollection<UserModel> selectedUsers = [];

        [ObservableProperty]
        private UserModel createdModel = new();

        [RelayCommand]
        private void AddUserEntity()
        {
            SqliteHelper.Instance.ExecuteQuery(
                $"INSERT INTO Users (Name, Age, Gender) VALUES (' {CreatedModel.Name}', {CreatedModel.Age}, '{CreatedModel.Gender}');"
            );
            Refresh();
        }

        [RelayCommand]
        private async Task AddUser()
        {
            await GlobalNotifier.ShowForm(CreatedModel, AddUserEntityCommand);
        }

        [RelayCommand]
        private void DeleteUsers()
        {
            if (SelectedUsers.Count > 0)
            {
                SqliteHelper.Instance.ExecuteQuery(
                    string.Join("", SelectedUsers.Select(s => $"DELETE FROM Users WHERE Id = 2;"))
                );
                Refresh();
            }
        }
    }
}
