using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// ����ʵ�� INotifyPropertyChanged�������޸�����ֵʱ��Ч
    /// </summary>
    [EnableSelection]
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private string? name;

        [GridColumn(IsEditable = true, Label = "����")]
        public string? Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                    UserStr = ToString();
                }
            }
        }

        private int age;

        [GridColumn(IsEditable = true, Label = "����")]
        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                    UserStr = ToString();
                }
            }
        }

        [GridColumn(IsVisible = false)]
        public Array GenderList => Enum.GetValues(typeof(Gender));

        private Gender gender;

        [GridColumn(IsEditable = true, Label = "�Ա�")]
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
        /// ���userû��ʵ�� INotifyPropertyChanged����ֻ���滻userʱ�Ż����ҳ��
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
                Name = "����",
                Age = 16,
                Gender = Gender.Male,
            },
            new User
            {
                Name = "����",
                Age = 17,
                Gender = Gender.Female,
            },
            new User
            {
                Name = "����",
                Age = 18,
                Gender = Gender.Male,
            },
            new User
            {
                Name = "����",
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
