using System;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Windows;
using Autofac;
using Microsoft.Extensions.DependencyModel;
using WpfDemo.Models;
using WpfDemo.ViewModels;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer? Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var containerBuilder = new ContainerBuilder();

            // 注册服务
            if (DependencyContext.Default != null)
            {
                DependencyContext
                    .Default.RuntimeLibraries.Where(o => o.Name.StartsWith("WpfDemo"))
                    .Select(o => Assembly.Load(new AssemblyName(o.Name)))
                    .ToArray();
                var assemblies = AppDomain
                    .CurrentDomain.GetAssemblies()
                    .Where(r => r.GetName().ToString().StartsWith("WpfDemo"))
                    .ToArray();
                if (assemblies != null && assemblies.Any())
                {
                    containerBuilder
                        .RegisterAssemblyTypes(assemblies)
                        .Where(t => typeof(ViewModelBase).IsAssignableFrom(t) && !t.IsAbstract)
                        .AsSelf()
                        .InstancePerDependency();
                    containerBuilder
                        .RegisterAssemblyTypes(assemblies)
                        .Where(t => typeof(IDependency).IsAssignableFrom(t) && !t.IsAbstract)
                        .AsSelf()
                        .AsImplementedInterfaces()
                        .InstancePerDependency()
                        .PropertiesAutowired(); //启用属性自动注入
                }
            }

            Container = containerBuilder.Build();
        }
    }
}
