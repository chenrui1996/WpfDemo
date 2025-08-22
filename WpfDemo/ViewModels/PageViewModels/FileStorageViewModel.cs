using System.Collections.ObjectModel;
using System.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class FileStorageViewModel(FileStorageService service) : ViewModelBase
    {
        private readonly FileStorageService _service = service;

        [ObservableProperty]
        private AppConfig? appConfigRead;

        [RelayCommand]
        private void ReadAppConfig()
        {
            AppConfigWrite = AppConfigRead = ConfigHelper.LoadConfig();
        }

        [ObservableProperty]
        private AppConfig? appConfigWrite;

        [RelayCommand]
        private void WriteAppConfig()
        {
            ConfigHelper.SaveConfig(AppConfigWrite);
        }
    }
}
