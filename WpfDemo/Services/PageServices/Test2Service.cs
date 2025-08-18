using WpfDemo.Models;

namespace WpfDemo.Services
{
    public class Test2Service : IDependency
    {
        public Test2Service() { }

        public string GetInfo()
        {
            return "Test2Service:GetInfo()";
        }
    }
}
 
