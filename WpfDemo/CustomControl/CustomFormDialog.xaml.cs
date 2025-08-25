using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using WpfDemo.Services;
using static WpfDemo.Attributes.CustomDataGridAttributes;

namespace WpfDemo.CustomControl
{
    /// <summary>
    /// CustomFormDialog.xaml 的交互逻辑
    /// </summary>
    public partial class CustomFormDialog : UserControl
    {
        public CustomFormDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region FormModel 依赖属性
        public object FormModel
        {
            get => GetValue(FormModelProperty);
            set => SetValue(FormModelProperty, value);
        }

        public static readonly DependencyProperty FormModelProperty = DependencyProperty.Register(
            "FormModel",
            typeof(object),
            typeof(CustomFormDialog),
            new PropertyMetadata(null, OnFormModelChanged)
        );

        private static void OnFormModelChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            if (d is CustomFormDialog window && e.NewValue != null)
            {
                window.GenerateForm(e.NewValue);
            }
        }
        #endregion

        #region ConfirmCommand 依赖属性
        public System.Windows.Input.ICommand ConfirmCommand
        {
            get => (System.Windows.Input.ICommand)GetValue(ConfirmCommandProperty);
            set
            {
                if (value == null)
                {
                    SetValue(
                        ConfirmCommandProperty,
                        MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand
                    );
                }
                else
                {
                    // 包装一层，确保外部命令和默认命令都执行
                    SetValue(
                        ConfirmCommandProperty,
                        new RelayCommand<object>(o =>
                        {
                            // 先执行外部命令
                            if (value.CanExecute(o))
                                value.Execute(o);

                            // 再执行默认操作
                            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(
                                null,
                                this
                            );
                        })
                    );
                }
            }
        }

        public static readonly DependencyProperty ConfirmCommandProperty =
            DependencyProperty.Register(
                "ConfirmCommand",
                typeof(System.Windows.Input.ICommand),
                typeof(CustomFormDialog)
            );
        #endregion

        private void GenerateForm(object model)
        {
            FormPanel.Children.Clear();

            var attr = model.GetType().GetCustomAttribute<GridEntityAttribute>();

            customGroupBox.Header = attr?.Label ?? model.GetType().Name;

            var props = model
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<GridColumnAttribute>()?.IsAddable == true);

            foreach (var prop in props)
            {
                var propAttr = prop.GetCustomAttribute<GridColumnAttribute>();

                var label = new TextBlock
                {
                    Text = propAttr?.Label ?? prop.Name,
                    Margin = new Thickness(0, 5, 0, 2),
                };
                FormPanel.Children.Add(label);

                if (prop.PropertyType == typeof(bool))
                {
                    var cb = new CheckBox();
                    cb.SetBinding(
                        CheckBox.IsCheckedProperty,
                        new Binding(prop.Name)
                        {
                            Source = model,
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        }
                    );
                    FormPanel.Children.Add(cb);
                    continue;
                }

                if (prop.PropertyType == typeof(int))
                {
                    var cb = new MaterialDesignThemes.Wpf.DecimalUpDown();
                    cb.SetBinding(
                        MaterialDesignThemes.Wpf.DecimalUpDown.ValueProperty,
                        new Binding(prop.Name)
                        {
                            Source = model,
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        }
                    );
                    FormPanel.Children.Add(cb);
                    continue;
                }

                if (prop.PropertyType.IsEnum)
                {
                    var cb = new ComboBox() { ItemsSource = Enum.GetValues(prop.PropertyType) };
                    cb.SetBinding(
                        ComboBox.SelectedItemProperty,
                        new Binding(prop.Name)
                        {
                            Source = model,
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        }
                    );
                    FormPanel.Children.Add(cb);
                    continue;
                }

                var tb = new TextBox();
                tb.SetBinding(
                    TextBox.TextProperty,
                    new Binding(prop.Name)
                    {
                        Source = model,
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    }
                );
                FormPanel.Children.Add(tb);
            }
        }
    }
}
