using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfCustomControlLibrary
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary;assembly=WpfCustomControlLibrary"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomButton/>
    ///
    /// </summary>
    public class CustomButton : Button
    {
        public static readonly RoutedEvent MyClickEvent = EventManager.RegisterRoutedEvent(
            "MyClick", // 事件名称
            RoutingStrategy.Bubble, // 路由策略（可以是 Bubble、Tunnel 或 Direct）
            typeof(RoutedEventHandler), // 事件处理器类型
            typeof(CustomButton)
        ); // 所属类型

        static CustomButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(CustomButton),
                new FrameworkPropertyMetadata(typeof(Button))
            );
        }

        // CLR事件包装器
        public event RoutedEventHandler MyClick
        {
            add { AddHandler(MyClickEvent, value); }
            remove { RemoveHandler(MyClickEvent, value); }
        }

        // 方法：触发事件
        protected void RaiseMyClickEvent()
        {
            RoutedEventArgs args = new(MyClickEvent);
            RaiseEvent(args);
        }

        // 示例：右键点击控件时触发事件
        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            RaiseMyClickEvent();
        }
    }
}
