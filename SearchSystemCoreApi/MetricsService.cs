using System.Collections.Generic;

namespace SearchSystemCoreApi
{
    internal static class MetricsService
    {
        internal static Dictionary<string, List<int>> metricsStorage = new Dictionary<string, List<int>>()
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
            metricsStorage[args.Name].Add(args.Time);
        }
    }
}
