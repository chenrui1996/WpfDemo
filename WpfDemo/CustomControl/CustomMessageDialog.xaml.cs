using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace WpfDemo.CustomControl
{
    /// <summary>
    /// Interaction logic for CustomMessageDialog.xaml
    /// </summary>
    public partial class CustomMessageDialog : UserControl
    {
        public CustomMessageDialog()
        {
            InitializeComponent();
            customGroupBox.Header = "提示";
        }

        public CustomMessageDialog(string header, Brush? background)
        {
            InitializeComponent();
            customGroupBox.Header = header ?? "提示";
            if (background != null)
            {
                ColorZoneAssist.SetBackground(customGroupBox, background);
            }
        }
    }
}
