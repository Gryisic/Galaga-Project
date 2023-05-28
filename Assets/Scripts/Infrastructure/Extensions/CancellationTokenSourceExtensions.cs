using System.Threading;

namespace Infrastructure.Extensions
{
    public static class CancellationTokenSourceExtensions
    {
        public static CancellationTokenSource CancelAndRefresh(this CancellationTokenSource tokenSource)
        {
            tokenSource.Cancel();
            tokenSource.Dispose();

            return new CancellationTokenSource();
        }
    }
}