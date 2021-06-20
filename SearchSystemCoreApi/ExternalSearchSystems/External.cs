using System;
using System.Threading;
using System.Threading.Tasks;

namespace SearchSystemCoreApi.ExternalSearchSystems
{
    internal abstract class External
    {
        internal event EventHandler<MetricsEventArgs> RequestCompleted;
        protected internal abstract string Name { get; }
        protected internal async Task<ResponseModel> Request(int min, int max, CancellationToken cancellationToken)
        {
            var rnd = new Random();
            var delayTime = rnd.Next(min, max);

            if (!cancellationToken.IsCancellationRequested)
            {
                using (var delayTask = Task.Delay(delayTime, cancellationToken))
                {
                    var continuationTask = delayTask.ContinueWith(task => { });
                    await continuationTask;
                }
            }

            var response = new ResponseModel()
            {
                Name = Name,
                Time = !cancellationToken.IsCancellationRequested ? $"{delayTime} ms" : null,
                StatusCode = !cancellationToken.IsCancellationRequested
                ? Enum.GetNames(typeof(StatusCodeEnum))[rnd.Next(2)]
                : Enum.GetName(typeof(StatusCodeEnum), StatusCodeEnum.TIMEOUT)
            };

            OnRequestCompleted(Name, delayTime, response.StatusCode);

            return response;
        }

        private void OnRequestCompleted(string name, int time, string statusCode)
        {
            if (RequestCompleted != null)
            {
                if (statusCode == Enum.GetName(typeof(StatusCodeEnum), StatusCodeEnum.OK))
                    RequestCompleted(this, new MetricsEventArgs() { Name = name, Time = time });
            }
        }
    }
}
