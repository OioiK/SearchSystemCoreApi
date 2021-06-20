using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SearchSystemCoreApi
{
    public class SearchParams
    {
        [Required]
        [FromQuery(Name = "wait")]        
        public int WaitTime { get; set; }

        [Required]
        [FromQuery(Name = "randomMin")]
        public int RandomMin { get; set; }

        [Required]
        [FromQuery(Name = "randomMax")]
        public int RandomMax { get; set; }
    }
}
