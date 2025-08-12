using System.Collections.ObjectModel;
using WpfDemo.Models;

namespace WpfDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            SetMenuTree();

            //HomePage放在首位
            SelectedTreeItem = _menuTrees.FirstOrDefault();

            HomeCommand = new RelayCommandImplementation(_ =>
            {
                SelectedTreeItem = _menuTrees.FirstOrDefault();
            });

            MovePrevCommand = new RelayCommandImplementation(
                _ =>
                {
                    if (SelectedTreeItem == null || !_menuTreeItemBack.Any())
                    {
                        return;
                    }
                    _menuTreeItemForward.Push(SelectedTreeItem);
                    if (_menuTreeItemBack.TryPop(out MenuTreeItem? re))
                    {
                        //使用前进后退时的页面不记录历史，仅导航当前堆栈内页面
                        _menuTreeIteHistory = null;
                        SelectedTreeItem = re;
                    }
                },
                _ => _menuTreeItemBack.Count() > 0
            );

            MoveNextCommand = new RelayCommandImplementation(
                _ =>
                {
                    if (SelectedTreeItem == null || !_menuTreeItemForward.Any())
                    {
                        return;
                    }
                    _menuTreeItemBack.Push(SelectedTreeItem);
                    if (_menuTreeItemForward.TryPop(out MenuTreeItem? re))
                    {
                        //使用前进后退时的页面不记录历史，仅导航当前堆栈内页面
                        _menuTreeIteHistory = null;
                        SelectedTreeItem = re;
                    }
                },
                _ => _menuTreeItemForward.Count() > 0
            );

            ClaerHistoryCommand = new RelayCommandImplementation(
                _ =>
                {
                    _menuTreeItemBack.Clear();
                    _menuTreeItemForward.Clear();
                },
                _ => _menuTreeItemForward.Count() > 0 || _menuTreeItemBack.Count() > 0
            );

            RefreshCommand = new RelayCommandImplementation(_ =>
            {
                SelectedTreeItem?.Refresh();
            });
        }

        private ObservableCollection<BreadcrumbItem> _breadcrumb = [];

        public ObservableCollection<BreadcrumbItem> Breadcrumb
        {
            get => _breadcrumb;
            set { SetProperty(ref _breadcrumb, value); }
        }

        private void SetBreadcrumb()
        {
            Breadcrumb.Clear();
            if (_selectedTreeItem == null)
            {
                return;
            }
            SetBreadcrumb(_selectedTreeItem);
        }

        private void SetBreadcrumb(MenuTreeItem treeItem)
        {
            if (treeItem == null)
            {
                return;
            }
            if (treeItem?.Parent != null)
            {
                SetBreadcrumb(treeItem.Parent);
            }
            Breadcrumb.Add(
                new BreadcrumbItem
                {
                    Label = treeItem?.Label ?? "",
                    Command = new RelayCommandImplementation(_ => { }),
                }
            );
        }

        private ObservableCollection<MenuTreeItem> _menuTrees = [];
        public ObservableCollection<MenuTreeItem> MenuTrees
        {
            get => _menuTrees;
            set => SetProperty(ref _menuTrees, value);
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => SetProperty(ref _selectedIndex, value);
        }

        private MenuTreeItem? _selectedTreeItem;
        public MenuTreeItem? SelectedTreeItem
        {
            get { return _selectedTreeItem; }
            set
            {
                try
                {
                    if (_selectedTreeItem != value)
                    {
                        SetProperty(ref _selectedTreeItem, value);
                        if (_selectedTreeItem != null)
                        {
                            //当前页的上一页作为历史记录放入历史记录堆栈
                            if (_menuTreeIteHistory != null)
                            {
                                _menuTreeItemBack.Push(_menuTreeIteHistory);
                            }
                            //存放当前页的上一页作为历史记录
                            _menuTreeIteHistory = _selectedTreeItem;
                        }
                        SetBreadcrumb();
                    }
                }
                catch { }
            }
        }

        private void SetMenuTree()
        {
            MenuTrees.Clear();
            Menu.MenuTrees.MenuTreeItems.ForEach(item =>
            {
                MenuTrees.Add(item);
            });
        }

        public RelayCommandImplementation HomeCommand { get; }
        public RelayCommandImplementation MovePrevCommand { get; }
        public RelayCommandImplementation MoveNextCommand { get; }
        public RelayCommandImplementation ClaerHistoryCommand { get; }
        public RelayCommandImplementation RefreshCommand { get; }

        private MenuTreeItem? _menuTreeIteHistory { set; get; }
        private Stack<MenuTreeItem> _menuTreeItemBack { set; get; } = new();
        private Stack<MenuTreeItem> _menuTreeItemForward { set; get; } = new();
    }
}
