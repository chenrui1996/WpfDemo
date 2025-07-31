using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using WpfDemo.Services;

namespace WpfDemo.ViewModels.PageViewModels
{
    public class LifecycleHandleViewModel : ViewModelBase
    {
        private LifecycleHandleService _service;

        public LifecycleHandleViewModel(LifecycleHandleService service)
        {
            _service = service;
            SaveAsSVG = new RelayCommand(ExportToSvg);
            Graph = DrawGraph();
        }

        /// <summary>
        /// ָ��
        /// </summary>
        public RelayCommand SaveAsSVG { get; }

        /// <summary>
        /// ��Ӧʽ����
        /// </summary>
        private string? _customProp;
        public string? CustomProp
        {
            get => _customProp;
            set => SetProperty(ref _customProp, value);
        }

        /// <summary>
        /// ��Ӧʽ����
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
                ("���캯��", "OnInitialized"),
                ("OnInitialized", "OnApplyTemplate"),
                ("OnApplyTemplate", "MeasureOverride"),
                ("MeasureOverride", "ArrangeOverride"),
                ("ArrangeOverride", "OnRender"),
                ("OnRender", "OnUnloaded"),
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
            // ��ִ�в��ּ���
            var renderer = new GraphRenderer(Graph);
            renderer.CalculateLayout();

            var dialog = new SaveFileDialog
            {
                Filter = "SVG �ļ� (*.svg)|*.svg",
                Title = "����Ϊ SVG �ļ�",
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
