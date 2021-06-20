using System;

namespace SearchSystemCoreApi
{
    public class MetricsEventArgs : EventArgs
    {
        public int Time { get; set; }
        public string Name { get; set; }
    }
}
