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
        [Required(ErrorMessage = "��������Ϊ��")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "�������ȱ����� 2 �� 20 ���ַ�֮��")]
        [property: GridColumn(IsEditable = true, Label = "����")]
        private string? name;

        [NotifyDataErrorInfo]
        [Range(0, 18, ErrorMessage = "��������� 0 �� 18 ֮��")]
        [property: GridColumn(IsEditable = true, Label = "����")]
        [ObservableProperty]
        private int age;

        [property: GridColumn(IsEditable = true, Label = "�Ա�")]
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
        [Required(ErrorMessage = "��������Ϊ��")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "�������ȱ����� 2 �� 20 ���ַ�֮��")]
        [property: GridColumn(IsEditable = true, Label = "����")]
        [ObservableProperty]
        private string? name;

        [NotifyDataErrorInfo]
        [Range(0, 18, ErrorMessage = "��������� 0 �� 18 ֮��")]
        [property: GridColumn(IsEditable = true, Label = "����")]
        [ObservableProperty]
        private int age;

        [property: GridColumn(IsEditable = true, Label = "�Ա�")]
        [ObservableProperty]
        private Gender gender;

        [ObservableProperty]
        private UserModel userModel = new() { Name = "Seven Chen", Age = 20, Gender = Gender.Male };

        [ObservableProperty]
        private ObservableCollection<UserModel> userModels = [
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

        [RelayCommand]
        private void Show()
        {
            MessageBox.Show("ͬ���ѵ��");
        }

        /// <summary>
        /// �Զ�����AsyncRelayCommand
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task ShowDelay()
        {
            await Task.Delay(2000);
            await Task.Run(() =>
            {
                MessageBox.Show($"�첽��ť�ѵ��");
            });
        }

        [ObservableProperty]
        private int age1 = 0;

        partial void OnAge1Changed(int value)
        {
            // ���Ա仯ʱ�Զ�ˢ�� CanExecute
            Minus1Command.NotifyCanExecuteChanged();
            // ���Ա仯ʱ�Զ�ˢ�� CanExecute
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
            MessageBox.Show($"������{para}");
        }
    }
}
