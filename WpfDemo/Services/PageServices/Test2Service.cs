using WpfDemo.Models;

namespace WpfDemo.Services
{
    public interface IService { }

    public class Test2Service : IService, IDependency
    {
        public Test2Service() { }
    }
}
