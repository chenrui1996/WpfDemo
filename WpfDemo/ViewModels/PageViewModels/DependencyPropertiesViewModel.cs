using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfDemo.Services;
using static WpfDemo.Attributes.CustomDataGridAttributes;

namespace WpfDemo.ViewModels.PageViewModels
{
    public enum Gender
    {
        Male,
        Female,
        Undefined,
    }

    /// <summary>
    /// 必须实现 INotifyPropertyChanged，否则修改属性值时无效
    /// </summary>
    [EnableSelection]
    public class User : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string? name;

        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "姓名长度必须在 2 到 20 个字符之间")]
        [GridColumn(IsEditable = true, Label = "姓名")]
        public string? Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                    ValidateProperty(value, nameof(Name));
                    UserStr = ToString();
                }
            }
        }

        private int age;

        [GridColumn(IsEditable = true, Label = "年龄")]
        [Range(0, 18, ErrorMessage = "年龄必须在 0 到 120 之间")]
        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                    ValidateProperty(value, nameof(Age));
                    UserStr = ToString();
                }
            }
        }

        [GridColumn(IsVisible = false)]
        public Array GenderList => Enum.GetValues(typeof(Gender));

        private Gender gender;

        [GridColumn(IsEditable = true, Label = "性别")]
        public Gender Gender
        {
            get => gender;
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged(nameof(Gender));
                    UserStr = ToString();
                }
            }
        }

        public override string? ToString()
        {
            return $"Name:{Name}; Age:{Age}; Gender:{Gender}; ";
        }

        private string? userStr;

        [GridColumn(IsVisible = false)]
        public string? UserStr
        {
            get => userStr;
            set
            {
                if (userStr != value)
                {
                    userStr = value;
                    OnPropertyChanged(nameof(userStr));
                }
            }
        }

        /// <summary>
        /// 可以放在ModelBase中
        /// 1. 需要实现INotifyDataErrorInfo接口
        /// 2. 需要View Binding Name, ValidatesOnDataErrors=True
        ///
        /// 过程：
        ///
        /// 用户输入 --> TextBox.Text --> Binding --> ViewModel 属性 set
        ///     ↓
        /// 执行验证逻辑
        ///     ↓
        /// 错误字典更新(_errors[propertyName] = [...])
        ///     ↓
        /// 触发 ErrorsChanged 事件
        ///     ↓
        /// WPF Binding 收到通知，调用 GetErrors(propertyName)
        ///     ↓
        /// Validation.Errors 更新 (控件默认含有 Validation.ErrorTemplate)
        ///     ↓
        /// UI(MaterialDesign) 显示或清除错误提示
        /// ValidationAssist.UsePopup="True" → 气泡提示（错误显示在控件旁边）
        /// ValidationAssist.UsePopup="False"（默认）→ 错误显示在控件下方
        ///
        /// </summary>
        #region Validation
        private readonly Dictionary<string, List<string>> _errors = [];

        public bool HasErrors => _errors.Count != 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return new List<string>();
            return _errors.TryGetValue(propertyName, out List<string>? value) ? value : [];
        }

        private void ValidateProperty(object? value, string propertyName)
        {
            ClearErrors(propertyName);

            var context = new ValidationContext(this) { MemberName = propertyName };
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(value, context, results))
            {
                _errors[propertyName] = results.Select(r => r.ErrorMessage ?? "").ToList();
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName) =>
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

        #endregion
    }

    public class DependencyPropertiesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private DependencyPropertiesService _service;

        public DependencyPropertiesViewModel()
        {
            _service = new DependencyPropertiesService();
        }

        public DependencyPropertiesViewModel(DependencyPropertiesService service)
        {
            _service = service;
        }

        private string? name;
        public string? Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }

        private Gender gender;
        public Gender Gender
        {
            get => gender;
            set
            {
                if (gender != value)
                {
                    gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        public Array GenderList => Enum.GetValues(typeof(Gender));

        private int age;
        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        /// <summary>
        /// 如果user没有实现 INotifyPropertyChanged，则只有替换user时才会更新页面
        /// </summary>
        private User user = new User { Name = "Seven Chen", Age = 20 };
        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private User? selectedUser;
        public User? SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        private ObservableCollection<User> users =
        [
            new User
            {
                Name = "张三",
                Age = 16,
                Gender = Gender.Male,
            },
            new User
            {
                Name = "李四",
                Age = 17,
                Gender = Gender.Female,
            },
            new User
            {
                Name = "王五",
                Age = 18,
                Gender = Gender.Male,
            },
            new User
            {
                Name = "刘六",
                Age = 19,
                Gender = Gender.Female,
            },
        ];

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(users));
            }
        }

        private ObservableCollection<User> selectedUsers = [];

        public ObservableCollection<User> SelectedUsers
        {
            get => selectedUsers;
            set
            {
                selectedUsers = value;
                OnPropertyChanged(nameof(selectedUsers));
            }
        }
    }
}
