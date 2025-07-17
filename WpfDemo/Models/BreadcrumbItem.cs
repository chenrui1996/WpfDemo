using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfDemo.Menu;

namespace WpfDemo.Models
{
    public class BreadcrumbItem : ObservableObject
    {
        private string? _label;
        public string? Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private ICommand? _command;
        public ICommand? Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }
    }
}
