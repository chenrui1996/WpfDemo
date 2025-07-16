@echo off
setlocal EnableDelayedExpansion

:: ��ʾ���� name
set /p name=������ؼ�/��ͼ�� Name������ĸ��д���� Index��
if "%name%"=="" (
  echo Name ����Ϊ�գ�
  goto :eof
)

:: �������Ŀ¼����������Ŀʵ��·��������
set xamlDir=.\Views\Pages
set vmDir=.\ViewModels\PageViewModels

:: ���Ŀ¼�Ƿ���ڣ������ھʹ���
if not exist "%xamlDir%" mkdir "%xamlDir%"
if not exist "%vmDir%" mkdir "%vmDir%"

:: XAML ģ������
set projectName=WpfDemo

:: ���廻�з�������NL�����������пհ�**����ʡ��**
(set NL=^

)

:: XAML.cs ģ������
set xamlTemplate=^<UserControl x:Class="%projectName%.Views.Pages.%name%"!NL!
set xamlTemplate=!xamlTemplate! xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"!NL!
set xamlTemplate=!xamlTemplate! xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"!NL!
set xamlTemplate=!xamlTemplate! xmlns:d="http://schemas.microsoft.com/expression/blend/2008"!NL!
set xamlTemplate=!xamlTemplate! xmlns:local="clr-namespace:%projectName%.Views.Pages"!NL!
set xamlTemplate=!xamlTemplate! xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"!NL!
set xamlTemplate=!xamlTemplate! xmlns:viewmodels="clr-namespace:%projectName%.ViewModels.PageViewModels"!NL!
set xamlTemplate=!xamlTemplate! d:DataContext="{d:DesignInstance Type=viewmodels:%name%ViewModel}"!NL!
set xamlTemplate=!xamlTemplate! d:DesignHeight="450"!NL!
set xamlTemplate=!xamlTemplate! d:DesignWidth="800"!NL!
set xamlTemplate=!xamlTemplate! mc:Ignorable="d"^>!NL!
set xamlTemplate=!xamlTemplate!  ^<Grid^> !NL!
set xamlTemplate=!xamlTemplate!       ^<StackPanel!NL!
set xamlTemplate=!xamlTemplate!           Grid.Column="1"!NL!
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

:: XAML.cs ģ�����ݮ�
set xamlCsTemplate=using System.Windows.Controls;!NL!
set xamlCsTemplate=!xamlCsTemplate!using %projectName%.ViewModels.PageViewModels;!NL!
set xamlCsTemplate=!xamlCsTemplate!!NL!
set xamlCsTemplate=!xamlCsTemplate!namespace %projectName%.Views.Pages!NL!
set xamlCsTemplate=!xamlCsTemplate!{!NL!
set xamlCsTemplate=!xamlCsTemplate!    /// ^<summary^> !NL!
set xamlCsTemplate=!xamlCsTemplate!    /// %name%.xaml �Ľ����߼�!NL!
set xamlCsTemplate=!xamlCsTemplate!    /// ^</%name%^> !NL!
set xamlCsTemplate=!xamlCsTemplate!    public partial class %name% : UserControl!NL!
set xamlCsTemplate=!xamlCsTemplate!    {!NL!
set xamlCsTemplate=!xamlCsTemplate!        public %name%()!NL!
set xamlCsTemplate=!xamlCsTemplate!        {!NL!
set xamlCsTemplate=!xamlCsTemplate!            InitializeComponent();!NL!
set xamlCsTemplate=!xamlCsTemplate!            DataContext = new %name%ViewModel();!NL!
set xamlCsTemplate=!xamlCsTemplate!        }!NL!
set xamlCsTemplate=!xamlCsTemplate!    }!NL!
set xamlCsTemplate=!xamlCsTemplate!}!NL!

:: ViewModel ģ������
set vmTemplate=using System.Collections.ObjectModel;!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!namespace %projectName%.ViewModels.PageViewModels!NL!
set vmTemplate=!vmTemplate!{!NL!
set vmTemplate=!vmTemplate!    public class %name%ViewModel : ViewModelBase!NL!
set vmTemplate=!vmTemplate!    {!NL!
set vmTemplate=!vmTemplate!        public %name%ViewModel()!NL!
set vmTemplate=!vmTemplate!        {!NL!
set vmTemplate=!vmTemplate!            CustomCommand = new AnotherCommandImplementation(_ =^>  { });!NL!
set vmTemplate=!vmTemplate!        }!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        /// ^<summary^> !NL!
set vmTemplate=!vmTemplate!        /// ָ��!NL!
set vmTemplate=!vmTemplate!        /// ^</summary^> !NL!
set vmTemplate=!vmTemplate!        public AnotherCommandImplementation CustomCommand { get; }!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        /// ^<summary^> !NL!
set vmTemplate=!vmTemplate!        /// ��Ӧʽ����!NL!
set vmTemplate=!vmTemplate!        /// ^</summary^> !NL!
set vmTemplate=!vmTemplate!        private int _customProp;!NL!
set vmTemplate=!vmTemplate!        public int CustomProp!NL!
set vmTemplate=!vmTemplate!        {!NL!
set vmTemplate=!vmTemplate!            get =^>   _customProp;!NL!
set vmTemplate=!vmTemplate!            set =^>   SetProperty(ref _customProp, value);!NL!
set vmTemplate=!vmTemplate!        }!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        /// ^<summary^> !NL!
set vmTemplate=!vmTemplate!        /// ��Ӧʽ����!NL!
set vmTemplate=!vmTemplate!        /// ^</summary^> !NL!
set vmTemplate=!vmTemplate!        private ObservableCollection^<object^>   _customList = [];!NL!
set vmTemplate=!vmTemplate!!NL!
set vmTemplate=!vmTemplate!        public ObservableCollection^<object^>   CustomList!NL!
set vmTemplate=!vmTemplate!        {!NL!
set vmTemplate=!vmTemplate!            get =^>   _customList;!NL!
set vmTemplate=!vmTemplate!            set!NL!
set vmTemplate=!vmTemplate!            {!NL!
set vmTemplate=!vmTemplate!                SetProperty(ref _customList, value);!NL!
set vmTemplate=!vmTemplate!            }!NL!
set vmTemplate=!vmTemplate!        }!NL!
set vmTemplate=!vmTemplate!    }!NL!
set vmTemplate=!vmTemplate!}!NL!

:: ����Xaml
set xamlFile=%xamlDir%\%name%.xaml
echo ���� %xamlFile%...
echo !xamlTemplate! > "%xamlFile%"

:: ����Xaml.cs
set xamlCsFile=%xamlDir%\%name%.xaml.cs
echo ���� %xamlCsFile%...
echo !xamlCsTemplate! > "%xamlCsFile%"

:: ���� ViewModel
set vmFile=%vmDir%\%name%ViewModel.cs
echo ���� %vmFile%...
echo !vmTemplate! > "%vmFile%"

echo.
echo ���ɽ��� ����Menu\MenuTrees.cs ���·�����������ɴ���
echo XAML 	   : %xamlFile%
echo XAML.cs   : %xamlCsFile%
echo ViewModel : %vmFile%
pause
