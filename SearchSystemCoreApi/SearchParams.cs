using Microsoft.AspNetCore.Mvc;

namespace SearchSystemCoreApi
{
    public class SearchParams
    {
        [FromQuery(Name = "wait")]
        public int WaitTime { get; set; }

        [FromQuery(Name = "randomMin")]
        public int RandomMin { get; set; }

        [FromQuery(Name = "randomMax")]
        public int RandomMax { get; set; }
    }
}
