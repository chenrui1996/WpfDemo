using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public partial class CommandViewModel : ViewModelBase
    {
        private CommandService _service;

        public CommandViewModel(CommandService service)
        {
            _service = service;
            CustomCommand = new RelayCommand(() =>
            {
                MessageBox.Show("同步已点击");
            });

            CustomCommand2 = new AsyncRelayCommand(async () =>
            {
                await Task.Delay(2000);
                await Task.Run(() =>
                {
                    MessageBox.Show($"异步按钮已点击");
                });
            });

            //Age > 0 时可以执行
            MinusCommand = new RelayCommand(() => Age--, () => Age > 0);
            //Age < 20 时可以执行
            PlusCommand = new RelayCommand(() => Age++, () => Age < 20);

            //Age > 0 时可以执行, 其他条件禁用按钮
            MinusCommand1 = new RelayCommand(() => Age1--, () => Age1 > 0);
            //Age < 20 时可以执行, 其他条件禁用按钮
            PlusCommand1 = new RelayCommand(() => Age1++, () => Age1 < 20);

            //Age > 0 时可以执行, 其他条件禁用按钮
            MinusCommand2 = new RelayCommandImplementation(_ => Age2--, _ => Age2 > 0);
            //Age < 20 时可以执行, 其他条件禁用按钮
            PlusCommand2 = new RelayCommandImplementation(_ => Age2++, _ => Age2 < 20);

            //固定参数
            TestParaCommand = new RelayCommand<string>(TestPara);
            //单个参数
            TestParaCommand1 = new RelayCommand<string>(TestPara1);
            //对个参数
            TestParaCommand2 = new RelayCommand<(string Name, string Age)>(TestPara2);
            //对象参数
            TestParaCommand3 = new RelayCommand<User>(TestPara3);
        }

        public ICommand CustomCommand { get; }
        public ICommand CustomCommand2 { get; }

        /// <summary>
        /// 不会通知变更，控件禁用状态无法更新
        /// </summary>
        [ObservableProperty]
        private int age = 0;
        public ICommand MinusCommand { get; }
        public ICommand PlusCommand { get; }

        /// <summary>
        /// 通知变更，控件禁用在Age变化时更新
        /// </summary>
        private int _age1 = 0;
        public int Age1
        {
            get => _age1;
            set
            {
                SetProperty(ref _age1, value);
                //通知变更
                ((RelayCommand)MinusCommand1).NotifyCanExecuteChanged();
                ((RelayCommand)PlusCommand1).NotifyCanExecuteChanged();
            }
        }
        public ICommand MinusCommand1 { get; }
        public ICommand PlusCommand1 { get; }

        /// <summary>
        /// 使用封装的RelayCommandImplementation（注册CanExecuteChanged事件）通知变更，控件禁用在Age变化时更新
        /// </summary>
        [ObservableProperty]
        private int age2 = 0;
        public ICommand MinusCommand2 { get; }
        public ICommand PlusCommand2 { get; }

        public ICommand TestParaCommand { get; }

        private void TestPara(string? para)
        {
            MessageBox.Show($"固定参数：{para}");
        }

        public ICommand TestParaCommand1 { get; }

        private void TestPara1(string? para)
        {
            MessageBox.Show($"单个参数：{para}");
        }

        public ICommand TestParaCommand2 { get; }

        private void TestPara2((string Name, string Age) param)
        {
            MessageBox.Show($"多个个参数：姓名: {param.Name}, 年龄: {param.Age}");
        }

        [ObservableProperty]
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
        public ICommand TestParaCommand3 { get; }

        private void TestPara3(User? param)
        {
            MessageBox.Show($"对象参数：{param?.ToString() ?? ""}");
        }
    }
}
