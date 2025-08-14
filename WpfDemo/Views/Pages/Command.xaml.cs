using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// Command.xaml 的交互逻辑
    /// </Command>
    public partial class Command : UserControl
    {
        public Command()
        {
            InitializeComponent();
            CommandBindings.Add(
                new CommandBinding(
                    ApplicationCommands.Open,
                    Open_Executed,
                    (s, e) =>
                    {
                        e.CanExecute = true;
                    }
                )
            );
            CommandBindings.Add(
                new CommandBinding(
                    ApplicationCommands.Save,
                    Save_Executed,
                    (s, e) =>
                    {
                        e.CanExecute = true;
                    }
                )
            );
            CommandBindings.Add(
                new CommandBinding(
                    ApplicationCommands.Print,
                    Print_Executed,
                    (s, e) =>
                    {
                        e.CanExecute = true;
                    }
                )
            );
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "RTF 文件 (*.rtf)|*.rtf|所有文件 (*.*)|*.*",
            };

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using FileStream fs = new(dlg.FileName, FileMode.Open);
                TextRange range = new(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf); // 加载到 RichTextBox
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog
            {
                Filter = "RTF 文件 (*.rtf)|*.rtf|所有文件 (*.*)|*.*",
                DefaultExt = ".rtf", // 默认扩展名
                FileName = "新文档.rtf", // 默认文件名
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), // 初始目录
            };

            var result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                using (FileStream fs = new(dlg.FileName, FileMode.Create))
                {
                    TextRange range = new(
                        mainRTB.Document.ContentStart,
                        mainRTB.Document.ContentEnd
                    );
                    range.Save(fs, DataFormats.Rtf); // 保存为 RTF
                }
                MessageBox.Show($"已保存到 {dlg.FileName}");
            }
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below
                pd.PrintVisual(mainRTB as Visual, "printing as visual");
                pd.PrintDocument(
                    (((IDocumentPaginatorSource)mainRTB.Document).DocumentPaginator),
                    "printing as paginator"
                );
            }
        }
    }
}
