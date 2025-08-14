using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfDemo.Views.Pages
{
    /// <summary>
    /// Command.xaml �Ľ����߼�
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
                Filter = "RTF �ļ� (*.rtf)|*.rtf|�����ļ� (*.*)|*.*",
            };

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using FileStream fs = new(dlg.FileName, FileMode.Open);
                TextRange range = new(mainRTB.Document.ContentStart, mainRTB.Document.ContentEnd);
                range.Load(fs, DataFormats.Rtf); // ���ص� RichTextBox
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog
            {
                Filter = "RTF �ļ� (*.rtf)|*.rtf|�����ļ� (*.*)|*.*",
                DefaultExt = ".rtf", // Ĭ����չ��
                FileName = "���ĵ�.rtf", // Ĭ���ļ���
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), // ��ʼĿ¼
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
                    range.Save(fs, DataFormats.Rtf); // ����Ϊ RTF
                }
                MessageBox.Show($"�ѱ��浽 {dlg.FileName}");
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
