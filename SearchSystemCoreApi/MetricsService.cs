using System.Collections.Generic;

namespace SearchSystemCoreApi
{
    internal static class MetricsService
    {
        private static readonly object _locker = new object();
        private static Dictionary<string, List<int>> metricsStorage = new Dictionary<string, List<int>>()
        {
            {
                "External A", new List<int>()
            },
            {
                "External B", new List<int>()
            },
            {
                "External C", new List<int>()
            },
            {
                "External D", new List<int>()
            }
        };
        internal static void OnRequestCompleted(object source, MetricsEventArgs args)
        {
            lock (_locker)
            {
                metricsStorage[args.Name].Add(args.Time);
            }            
        }

        internal static Dictionary<string, List<int>> GetMetricsStorage()
        {
            lock (_locker)
            {
                return metricsStorage;
            }
        }
    }
}
