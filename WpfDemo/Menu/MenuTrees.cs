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
                        Label = "视图（UI）",
                        Children =
                        [
                            new MenuTreeItem
                            {
                                Label = "控件使用",
                                ContentType = typeof(Views.Pages.ControlUsage),
                            },
                            new MenuTreeItem
                            {
                                Label = "资源",
                                ContentType = typeof(Views.Pages.Resources),
                            },
                            new MenuTreeItem
                            {
                                Label = "模板",
                                ContentType = typeof(Views.Pages.Template),
                            },
                        ],
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
