using System.Collections;
using System.Text;
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
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibrary;assembly=WpfCustomControlLibrary"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:Breadcrumb/>
    ///
    /// </summary>
    public class Breadcrumb : ItemsControl
    {
        static Breadcrumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Breadcrumb), new FrameworkPropertyMetadata(typeof(Breadcrumb)));
        }

        public static readonly DependencyProperty SeparatorProperty =
                DependencyProperty.Register("Separator", typeof(object), typeof(Breadcrumb), new PropertyMetadata("/"));

        public object Separator
        {
            get => GetValue(SeparatorProperty);
            set => SetValue(SeparatorProperty, value);
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is BreadcrumbItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BreadcrumbItem();
        }
    }

    public class BreadcrumbItem : ContentControl
    {
        static BreadcrumbItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BreadcrumbItem),
                new FrameworkPropertyMetadata(typeof(BreadcrumbItem)));
        }

        //Breadcrumb使用 <Setter Property="ItemContainerStyle"> 传递
        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.Register(nameof(Separator), typeof(object), typeof(BreadcrumbItem));

        public object Separator
        {
            get => GetValue(SeparatorProperty);
            set => SetValue(SeparatorProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(BreadcrumbItem));

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(BreadcrumbItem));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}