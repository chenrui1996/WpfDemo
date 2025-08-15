using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfDemo.ViewModels
{
    /// <summary>
    /// No WPF project is complete without it's own version of this.
    /// </summary>
    public class RelayCommandImplementation : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;

        public RelayCommandImplementation(Action<object?> execute)
            : this(execute, null) { }

        public RelayCommandImplementation(Action<object?> execute, Func<object?, bool>? canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object? parameter) => _canExecute(parameter);

        public void Execute(object? parameter) => _execute(parameter);

        /// <summary>\
        /// Button、MenuItem 等控件在绑定 Command 时，会：订阅ICommand.CanExecuteChanged 事件（调用上面的 add 语句）
        /// 实际上就是把 刷新逻辑订阅到了 CommandManager.RequerySuggested
        /// CommandManager 会在这些时机触发 RequerySuggested：
        /// - UI 有输入焦点变化
        /// - 鼠标键盘事件后
        /// - 你手动调用 CommandManager.InvalidateRequerySuggested [Refresh()]
        /// 触发后，WPF 会自动遍历所有订阅 RequerySuggested 的事件处理器，调用它们，从而触发控件重新执行 CanExecute。
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public abstract class ViewModelBase : ObservableValidator { }

}
