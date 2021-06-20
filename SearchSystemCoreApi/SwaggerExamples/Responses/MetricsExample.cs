using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace SearchSystemCoreApi.SwaggerExamples.Responses
{
    internal class MetricsExample : IExamplesProvider<string>
    {
        public string GetExamples()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 3; i++)
            {
                var firstNumber = i;
                var secondNumber = i + 1;

                sb.Append($"{firstNumber}-{secondNumber} сек:\n");
                sb.Append($"External A --> 1\n");
                sb.Append($"External B --> 3\n");
                sb.Append($"External C --> 2\n");
                sb.Append($"External D --> 0\n");
            }

            return sb.ToString();
        }
    }
}
