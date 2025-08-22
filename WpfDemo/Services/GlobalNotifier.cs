using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using WpfDemo.CustomControl;

namespace WpfDemo.Services
{
    public static class GlobalNotifier
    {
        public static SnackbarMessageQueue MessageQueue { get; } =
            new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

        public static void ShowSuccess(string msg, object? actionContent, Action? actionHandler) =>
            Show(msg, actionContent, actionHandler, Brushes.DarkGreen);

        public static void ShowError(string msg, object? actionContent, Action? actionHandler) =>
            Show(msg, actionContent, actionHandler, Brushes.DarkRed);

        public static void ShowInfo(string msg, object? actionContent, Action? actionHandler) =>
            Show(
                msg,
                actionContent,
                actionHandler,
                new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#424242") }
            );

        private static void Show(
            string msg,
            object? actionContent,
            Action? actionHandler,
            Brush background
        )
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (
                    App.Current.MainWindow.FindName("GlobalSnackbar") is Snackbar snackbar
                    && snackbar != null
                    && snackbar.MessageQueue != null
                )
                {
                    snackbar.Background = background;
                    snackbar.MessageQueue.Enqueue(msg, actionContent, actionHandler);
                }
            });
        }

        public static async Task ShowSuccessDialog(string msg, string header) =>
            await ShowDialog(msg, header, Brushes.DarkGreen);

        public static async Task ShowErrorDialog(string msg, string header) =>
            await ShowDialog(msg, header, Brushes.DarkRed);

        public static async Task ShowInfoDialog(string msg, string header) =>
            await ShowDialog(
                msg,
                header,
                new SolidColorBrush() { Color = (Color)ColorConverter.ConvertFromString("#424242") }
            );

        public static async Task ShowDialog(string msg, string header) =>
            await ShowDialog(msg, header, null);

        private static async Task ShowDialog(string msg, string header, Brush? background)
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                var messageDialog = new CustomMessageDialog(header, background)
                {
                    Message = { Text = msg },
                };

                await DialogHost.Show(messageDialog, "RootDialog");
            });
        }

        public static async Task ShowForm(object formModel, ICommand command)
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                var form = new CustomFormDialog { FormModel = formModel, ConfirmCommand = command };
                await DialogHost.Show(form, "RootDialog");
            });
        }
    }
}
