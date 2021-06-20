using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SearchSystemCoreApi.ExternalSearchSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchSystemCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;

        private readonly ExternalA _externalA;
        private readonly ExternalB _externalB;
        private readonly ExternalC _externalC;
        private readonly ExternalD _externalD;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;

            _externalA = new ExternalA();
            _externalB = new ExternalB();
            _externalC = new ExternalC();
            _externalD = new ExternalD();
        }

        private async Task<List<ResponseModel>> RunRequestAsync(int min, int max, CancellationToken cancellationToken)
        {
            var responseModelList = new List<ResponseModel>();

            _externalA.RequestCompleted += MetricsService.OnRequestCompleted;
            _externalB.RequestCompleted += MetricsService.OnRequestCompleted;
            _externalC.RequestCompleted += MetricsService.OnRequestCompleted;
            _externalD.RequestCompleted += MetricsService.OnRequestCompleted;

            var responseTaskA = _externalA.Request(min, max, cancellationToken);
            var responseTaskB = _externalB.Request(min, max, cancellationToken);
            var responseTaskC = _externalC.Request(min, max, cancellationToken);

            await Task.WhenAll(responseTaskA, responseTaskB, responseTaskC);

            responseModelList.Add(await responseTaskA);
            responseModelList.Add(await responseTaskB);

            var responseResultC = await responseTaskC;
            responseModelList.Add(responseResultC);

            if (responseResultC.StatusCode != Enum.GetName(typeof(StatusCodeEnum), StatusCodeEnum.OK))
            {
                return responseModelList;
            }

            var responseResultD = await _externalD.Request(min, max, cancellationToken);
            responseModelList.Add(responseResultD);

            return responseModelList;
        }

        /// <summary>
        /// Makes a requests to Externals
        /// </summary>
        /// <remarks>
        /// Sample **request**:
        ///
        ///     GET /Search?wait=300&amp;randomMin=100&amp;randomMax=200
        ///
        /// </remarks>
        /// <param name="parameters"></param>
        /// <returns>
        /// <response code="200">Returns result of requests</response>
        /// </returns>        
        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseModel>), 200)]
        public async Task<IActionResult> GetResponse([FromQuery] SearchParams parameters)
        {
            var cancellationTokenSource = new CancellationTokenSource(parameters.WaitTime);
            var token = cancellationTokenSource.Token;

            var responseModels = await RunRequestAsync(parameters.RandomMin, parameters.RandomMax, token);

            cancellationTokenSource.Dispose();

            Response.Headers.Add("Content-Type", "application/json");

            return Ok(JsonConvert.SerializeObject(responseModels));
        }
    }
}
