using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public abstract class Example
    {
        public abstract string DisplayName { get; }

        public abstract Task RunAsync(CancellationToken cancellationToken);
    }
}
