using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class IndexViewModel : ViewModelBase
    {
        public IndexViewModel()
        {
            SaveAsSVG = new RelayCommand(ExportToSvg);
            Graph = DrawGraph();
        }

        /// <summary>
        /// 指令
        /// </summary>
        public ICommand SaveAsSVG { get; }

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
                ("MyWPFApp", "View"),
                ("MyWPFApp", "ViewModel"),
                ("MyWPFApp", "Converters(数据转换)"),
                ("MyWPFApp", "Model"),
                ("MyWPFApp", "Services"),
                ("MyWPFApp", "Data"),
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
                ("ViewModel", "Dependency Property"),
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
                node.Attr.Padding = 10;
            }

            foreach (var edge in graph.Edges)
            {
                edge.Attr.ArrowheadAtTarget = ArrowStyle.Normal;
            }

            return graph;
        }

        private void ExportToSvg()
        {
            if (Graph == null)
            {
                return;
            }
            // 先执行布局计算
            var renderer = new GraphRenderer(Graph);
            renderer.CalculateLayout();

            var dialog = new SaveFileDialog
            {
                Filter = "SVG 文件 (*.svg)|*.svg",
                Title = "保存为 SVG 文件",
                FileName = "graph.svg",
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = dialog.FileName;
                using var stream = File.Create(selectedPath);
                var writer = new SvgGraphWriter(stream, Graph) { Precision = 4 };
                writer.Write();
            }
        }
    }
}
