using WpfDemo.Models;

namespace WpfDemo.Services
{
    public class TestService : IDependency
    {
        public TestService() { }

        public string Test()
        {
            return "Test Service";
        }
    }
}
