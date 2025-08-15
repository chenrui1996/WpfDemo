using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using WpfDemo.Services;
using static WpfDemo.Attributes.CustomDataGridAttributes;

namespace WpfDemo.ViewModels.PageViewModels
{
    [EnableSelection]
    public partial class UserModel: ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "姓名长度必须在 2 到 20 个字符之间")]
        [property: GridColumn(IsEditable = true, Label = "姓名")]
        private string? name;

        [NotifyDataErrorInfo]
        [Range(0, 18, ErrorMessage = "年龄必须在 0 到 18 之间")]
        [property: GridColumn(IsEditable = true, Label = "年龄")]
        [ObservableProperty]
        private int age;

        [property: GridColumn(IsEditable = true, Label = "性别")]
        [ObservableProperty]
        private Gender gender; 
        
        [property: GridColumn(IsVisible = false)]
        public Array GenderList => Enum.GetValues(typeof(Gender));

        public override string? ToString()
        {
            return $"Name:{Name}; Age:{Age}; Gender:{Gender}; ";
        }
    }

    public partial class CommunityToolkitViewModel : ViewModelBase
    {
        private CommunityToolkitService _service;

        public CommunityToolkitViewModel(CommunityToolkitService service)
        {
            _service = service;
        }

        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "姓名长度必须在 2 到 20 个字符之间")]
        [property: GridColumn(IsEditable = true, Label = "姓名")]
        [ObservableProperty]
        private string? name;

        [NotifyDataErrorInfo]
        [Range(0, 18, ErrorMessage = "年龄必须在 0 到 18 之间")]
        [property: GridColumn(IsEditable = true, Label = "年龄")]
        [ObservableProperty]
        private int age;

        [property: GridColumn(IsEditable = true, Label = "性别")]
        [ObservableProperty]
        private Gender gender;

        [ObservableProperty]
        private UserModel userModel = new() { Name = "Seven Chen", Age = 20, Gender = Gender.Male };

        [ObservableProperty]
        private ObservableCollection<UserModel> userModels = [
               new UserModel
                {
                    Name = "张三",
                    Age = 16,
                    Gender = Gender.Male,
                },
                new UserModel
                {
                    Name = "李四",
                    Age = 17,
                    Gender = Gender.Female,
                },
                new UserModel
                {
                    Name = "王五",
                    Age = 18,
                    Gender = Gender.Male,
                },
                new UserModel
                {
                    Name = "刘六",
                    Age = 19,
                    Gender = Gender.Female,
                },
        ];

        [ObservableProperty]
        private ObservableCollection<UserModel> selectedUsers = [];

        [RelayCommand]
        private void Show()
        {
            MessageBox.Show("同步已点击");
        }

        /// <summary>
        /// 自动生成AsyncRelayCommand
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task ShowDelay()
        {
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                MessageBox.Show($"异步按钮已点击");
            });
        }

        [ObservableProperty]
        private int age1 = 0;

        partial void OnAge1Changed(int value)
        {
            // 属性变化时自动刷新 CanExecute
            Minus1Command.NotifyCanExecuteChanged();
            // 属性变化时自动刷新 CanExecute
            PlusCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanMinus1))]
        private void Minus1()
        {
            Age1--;
        }
        private bool CanMinus1() => Age1 > 0;


        [RelayCommand(CanExecute = nameof(CanPlus))]
        private void Plus()
        {
            Age1++;
        }
        private bool CanPlus() => Age1 < 20;


        [RelayCommand]
        private void TestPara(string? para)
        {
            MessageBox.Show($"参数：{para}");
        }
    }
}
