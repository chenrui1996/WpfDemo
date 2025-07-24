@echo off
setlocal EnableDelayedExpansion

:: 提示输入 name
set /p name=请输入控件/视图的 Name（首字母大写，如 Index）
if "%name%"=="" (
  echo Name 不能为空！
  goto :eof
)

:: 配置输出目录（根据你项目实际路径调整）
set xamlDir=.\Views\Pages
set vmDir=.\ViewModels\PageViewModels
set serviceDir=.\Services\PageServices

:: 检查目录是否存在，不存在就创建
if not exist "%xamlDir%" mkdir "%xamlDir%"
if not exist "%vmDir%" mkdir "%vmDir%"
if not exist "%serviceDir%" mkdir "%serviceDir%"

:: XAML 模板内容
set projectName=WpfDemo

:: 定义换行符变量“NL”。其中两行空白**不可省略**
(set NL=^

)

:: XAML.cs 模板内容
set xamlTemplate=^<UserControl x:Class="%projectName%.Views.Pages.%name%"!NL!
set xamlTemplate=!xamlTemplate! xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"!NL!
set xamlTemplate=!xamlTemplate! xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"!NL!
set xamlTemplate=!xamlTemplate! xmlns:d="http://schemas.microsoft.com/expression/blend/2008"!NL!
set xamlTemplate=!xamlTemplate! xmlns:local="clr-namespace:%projectName%.Views.Pages"!NL!
set xamlTemplate=!xamlTemplate! xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"!NL!
set xamlTemplate=!xamlTemplate! xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"!NL!
set xamlTemplate=!xamlTemplate! xmlns:viewmodels="clr-namespace:%projectName%.ViewModels.PageViewModels"!NL!
set xamlTemplate=!xamlTemplate! d:DataContext="{d:DesignInstance Type=viewmodels:%name%ViewModel}"!NL!
set xamlTemplate=!xamlTemplate! mc:Ignorable="d"^>!NL!
set xamlTemplate=!xamlTemplate!  ^<Grid^> !NL!
set xamlTemplate=!xamlTemplate!       ^<StackPanel!NL!
set xamlTemplate=!xamlTemplate!           HorizontalAlignment="Center"!NL!
set xamlTemplate=!xamlTemplate!           Orientation="Horizontal"^> !NL!
set xamlTemplate=!xamlTemplate!           ^<Border Padding="16"^> !NL!
set xamlTemplate=!xamlTemplate!               ^<TextBlock!NL!
set xamlTemplate=!xamlTemplate!                   Margin="0,0,0,0"!NL!
set xamlTemplate=!xamlTemplate!                   HorizontalAlignment="Center"!NL!
set xamlTemplate=!xamlTemplate!                   VerticalAlignment="Top"!NL!
set xamlTemplate=!xamlTemplate!                   AutomationProperties.Name="IndexLabel"!NL!
set xamlTemplate=!xamlTemplate!                   FontSize="22"!NL!
set xamlTemplate=!xamlTemplate!                   Text="%name%" /^> !NL!
set xamlTemplate=!xamlTemplate!           ^</Border^> !NL!
set xamlTemplate=!xamlTemplate!       ^</StackPanel^> !NL!
set xamlTemplate=!xamlTemplate!  ^</Grid^> 
set xamlTemplate=!xamlTemplate!^</UserControl^> 

:: XAML.cs 模板内容
set xamlCsTemplate=using System.Windows.Controls;!NL!
set xamlCsTemplate=!xamlCsTemplate!using %projectName%.ViewModels.PageViewModels;!NL!
set xamlCsTemplate=!xamlCsTemplate!!NL!
set xamlCsTemplate=!xamlCsTemplate!namespace %projectName%.Views.Pages!NL!
set xamlCsTemplate=!xamlCsTemplate!{!NL!
set xamlCsTemplate=!xamlCsTemplate!    /// ^<summary^> !NL!
set xamlCsTemplate=!xamlCsTemplate!    /// %name%.xaml 的交互逻辑!NL!
set xamlCsTemplate=!xamlCsTemplate!    /// ^</%name%^> !NL!
set xamlCsTemplate=!xamlCsTemplate!    public partial class %name% : UserControl!NL!
set xamlCsTemplate=!xamlCsTemplate!    {!NL!
set xamlCsTemplate=!xamlCsTemplate!        public %name%()!NL!
set xamlCsTemplate=!xamlCsTemplate!        {!NL!
set xamlCsTemplate=!xamlCsTemplate!            InitializeComponent();!NL!
set xamlCsTemplate=!xamlCsTemplate!        }!NL!
set xamlCsTemplate=!xamlCsTemplate!    }!NL!
set xamlCsTemplate=!xamlCsTemplate!}!NL!

