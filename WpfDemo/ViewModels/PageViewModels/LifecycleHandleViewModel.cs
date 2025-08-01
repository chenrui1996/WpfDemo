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
            SaveAsSVGForPage = new RelayCommand(ExportToSvgForPage);
            Graph = DrawGraph();
            GraphForPage = DrawGraphForPage();
        }

        /// <summary>
        /// ָ��
        /// </summary>
        public RelayCommand SaveAsSVG { get; }

        /// <summary>
        /// ָ��
        /// </summary>
        public RelayCommand SaveAsSVGForPage { get; }

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
                ("OnRender", "OnRenderSizeChanged"),
                ("OnRenderSizeChanged", "Loaded"),
                ("OnVisualParentChanged", "UnLoaded"),
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
                FileName = "graph4ContolLifecycle.svg",
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = dialog.FileName;
                using var stream = File.Create(selectedPath);
                var writer = new SvgGraphWriter(stream, Graph) { Precision = 4 };
                writer.Write();
            }
        }

        private Graph? _graphForPage;

        public Graph? GraphForPage
        {
            get => _graphForPage;
            set => SetProperty(ref _graphForPage, value);
        }

        private Graph DrawGraphForPage()
        {
            var graph = new Graph();
            var inheritEdges = new[]
            {
                //��ʼ��Page1
                ("��ʼ��(Page1)", "���캯��(Page1)"),
                ("���캯��(Page1)", "OnInitialized(Page1)"),
                ("OnInitialized(Page1)", "��ʼ��Pages����"),
                //��ʼ��Page2
                ("��ʼ��(Page2)", "���캯��(Page2)"),
                ("���캯��(Page2)", "OnInitialized(Page2)"),
                ("OnInitialized(Page2)", "��ʼ��Pages����"),
                ("��ʼ��Pages����", "������ Page1(Frame[null])"),
                //������ Page1(��ǰFrameΪnull)
                ("������ Page1(Frame[null])", "Navigating(Frame[null])"),
                ("Navigating(Frame[null])", "Navigated(Frame[null])"),
                ("Navigated(Frame[null])", "OnApplyTemplate(Page1)"),
                ("OnApplyTemplate(Page1)", "MeasureOverride(Page1)"),
                ("MeasureOverride(Page1)", "ArrangeOverride(Page1)"),
                ("ArrangeOverride(Page1)", "OnRender(Page1)"),
                ("OnRender(Page1)", "OnRenderSizeChanged(Page1)"),
                ("OnRenderSizeChanged(Page1)", "Loaded(Page1)"),
                //������ Page2(��ǰFrameΪPage1)
                ("������ Page2(Frame[Page1])", "Navigating(Frame[Page1])"),
                ("Navigating(Frame[Page1])", "Navigated(Frame[Page1])"),
                ("Navigated(Frame[Page1])", "OnApplyTemplate(Page2)"),
                ("OnApplyTemplate(Page2)", "MeasureOverride(Page2)"),
                ("MeasureOverride(Page2)", "ArrangeOverride(Page2)"),
                ("ArrangeOverride(Page2)", "OnRender(Page2)"),
                ("OnRender(Page2)", "OnRenderSizeChanged(Page2)"),
                ("OnRenderSizeChanged(Page2)", "UnLoaded(Page1)"),
                ("UnLoaded(Page1)", "Loaded(Page2)"),
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

        private void ExportToSvgForPage()
        {
            if (GraphForPage == null)
            {
                return;
            }
            // ��ִ�в��ּ���
            var renderer = new GraphRenderer(GraphForPage);
            renderer.CalculateLayout();

            var dialog = new SaveFileDialog
            {
                Filter = "SVG �ļ� (*.svg)|*.svg",
                Title = "����Ϊ SVG �ļ�",
                FileName = "graph4PageLifecycle.svg",
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var selectedPath = dialog.FileName;
                using var stream = File.Create(selectedPath);
                var writer = new SvgGraphWriter(stream, GraphForPage) { Precision = 4 };
                writer.Write();
            }
        }
    }
}
