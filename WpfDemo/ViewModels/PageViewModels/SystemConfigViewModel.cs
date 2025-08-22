using System.Collections.ObjectModel;
using System.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class SystemConfigViewModel(SystemConfigService service) : ViewModelBase
    {
        private readonly SystemConfigService _service = service;

        [ObservableProperty]
        private string? appConfigRead;

        [RelayCommand]
        private void ReadAppConfig()
        {
            AppConfigWrite = AppConfigRead = ConfigurationManager.AppSettings["AppSettingTest"];
        }

        [ObservableProperty]
        private string? appConfigWrite;

        [RelayCommand]
        private void WriteAppConfig()
        {
            ConfigurationManager.AppSettings["AppSettingTest"] = AppConfigWrite;
        }

        [ObservableProperty]
        private string? settingConfigRead;

        [RelayCommand]
        private void ReadSetting()
        {
            SettingConfigWrite = SettingConfigRead = Properties.Settings.Default.SettingTest;
        }

        [ObservableProperty]
        private string? settingConfigWrite;

        [RelayCommand]
        private void WriteSetting()
        {
            Properties.Settings.Default.SettingTest = SettingConfigWrite;
            Properties.Settings.Default.Save();
        }
    }
}