:: ViewModel 模板内容
set vmTemplate=using System.Collections.ObjectModel;!NL!
set vmTemplate=!vmTemplate!using %projectName%.Services;!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!namespace %projectName%.ViewModels.PageViewModels!NL!
set vmTemplate=!vmTemplate!{!NL!
set vmTemplate=!vmTemplate!    public class %name%ViewModel : ViewModelBase!NL!
set vmTemplate=!vmTemplate!    {!NL!
set vmTemplate=!vmTemplate!        private %name%Service _service;
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        public %name%ViewModel(%name%Service service)!NL!
set vmTemplate=!vmTemplate!        {!NL!
set vmTemplate=!vmTemplate!            _service = service;!NL!
set vmTemplate=!vmTemplate!            CustomCommand = new AnotherCommandImplementation(_ =^>  { });!NL!
set vmTemplate=!vmTemplate!        }!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        /// ^<summary^> !NL!
set vmTemplate=!vmTemplate!        /// 指令!NL!
set vmTemplate=!vmTemplate!        /// ^</summary^> !NL!
set vmTemplate=!vmTemplate!        public AnotherCommandImplementation CustomCommand { get; }!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        /// ^<summary^> !NL!
set vmTemplate=!vmTemplate!        /// 响应式属性!NL!
set vmTemplate=!vmTemplate!        /// ^</summary^> !NL!
set vmTemplate=!vmTemplate!        private string? _customProp;!NL!
set vmTemplate=!vmTemplate!        public string? CustomProp!NL!
set vmTemplate=!vmTemplate!        {!NL!
set vmTemplate=!vmTemplate!            get =^>   _customProp;!NL!
set vmTemplate=!vmTemplate!            set =^>   SetProperty(ref _customProp, value);!NL!
set vmTemplate=!vmTemplate!        }!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        /// ^<summary^> !NL!
set vmTemplate=!vmTemplate!        /// 响应式集合!NL!
set vmTemplate=!vmTemplate!        /// ^</summary^> !NL!
set vmTemplate=!vmTemplate!        private ObservableCollection^<object^> _customList = [];!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        public ObservableCollection^<object^> CustomList!NL!
set vmTemplate=!vmTemplate!        {!NL!
set vmTemplate=!vmTemplate!            get =^> _customList;!NL!
set vmTemplate=!vmTemplate!            set!NL!
set vmTemplate=!vmTemplate!            {!NL!
set vmTemplate=!vmTemplate!                SetProperty(ref _customList, value);!NL!
set vmTemplate=!vmTemplate!            }!NL!
set vmTemplate=!vmTemplate!        }!NL!
set vmTemplate=!vmTemplate!    }!NL!
set vmTemplate=!vmTemplate!}!NL!

set serviceTemplate=using %projectName%.Models;!NL!
set serviceTemplate=!serviceTemplate!!NL!
set serviceTemplate=!serviceTemplate!namespace %projectName%.Services!NL!
set serviceTemplate=!serviceTemplate!{!NL!
set serviceTemplate=!serviceTemplate!    public class %name%Service : IDependency!NL!
set serviceTemplate=!serviceTemplate!    {!NL!
set serviceTemplate=!serviceTemplate!        public %name%Service() { }!NL!
set serviceTemplate=!serviceTemplate!    }!NL!
set serviceTemplate=!serviceTemplate!}!NL!

:: 生成Xaml
set xamlFile=%xamlDir%\%name%.xaml
if exist "%xamlFile%" (
    echo "%xamlFile%" 文件已存在，正在退出...
    pause
    exit /b 0
)
echo 生成 %xamlFile%...
echo !xamlTemplate! > "%xamlFile%"

:: 生成Xaml.cs
set xamlCsFile=%xamlDir%\%name%.xaml.cs
if exist "%xamlCsFile%" (
    echo "%xamlCsFile%" 文件已存在，正在退出...
    pause
    exit /b 0
)
echo 生成 %xamlCsFile%...
echo !xamlCsTemplate! > "%xamlCsFile%"

:: 生成 ViewModel
set vmFile=%vmDir%\%name%ViewModel.cs
if exist "%vmFile%" (
    echo "%vmFile%" 文件已存在，正在退出...
    pause
    exit /b 0
)
echo 生成 %vmFile%...
echo !vmTemplate! > "%vmFile%"

:: 生成 Service
set serviceFile=%serviceDir%\%name%Service.cs
if exist "%serviceFile%" (
    echo "%serviceFile%" 文件已存在，正在退出...
    pause
    exit /b 0
)
echo 生成 %serviceFile%...
echo !serviceTemplate! > "%serviceFile%"

echo.
echo 生成结束 请在Menu\MenuTrees.cs 添加路径后重新生成代码
echo XAML 	   : %xamlFile%
echo XAML.cs   : %xamlCsFile%
echo ViewModel : %vmFile%
echo Service : %serviceFile%
pause
