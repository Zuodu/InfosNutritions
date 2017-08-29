using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InfosNutritions.Helpers
{
    class MyHttpClient : HttpClient
    {
        private const int TimeoutLimit = 6000;
        private readonly string _endUri;
        #region Constructor

        public MyHttpClient(string uri,string endUri)
        {
            BaseAddress = new Uri(uri);
            _endUri = endUri;
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion

        #region Methods

        public async Task<string> RequestWithResult(HttpMethod method, string path, object param = null)
        {
            var cancel = new CancellationTokenSource();
            var message = new HttpRequestMessage(method, $"{path}{_endUri}");
            if (method == HttpMethod.Post)
            {
                message.Content = param?.GetType() == typeof(string)
                    ? new StringContent((string)param)
                    : new StringContent(param?.ToString());
            }
            var resultTask = SendAsync(message, cancel.Token);
            var timeout = Task.Delay(TimeoutLimit, cancel.Token);
            var firstTask = await Task.WhenAny(resultTask, timeout);
            if (firstTask == resultTask)
            {
                if (resultTask.Result.StatusCode == HttpStatusCode.OK)
                {
                    var result = await resultTask.Result.Content.ReadAsStringAsync();
                    cancel.Cancel(false);
                    return result;
                }
            }
            cancel.Cancel(false);
            return null;
        }

        #endregion
    }
}
