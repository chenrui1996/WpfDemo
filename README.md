# WpfDemo

����ĿΪ [WPF �̳�](https://chenrui1996.github.io/CSharpGuide/c-sharp/WPF.html) ʾ����

Ŀ¼���� [WPF �̳�](https://chenrui1996.github.io/CSharpGuide/c-sharp/WPF.html) ��

::: tip 

����Ŀ���� [MaterialDesignThemes](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) ��Ϊǰ������⡣

����MaterialDesignThemes ��������

1. ��ҳ�ؼ�
2. ���м�ؼ�
3. Growl ��Ϣ֪ͨ
����

�ȹؼ��ؼ���

����Ҫ�ḻ�ؼ����Ƽ�ʹ�ã�[handyorg](https://handyorg.github.io/)

:::

��Ŀչʾ��

![](Pictures/WPFDemo.gif)

## �˵���

��Ŀ�˵��� `/Menu/MenuTrees.cs` �м��� [������Ҫ�ɴӺ�̨��ȡ] ���˵�����

ʾ��:

``` csharp
new MenuTreeItem { Label = "��ҳ", ContentType = typeof(Views.Pages.Index) },
```

�ڲ˵����� �ն˽ڵ㣨Terminal Node��[û���ӽڵ��Ҷ�ӽڵ�] �б����˶�Ӧ�˵���ҳ�����ݡ�

���˵�����ѡ��˵��仯ʱ���� `ContentControl` ��̬���ء�

����֮�⣬

ÿ��ҳ�����ݱ�����ע�������Ӧ�� ViewModel��ViewModel ��������ע���˶�Ӧ�� Service��

���Ͼ�����Ҫ�ֶ���д��Ӧ���롣


## ��������

1. �����Ŀ��Ŀ¼�е� `GeneratePage.bat` ������Ҫ��ҳ�����ơ�

![](Pictures/GeneratePage1.png)

![](Pictures/GeneratePage2.png)

2. ���ɽ�������`/Menu/MenuTrees.cs`���Ŀ¼��

![](Pictures/GeneratePage3.png)

![](Pictures/GeneratePage4.png)

3. �ڶ�Ӧҳ�桢ViewModel��Services�б�дҵ����롣