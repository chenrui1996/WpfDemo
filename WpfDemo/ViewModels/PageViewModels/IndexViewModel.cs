using System.Collections.ObjectModel;
using Microsoft.Msagl.Drawing;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class IndexViewModel : ViewModelBase
    {
        public IndexViewModel()
        {
            CustomCommand = new AnotherCommandImplementation(_ => { });
            Graph = DrawGraph();
        }

        /// <summary>
        /// 指令
        /// </summary>
        public AnotherCommandImplementation CustomCommand { get; }

        /// <summary>
        /// 响应式属性
        /// </summary>
        private int _customProp;
        public int CustomProp
        {
            get => _customProp;
            set => SetProperty(ref _customProp, value);
        }

        /// <summary>
        /// 响应式集合
        /// </summary>
        private ObservableCollection<object> _customList = [];

        public ObservableCollection<object> CustomList
        {
            get => _customList;
            set { SetProperty(ref _customList, value); }
        }

        private Graph? _graph;

        public Graph? Graph
        {
            get => _graph;
            set => SetProperty(ref _graph, value);
        }

        private Graph DrawGraph()
        {
            var graph = new Graph();
            var inheritEdges = new[]
            {
                ("MyApp", "View"),
                ("MyApp", "Converters"),
                ("MyApp", "ViewModel"),
                ("MyApp", "Model"),
                ("MyApp", "Services"),
                ("MyApp", "Data"),
                //View
                ("View", "XAML(UI)"),
                ("View", "XAML.cs(交互)"),
                //XAML
                ("XAML(UI)", "Control"),
                ("XAML(UI)", "Resources"),
                //Control
                ("Control", "Property"),
                ("Control", "Style"),
                ("Control", "Template"),
                ("Control", "Trigger"),
                //Template
                ("Template", "ControlTemplate"),
                ("Template", "DataTemplate"),
                ("Template", "ItemsPanelTemplate"),
                ("Template", "HierarchicalDataTemplate"),
                //Trigger
                ("Trigger", "DefaultTrigger"),
                ("Trigger", "DataTrigger"),
                ("Trigger", "MultiTrigger"),
                ("Trigger", "MultiDataTrigger"),
                ("Trigger", "EventTrigger"),
                //XAML.cs(交互)
                ("XAML.cs(交互)", "Event Handle"),
                ("XAML.cs(交互)", "Control Handle"),
                ("XAML.cs(交互)", "DataContext(ViewModel)"),
                ("XAML.cs(交互)", "Lifecycle Handle"),
                //ViewModel
                ("ViewModel", "DependencyProperty"),
                ("ViewModel", "Command"),
                ("ViewModel", "Services Call"),
                //Command
                ("Command", "RoutedCommand"),
                ("Command", "RoutedUICommand"),
                ("Command", "ICommand"),
                //Data
                ("Data", "Json"),
                ("Data", "SQLite/LiteDB"),
                //Services
                ("Services", "Data Handle"),
                ("Services", "Business Services"),
                ("Services", "Api Call"),
            };

            foreach (var (parent, child) in inheritEdges)
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
    }
}
