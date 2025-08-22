using WpfDemo.Models;

namespace WpfDemo.Menu
{
    public static class MenuTrees
    {
        public static readonly List<MenuTreeItem> MenuTreeItems =
        [
            new MenuTreeItem { Label = "首页", ContentType = typeof(Views.Pages.Index) },
            new MenuTreeItem
            {
                Label = "基本用法",
                Children =
                [
                    new MenuTreeItem
                    {
                        Label = "XAML (UI)",
                        Children =
                        [
                            new MenuTreeItem
                            {
                                Label = "控件使用",
                                ContentType = typeof(Views.Pages.ControlUsage),
                            },
                            new MenuTreeItem
                            {
                                Label = "资源(Resources)",
                                ContentType = typeof(Views.Pages.Resources),
                            },
                            new MenuTreeItem
                            {
                                Label = "样式(Style)",
                                ContentType = typeof(Views.Pages.Style),
                            },
                            new MenuTreeItem
                            {
                                Label = "模板(Template)",
                                ContentType = typeof(Views.Pages.Template),
                            },
                            new MenuTreeItem
                            {
                                Label = "触发器(Trigger)",
                                ContentType = typeof(Views.Pages.Trigger),
                            },
                        ],
                    },
                    new MenuTreeItem
                    {
                        Label = "XAML.cs (交互)",
                        Children =
                        [
                            new MenuTreeItem
                            {
                                Label = "事件处理(Event)",
                                ContentType = typeof(Views.Pages.EventHandle),
                            },
                            new MenuTreeItem
                            {
                                Label = "控件访问和动态操作(Control)",
                                ContentType = typeof(Views.Pages.ControlHandle),
                            },
                            new MenuTreeItem
                            {
                                Label = "生命周期事件(Lifecycle)",
                                ContentType = typeof(Views.Pages.LifecycleHandle),
                            },
                        ],
                    },
                    new MenuTreeItem
                    {
                        Label = "ViewModel",
                        Children =
                        [
                            new MenuTreeItem
                            {
                                Label = "响应式属性(Dependency properties)",
                                ContentType = typeof(Views.Pages.DependencyProperties),
                            },
                            new MenuTreeItem
                            {
                                Label = "指令(Command)",
                                ContentType = typeof(Views.Pages.Command),
                            },
                            new MenuTreeItem
                            {
                                Label = "CommunityToolkit.Mvvm",
                                ContentType = typeof(Views.Pages.CommunityToolkit),
                            },
                        ],
                    },
                    new MenuTreeItem
                    {
                        Label = "转换器(Converts)",
                        ContentType = typeof(Views.Pages.Converters),
                    },
                ],
            },
            new MenuTreeItem
            {
                Label = "组件",
                Children =
                [
                    new MenuTreeItem
                    {
                        Label = "组件概述",
                        ContentType = typeof(Views.Pages.ControlSummary),
                    },
                    new MenuTreeItem
                    {
                        Label = "组合控件（UserControl）",
                        ContentType = typeof(Views.Pages.UserControlDemo),
                    },
                    new MenuTreeItem
                    {
                        Label = "扩展控件（基于现有控件）",
                        ContentType = typeof(Views.Pages.ExtendedControlDemo),
                    },
                    new MenuTreeItem
                    {
                        Label = "自定义控件（CustomControl）",
                        ContentType = typeof(Views.Pages.CustomControlDemo),
                    },
                ],
            },
            new MenuTreeItem
            {
                Label = "数据处理",
                Children =
                [
                    new MenuTreeItem
                    {
                        Label = "系统配置文件",
                        ContentType = typeof(Views.Pages.SystemConfig),
                    },
                    new MenuTreeItem
                    {
                        Label = "文件存储",
                        ContentType = typeof(Views.Pages.FileStorage),
                    },
                    new MenuTreeItem
                    {
                        Label = "数据库存储",
                        ContentType = typeof(Views.Pages.DatabaseStorage),
                    },
                    new MenuTreeItem
                    {
                        Label = "Http请求",
                        ContentType = typeof(Views.Pages.HttpRequest),
                    },
                ],
            },
            new MenuTreeItem
            {
                Label = "生成测试",
                Children =
                [
                    new MenuTreeItem
                    {
                        Label = "测试页面1",
                        ContentType = typeof(Views.Pages.Test),
                    },
                    new MenuTreeItem
                    {
                        Label = "测试页面2",
                        ContentType = typeof(Views.Pages.Test2),
                    },
                ],
            },
        ];
    }
}
