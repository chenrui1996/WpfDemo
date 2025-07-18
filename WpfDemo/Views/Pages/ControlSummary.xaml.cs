using System.Windows;
using System.Windows.Controls;
using Microsoft.Msagl.Drawing;
using WpfDemo.ViewModels.PageViewModels;

namespace WpfDemo.Views.Pages
{
    static class ControlDescription
    {
        public static readonly Dictionary<string, List<(string, string)>> ControlDescriptionDic =
            new()
            {
                ["UIElement"] =
                [
                    // 属性
                    ("IsEnabled", "控件是否可用"),
                    ("IsFocused", "控件是否获得焦点"),
                    ("IsMouseOver", "鼠标是否悬停在控件上"),
                    ("Visibility", "控件的可见性"),
                    ("RenderTransform", "渲染转换（缩放、旋转等）"),
                    ("Opacity", "控件透明度"),
                    ("Clip", "裁剪区域"),
                    ("IsKeyboardFocused", "是否拥有键盘焦点"),
                    ("AllowDrop", "是否允许拖放"),
                    // 方法
                    ("Focus()", "尝试将焦点设置到当前元素"),
                    ("CaptureMouse()", "捕获鼠标输入"),
                    ("ReleaseMouseCapture()", "释放鼠标捕获"),
                    ("InvalidateVisual()", "强制重绘元素"),
                    ("Measure(Size)", "计算元素大小（用于布局）"),
                    ("Arrange(Rect)", "安排元素在容器中的位置"),
                    ("AddHandler(RoutedEvent, Delegate)", "添加事件处理器"),
                ],

                ["FrameworkElement"] =
                [
                    // 属性
                    ("Width", "设置元素宽度"),
                    ("Height", "设置元素高度"),
                    ("MinWidth", "最小宽度"),
                    ("MaxWidth", "最大宽度"),
                    ("Margin", "元素外边距"),
                    ("HorizontalAlignment", "水平对齐方式"),
                    ("VerticalAlignment", "垂直对齐方式"),
                    ("DataContext", "数据绑定上下文"),
                    ("Style", "控件样式"),
                    ("Resources", "控件资源集合"),
                    ("Name", "控件名称"),
                    ("Tag", "附加任意数据"),
                    ("ActualWidth", "控件实际宽度"),
                    ("ActualHeight", "控件实际高度"),
                    ("Parent", "父元素"),
                    ("ToolTip", "工具提示"),
                    // 方法
                    ("SetBinding(DependencyProperty, Binding)", "设置数据绑定"),
                    ("FindName(string)", "查找子元素"),
                    ("ApplyTemplate()", "应用模板"),
                    ("UpdateLayout()", "更新布局"),
                    ("GetBindingExpression(DependencyProperty)", "获取绑定表达式"),
                ],

                ["Control"] =
                [
                    ("Template", "获取或设置控件的控制模板"),
                    ("ApplyTemplate()", "应用模板并构建可视树"),
                    ("IsEnabled", "指示控件是否可交互"),
                    ("Focusable", "指示控件是否可以获得焦点"),
                ],

                ["ContentControl"] =
                [
                    ("Content", "获取或设置控件显示的内容"),
                    ("ContentTemplate", "内容的 DataTemplate"),
                    ("HasContent", "指示 Content 是否不为 null"),
                ],

                ["ButtonBase"] =
                [
                    ("Click", "用户点击按钮时发生的事件"),
                    ("Command", "绑定的命令"),
                    ("CommandParameter", "传递给命令的参数"),
                ],

                ["Button"] =
                [
                    // Inherits from ButtonBase, ContentControl, Control
                    ("Click", "用户点击按钮时发生的事件"),
                    ("Content", "获取或设置按钮的显示内容"),
                    ("Command", "绑定的命令"),
                    ("IsDefault", "是否为默认按钮（回车触发）"),
                    ("IsCancel", "是否为取消按钮（Esc 触发）"),
                ],

                ["TextBoxBase"] =
                [
                    ("IsReadOnly", "是否只读"),
                    ("Undo()", "撤销上一个操作"),
                    ("Redo()", "重做上一个撤销"),
                    ("Clear()", "清除文本"),
                ],

                ["TextBox"] =
                [
                    // Inherits from TextBoxBase, Control
                    ("Text", "获取或设置文本内容"),
                    ("MaxLength", "限制最大字符数"),
                    ("AcceptsReturn", "是否接受回车换行"),
                    ("CaretIndex", "光标索引"),
                ],

                ["ItemsControl"] =
                [
                    ("ItemsSource", "绑定的数据集合"),
                    ("Items", "控件所包含的项集合"),
                    ("ItemTemplate", "每一项使用的模板"),
                ],

                ["ListBox"] =
                [
                    // Inherits from Selector, ItemsControl
                    ("SelectedItem", "当前选中的项"),
                    ("SelectedIndex", "当前选中项的索引"),
                    ("SelectionMode", "多选或单选"),
                ],

                ["ComboBox"] =
                [
                    // Inherits from Selector, ItemsControl
                    ("IsDropDownOpen", "是否展开下拉"),
                    ("Text", "选中项文本（可编辑模式）"),
                    ("SelectedValue", "获取选中项的指定属性值"),
                ],

                ["CheckBox"] =
                [
                    // Inherits from ToggleButton, ButtonBase, ContentControl
                    ("IsChecked", "是否被选中"),
                    ("Checked", "选中时触发事件"),
                    ("Unchecked", "取消选中时触发事件"),
                ],

                ["RadioButton"] = [("GroupName", "指定单选组名称"), ("IsChecked", "是否选中")],

                ["Label"] =
                [
                    // Inherits from ContentControl
                    ("Target", "与某一控件关联（可用于无障碍支持）"),
                ],

                ["Slider"] =
                [
                    ("Minimum", "最小值"),
                    ("Maximum", "最大值"),
                    ("Value", "当前值"),
                    ("TickFrequency", "刻度间距"),
                ],
            };
    }

    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class ControlSummary : UserControl
    {
        public ControlSummary()
        {
            InitializeComponent();
            graphViewer.Graph = DrawGraph();
        }

        private Graph DrawGraph()
        {
            var graph = new Graph();
            var edges = new[]
            {
                ("UIElement", "FrameworkElement"),
                ("FrameworkElement", "Control"),
                ("Control", "ContentControl"),
                ("ContentControl", "HeaderedContentControl"),
                ("Control", "ItemsControl"),
                ("ItemsControl", "Selector"),
                ("Selector", "ComboBox"),
                ("Selector", "ListBox"),
                ("ContentControl", "ButtonBase"),
                ("ButtonBase", "Button"),
                ("ContentControl", "Label"),
                ("Control", "TextBoxBase"),
                ("TextBoxBase", "TextBox"),
                ("HeaderedContentControl", "GroupBox"),
            };

            foreach (var (parent, child) in edges)
            {
                graph.AddEdge(parent, child);
            }

            foreach (var node in graph.Nodes)
            {
                node.Attr.Shape = Shape.Box;
                node.Attr.FillColor = Color.LightBlue;
            }

            foreach (var edge in graph.Edges)
            {
                edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
            }

            return graph;
        }

        private void GraphViewer_ObjectUnderMouseCursorChanged(
            object sender,
            ObjectUnderMouseCursorChangedEventArgs e
        )
        {
            var obj = graphViewer.ObjectUnderMouseCursor;
            if (obj is IViewerNode vn)
            {
                var key = vn.Node.LabelText.Trim();
                description.Header = key;
                if (ControlDescription.ControlDescriptionDic.ContainsKey(key))
                {
                    descriptionItems.ItemsSource = ControlDescription
                        .ControlDescriptionDic[key]
                        .Select(s => new { Name = s.Item1, Description = s.Item2 });
                }
            }
        }

        private void Description_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            descriptionScrollViewer.Height = e.NewSize.Height * 0.85;
        }
    }
}
