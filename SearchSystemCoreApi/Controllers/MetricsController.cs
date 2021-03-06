using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;

namespace SearchSystemCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("text/plain")]
    public class MetricsController : ControllerBase
    {
        private readonly ILogger<MetricsController> _logger;

        public MetricsController(ILogger<MetricsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Makes a requests for api metrics
        /// </summary>
        /// <remarks>
        /// Sample **request**:
        ///
        ///     GET /Metrics
        ///
        /// </remarks>
        /// <returns>
        /// <response code="200">Returns api metrics</response>
        /// </returns>     
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public string GetMetrics()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                var firstNumber = i;
                var secondNumber = i + 1;

                sb.Append($"{firstNumber}-{secondNumber} сек:\n");

                var metricsStorage = MetricsService.GetMetricsStorage();

                foreach (var pair in metricsStorage)
                {
                    sb.Append($"{pair.Key} --> {pair.Value.Where(x => x >= (firstNumber * 1000) && x < (secondNumber * 1000)).Count()}\n");
                }
            }

            return sb.ToString();
        }
    }
}
