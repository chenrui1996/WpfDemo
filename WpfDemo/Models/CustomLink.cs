using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Input;
using WpfDemo.ViewModels;

namespace WpfDemo.Models
{
    public enum DocumentationLinkType
    {
        PageSource,
        Specs,
    }

    public class CustomLink
    {
        public CustomLink(DocumentationLinkType type, string url)
            : this(type, url, null) { }

        public CustomLink(DocumentationLinkType type, string url, string? label)
        {
            Label = label ?? type.ToString();
            Url = url;
            Type = type;
            Open = new RelayCommandImplementation(Execute);
        }

        public static CustomLink PageLink<TDemoPage>() => PageLink<TDemoPage>(null);

        public static CustomLink PageLink<TDemoPage>(string? label) =>
            PageLink<TDemoPage>(label, null);

        public static CustomLink PageLink<TDemoPage>(string? label, string? @namespace)
        {
            // todo
            // link to user page
            return new CustomLink(DocumentationLinkType.PageSource, "");
        }

        public static CustomLink SpecsLink(string url, string? label) =>
            new(DocumentationLinkType.Specs, url, label ?? "Specs");

        public string Label { get; }

        public string Url { get; }

        public DocumentationLinkType Type { get; }
        public ICommand Open { get; }

        private void Execute(object? _)
        {
            if (Url is not null && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(Url) { UseShellExecute = true });
            }
        }
    }
}
