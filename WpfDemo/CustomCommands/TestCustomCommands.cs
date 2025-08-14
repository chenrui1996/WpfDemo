using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfDemo.CustomCommands
{
    public static class TestCustomCommands
    {
        //普通 RoutedCommand（不带 UI 文本）
        public static readonly RoutedCommand SimpleCommand = new(
            "ShowSimpleMessage", // 命令名称
            typeof(TestCustomCommands),
            [
                new KeyGesture(Key.B, ModifierKeys.Control), // Ctrl+B
            ]
        );

        //普通 RoutedCommand（不带 UI 文本）
        public static readonly RoutedCommand ExitCommand = new(
            "ExitCommand", // 命令名称
            typeof(TestCustomCommands),
            [
                new KeyGesture(Key.E, ModifierKeys.Control), // Ctrl+B
            ]
        );

        //普通 RoutedCommand（不带 UI 文本）
        public static readonly RoutedCommand GlobalCommand = new(
            "GlobalCommand", // 命令名称
            typeof(TestCustomCommands),
            [
                new KeyGesture(Key.G, ModifierKeys.Control | ModifierKeys.Alt), // Ctrl+Alt+G
            ]
        );

        //带 UI 文本 + 快捷键的 RoutedUICommand
        public static readonly RoutedUICommand ShowMessageCommand = new(
            "Control + M", // UI（菜单、按钮等绑定时） 显示文本
            "ShowMessage", // 命令名称
            typeof(TestCustomCommands),
            [
                new KeyGesture(Key.M, ModifierKeys.Control), // Ctrl+M
            ]
        );
    }
}
