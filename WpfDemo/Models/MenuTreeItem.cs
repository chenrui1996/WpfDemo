using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Xml.Linq;
using Autofac;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfDemo.Models
{
    public partial class MenuTreeItem : Modelbase
    {
        public Guid Guid { get; set; }
        public bool Focusable
        {
            get { return !Children.Any(); }
        }

        private string? _label { set; get; }
        public string? Label
        {
            get { return _label; }
            set
            {
                _label = value;
                Guid = Guid.NewGuid();
            }
        }

        public MenuTreeItem? Parent { get; set; }

        private BaseCollection<MenuTreeItem> _children = [];

        public BaseCollection<MenuTreeItem> Children
        {
            get { return _children; }
            set
            {
                if (value.Any())
                {
                    foreach (var item in value)
                    {
                        item.Parent = this;
                    }
                }
                _children = value;
            }
        }

        private Type? _contentType;

        public Type? ContentType
        {
            get { return _contentType; }
            set
            {
                _contentType = value;
                if (value != null)
                {
                    Content = CreateContent(value);
                }
            }
        }

        /// <summary>
        /// 需要刷新，所以需要双向绑定
        /// </summary>
        private object? _content;
        public object? Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private object? CreateContent(Type contentType)
        {
            var content = Activator.CreateInstance(contentType);
            if (content == null)
            {
                return null;
            }

            string viewModelName = contentType.Name + "ViewModel";
            Type? viewModelType = Type.GetType(
                "WpfDemo.ViewModels.PageViewModels." + viewModelName
            );
            if (viewModelType == null)
            {
                return content;
            }

            if (App.Container == null || !App.Container.IsRegistered(viewModelType))
            {
                return content;
            }

            var viewModel = App.Container.Resolve(viewModelType);
            if (viewModel == null)
            {
                return content;
            }

            if (content is System.Windows.Controls.UserControl userControl)
            {
                userControl.DataContext = viewModel;
                return userControl;
            }

            return content;
        }

        public void Refresh()
        {
            if (ContentType == null)
                return;
            Content = null;
            Content = CreateContent(ContentType);
        }

        public IEnumerable<MenuTreeItem> GetAllChildren()
        {
            foreach (var child in Children)
            {
                yield return child;
                foreach (var sub in child.GetAllChildren())
                {
                    yield return sub;
                }
            }
        }
    }
}
