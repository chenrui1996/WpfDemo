using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfDemo.Models
{
    public partial class MenuTreeItem : ObservableObject
    {
        public Guid Guid { get; set; }
        public bool Focusable 
        { 
            get 
            { 
                return !Children.Any(); 
            } 
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

        public object? Content { set; get; }

        private object? CreateContent(Type contentType)
        {
            return Activator.CreateInstance(contentType);
        }
    }
}