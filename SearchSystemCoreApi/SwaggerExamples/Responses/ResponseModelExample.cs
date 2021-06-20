using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace SearchSystemCoreApi.SwaggerExamples.Responses
{
    internal class ResponseModelExample : IExamplesProvider<List<ResponseModel>>
    {
        public List<ResponseModel> GetExamples()
        {
            return new List<ResponseModel>()
            {
                new ResponseModel()
                {
                    Name = "External A",
                    StatusCode = "OK",
                    Time = "234 ms"
                },
                new ResponseModel()
                {
                    Name = "External B",
                    StatusCode = "OK",
                    Time = "256 ms"
                },
                new ResponseModel()
                {
                    Name = "External C",
                    StatusCode = "ERROR",
                    Time = "231 ms"
                }
            };
        }
    }
}
