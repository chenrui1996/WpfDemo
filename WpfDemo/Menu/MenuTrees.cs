using WpfDemo.Models;

namespace WpfDemo.Menu
{
    public static class MenuTrees
    {
        public static readonly List<MenuTreeItem> MenuTreeItems =
        [
            new MenuTreeItem
            {
                Label = "首页",
                ContentType = typeof(Views.Pages.Index)
            },
            new MenuTreeItem
            {
                Label = "测试菜单",
                Children = 
                [
                    new MenuTreeItem
                    {
                        Label = "测试",
                        ContentType = typeof(Views.Pages.Test)
                    },
                ]
            },
        ];
    }
}
