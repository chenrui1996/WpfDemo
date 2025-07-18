﻿using WpfDemo.Models;

namespace WpfDemo.Menu
{
    public static class MenuTrees
    {
        public static readonly List<MenuTreeItem> MenuTreeItems =
        [
            new MenuTreeItem { Label = "首页", ContentType = typeof(Views.Pages.Index) },
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
            new MenuTreeItem { Label = "Test", ContentType = typeof(Views.Pages.Test) },
            new MenuTreeItem { Label = "Test2", ContentType = typeof(Views.Pages.Test2) },
        ];
    }
}
