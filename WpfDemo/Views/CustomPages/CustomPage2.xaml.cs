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

namespace WpfDemo.Views.CustomPages
{
    /// <summary>
    /// CustomPage2.xaml 的交互逻辑
    /// </summary>
    public partial class CustomPage2 : Page
    {
        public CustomPage2()
        {
            InitializeComponent();
        }

        public CustomPage2(ListView pageEventRecord)
        {
            _pageEventRecord = pageEventRecord;
            _pageEventRecord?.Items.Add("[CustomPage2]: 构造方法--");
            _pageEventRecord?.Items.Add("[CustomPage2]: Befor InitializeComponent--");
            InitializeComponent();
            _pageEventRecord?.Items.Add("[CustomPage2]: End InitializeComponent--");
        }

        private ListView? _pageEventRecord;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            _pageEventRecord?.Items.Add("[CustomPage2]: OnInitialized");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _pageEventRecord?.Items.Add("[CustomPage2]: OnApplyTemplate");
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            _pageEventRecord?.Items.Add($"[CustomPage2]: MeasureOverride: Size={availableSize}");
            // 关键：保持默认行为
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _pageEventRecord?.Items.Add($"[CustomPage2]: ArrangeOverride: Size={finalSize}");
            //return finalSize;
            return base.ArrangeOverride(finalSize);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            _pageEventRecord?.Items.Add($"[CustomPage2]: OnRender");
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            _pageEventRecord?.Items.Add($"[CustomPage2]: RenderSizeChanged: {sizeInfo.NewSize}");
        }
    }
}
