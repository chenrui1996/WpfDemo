using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.DwayneNeed.Win32.Gdi32;
using WpfDemo.ViewModels.PageViewModels;
using static WpfDemo.Attributes.CustomDataGridAttributes;

namespace WpfDemo.CustomControl
{
    public class SelectableItem<T>(T item) : INotifyPropertyChanged
    {
        public T Item { get; } = item;

        private bool _sc_isSelected;
        public bool SC_IsSelected
        {
            get => _sc_isSelected;
            set
            {
                if (_sc_isSelected != value)
                {
                    _sc_isSelected = value;
                    OnPropertyChanged(nameof(SC_IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    /// <summary>
    /// CustomDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class CustomDataGrid : UserControl
    {
        public CustomDataGrid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 定义外部数据源
        /// </summary>
        public static readonly DependencyProperty SourceItemsProperty = DependencyProperty.Register(
            nameof(SourceItems),
            typeof(IEnumerable),
            typeof(CustomDataGrid),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                OnSourceItemsChanged
            )
        );

        public IEnumerable SourceItems
        {
            get => (IEnumerable)GetValue(SourceItemsProperty);
            set => SetValue(SourceItemsProperty, value);
        }

        private static void OnSourceItemsChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            var control = (CustomDataGrid)d;
            if (e.NewValue is not IEnumerable items)
            {
                return;
            }

            control.DataGrid.AutoGenerateColumns = false;
            control.DataGrid.Columns.Clear();

            var itemType = GetItemType(items);
            if (itemType == null)
            {
                return;
            }
            var properties = itemType.GetProperties();

            if (itemType.GetCustomAttribute<EnableSelectionAttribute>()?.EnableCheckbox == true)
            {
                //每次失去焦点才触发
                //control.DataGrid.Columns.Add(
                //    new DataGridCheckBoxColumn { Header = "", Binding = new Binding("IsSelected") }
                //);
                //修改为
                var checkBoxColumn = new DataGridTemplateColumn
                {
                    Header = "",
                    CellTemplate = CreateCheckBoxTemplate("SC_IsSelected"),
                };
                control.DataGrid.Columns.Add(checkBoxColumn);
            }

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<GridColumnAttribute>();
                if (attr == null || !attr.IsVisible)
                    continue;
                //失去了control.DataGrid.AutoGenerateColumns = true; 时的枚举类型和数字输入框的特性
                //var column = new DataGridTextColumn
                //{
                //    Header = string.IsNullOrEmpty(attr?.Label) ? prop.Name : attr.Label,
                //    Binding = new Binding($"Item.{prop.Name}"),
                //    IsReadOnly = !(attr?.IsEditable ?? false),
                //};
                //修改为
                var column = CreateColumn(prop, attr);
                control.DataGrid.Columns.Add(column);
            }

            var list = (IList)
                Activator.CreateInstance(
                    typeof(List<>).MakeGenericType(
                        typeof(SelectableItem<>).MakeGenericType(itemType)
                    )
                )!;

            foreach (var item in items)
            {
                var wrapper = Activator.CreateInstance(
                    typeof(SelectableItem<>).MakeGenericType(itemType),
                    item
                );
                if (wrapper is INotifyPropertyChanged npc)
                {
                    //绑定每一行的属性变化事件，捕获checkbox是否选中
                    npc.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == "SC_IsSelected")
                        {
                            control.SelectedItems.Clear();
                            control
                                .Items.Cast<object>()
                                ?.ToList()
                                .Where(i =>
                                {
                                    var isCheckedProp = i.GetType().GetProperty("SC_IsSelected");
                                    return isCheckedProp != null
                                        && (bool?)isCheckedProp.GetValue(i) == true;
                                })
                                ?.ToList()
                                .ForEach(
                                    (it) =>
                                    {
                                        control.SelectedItems.Add(
                                            it.GetType().GetProperty("Item")?.GetValue(it)
                                        );
                                    }
                                );
                            control.RaiseEvent(new RoutedEventArgs(SelectedItemsChangedEvent));
                            //不能直接赋值，会中断传递
                            //control.SelectedItems = selected;
                            //control.SetCurrentValue(SelectedItemsProperty, selected);
                        }
                    };
                }
                list.Add(wrapper);
            }
            // 赋值回 Items（避免无限递归要用 SetCurrentValue 或保护递归）
            control.SetCurrentValue(ItemsProperty, list);
        }

        private static DataTemplate CreateCheckBoxTemplate(string bindingPath)
        {
            string xaml =
                $@"<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                        xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                <CheckBox IsChecked=""{{Binding {bindingPath}, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}}"" 
                          HorizontalAlignment=""Center"" VerticalAlignment=""Center""/>
          </DataTemplate>";

            return (DataTemplate)System.Windows.Markup.XamlReader.Parse(xaml);
        }

        private static DataGridColumn CreateColumn(PropertyInfo prop, GridColumnAttribute attr)
        {
            var binding = new Binding($"Item.{prop.Name}")
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };

            string header = string.IsNullOrEmpty(attr?.Label) ? prop.Name : attr.Label;

            if (prop.PropertyType.IsEnum)
            {
                return new DataGridComboBoxColumn
                {
                    Header = header,
                    SelectedItemBinding = binding,
                    ItemsSource = Enum.GetValues(prop.PropertyType),
                    IsReadOnly = !(attr?.IsEditable ?? false),
                };
            }
            else if (prop.PropertyType == typeof(bool))
            {
                return new DataGridCheckBoxColumn
                {
                    Header = header,
                    Binding = binding,
                    IsReadOnly = !(attr?.IsEditable ?? false),
                };
            }
            else
            {
                return new DataGridTextColumn
                {
                    Header = header,
                    Binding = binding,
                    IsReadOnly = !(attr?.IsEditable ?? false),
                };
            }
        }

        /// <summary>
        /// 定义内部数据源
        /// </summary>
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            nameof(Items),
            typeof(IEnumerable),
            typeof(CustomDataGrid),
            new PropertyMetadata(null)
        );

        public IEnumerable Items
        {
            get => (IEnumerable)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        /// <summary>
        /// 定义已选择数据
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register(
                nameof(SelectedItems),
                typeof(IList),
                typeof(CustomDataGrid),
                new FrameworkPropertyMetadata(
                    null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                )
            );

        public static readonly RoutedEvent SelectedItemsChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(SelectedItemsChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(CustomDataGrid)
            );

        public event RoutedEventHandler SelectedItemsChanged
        {
            add => AddHandler(SelectedItemsChangedEvent, value);
            remove => RemoveHandler(SelectedItemsChangedEvent, value);
        }

        public IList SelectedItems
        {
            get => (IList)GetValue(SelectedItemsProperty);
            set => SetValue(SelectedItemsProperty, value);
        }

        private void DataGrid_DataContextChanged(
            object sender,
            DependencyPropertyChangedEventArgs e
        )
        {
            //不能在此处处理 DataGrid
            //依赖属性的绑定是在 布局加载完成后逐步执行的。
            //具体过程如下：
            //控件加载时，XAML 引擎解析控件结构；
            //设置 DataContext（触发 DataContextChanged）；
            //然后 ItemsSource 绑定生效 → Items 才会被赋值。
            //DataContextChanged 发生时，Items 绑定还没完成
        }

        private static Type? GetItemType(IEnumerable items)
        {
            var type = items?.GetType();
            if (type == null)
                return null;
            if (type.IsGenericType)
                return type.GetGenericArguments().FirstOrDefault();
            return type.GetElementType();
        }
    }
}
