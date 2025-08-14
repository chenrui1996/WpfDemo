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
                MessageBox.Show("ͬ���ѵ��");
            });

            CustomCommand2 = new AsyncRelayCommand(async () =>
            {
                await Task.Delay(2000);
                await Task.Run(() =>
                {
                    MessageBox.Show($"�첽��ť�ѵ��");
                });
            });

            //Age > 0 ʱ����ִ��
            MinusCommand = new RelayCommand(() => Age--);
            //Age < 20 ʱ����ִ��
            PlusCommand = new RelayCommand(() => Age++);

            //Age > 0 ʱ����ִ��
            MinusCommand1 = new RelayCommand(() => Age1--, () => Age1 > 0);
            //Age < 20 ʱ����ִ��
            PlusCommand1 = new RelayCommand(() => Age1++, () => Age1 < 20);

            //Age > 0 ʱ����ִ��, �����������ð�ť
            MinusCommand2 = new RelayCommand(() => Age2--, () => Age2 > 0);
            //Age < 20 ʱ����ִ��, �����������ð�ť
            PlusCommand2 = new RelayCommand(() => Age2++, () => Age2 < 20);

            //Age > 0 ʱ����ִ��, �����������ð�ť
            MinusCommand3 = new RelayCommandImplementation(_ => Age3--, _ => Age3 > 0);
            //Age < 20 ʱ����ִ��, �����������ð�ť
            PlusCommand3 = new RelayCommandImplementation(_ => Age3++, _ => Age3 < 20);

            //�̶�����
            TestParaCommand = new RelayCommand<string>(TestPara);
            //��������
            TestParaCommand1 = new RelayCommand<string>(TestPara1);
            //�Ը�����
            TestParaCommand2 = new RelayCommand<(string Name, string Age)>(TestPara2);
            //�������
            TestParaCommand3 = new RelayCommand<User>(TestPara3);
        }

        public ICommand CustomCommand { get; }
        public ICommand CustomCommand2 { get; }

        [ObservableProperty]
        private int age = 0;
        public ICommand MinusCommand { get; }
        public ICommand PlusCommand { get; }

        /// <summary>
        /// ����֪ͨ������ؼ�����״̬�޷�����
        /// </summary>
        [ObservableProperty]
        private int age1 = 0;
        public ICommand MinusCommand1 { get; }
        public ICommand PlusCommand1 { get; }

        /// <summary>
        /// ֪ͨ������ؼ�������Age�仯ʱ����
        /// </summary>
        private int _age2 = 0;
        public int Age2
        {
            get => _age2;
            set
            {
                SetProperty(ref _age2, value);
                //֪ͨ���
                ((RelayCommand)MinusCommand2).NotifyCanExecuteChanged();
                ((RelayCommand)PlusCommand2).NotifyCanExecuteChanged();
            }
        }
        public ICommand MinusCommand2 { get; }
        public ICommand PlusCommand2 { get; }

        /// <summary>
        /// ʹ�÷�װ��RelayCommandImplementation��ע��CanExecuteChanged�¼���֪ͨ������ؼ�������Age�仯ʱ����
        /// </summary>
        [ObservableProperty]
        private int age3 = 0;
        public ICommand MinusCommand3 { get; }
        public ICommand PlusCommand3 { get; }

        public ICommand TestParaCommand { get; }

        private void TestPara(string? para)
        {
            MessageBox.Show($"�̶�������{para}");
        }

        public ICommand TestParaCommand1 { get; }

        private void TestPara1(string? para)
        {
            MessageBox.Show($"����������{para}");
        }

        public ICommand TestParaCommand2 { get; }

        private void TestPara2((string Name, string Age) param)
        {
            MessageBox.Show($"���������������: {param.Name}, ����: {param.Age}");
        }

        [ObservableProperty]
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
        public ICommand TestParaCommand3 { get; }

        private void TestPara3(User? param)
        {
            MessageBox.Show($"���������{param?.ToString() ?? ""}");
        }
    }
}
