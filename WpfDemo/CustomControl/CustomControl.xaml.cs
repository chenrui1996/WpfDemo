using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfDemo.CustomControl
{
    /// <summary>
    /// CustomControl.xaml 的交互逻辑
    /// </summary>
    public partial class CustomControl : UserControl
    {
        public CustomControl()
        {
            InitializeComponent();
        }

        public CustomControl(ListView controlEventRecord)
        {
            _controlEventRecord = controlEventRecord;
            InitializeComponent();
        }

        private ListView? _controlEventRecord;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _controlEventRecord?.Items.Add("OnInitialized");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _controlEventRecord?.Items.Add("OnApplyTemplate");
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            _controlEventRecord?.Items.Add($"MeasureOverride: Size={availableSize}");
            // 关键：保持默认行为
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _controlEventRecord?.Items.Add($"ArrangeOverride: Size={finalSize}");
            //return finalSize;
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            _controlEventRecord?.Items.Add($"OnRender");
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            _controlEventRecord?.Items.Add($"OnRenderSizeChanged: {sizeInfo.NewSize}");
        }

        protected override void OnVisualParentChanged(DependencyObject e)
        {
            base.OnVisualParentChanged(e);
            _controlEventRecord?.Items.Add("OnVisualParentChanged");
        }
    }
}
