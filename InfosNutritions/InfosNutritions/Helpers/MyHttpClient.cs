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
        private const int TimeoutLimit = 18000;
        #region Constructor

        public AzureHttpClient(string uri)
        {
            BaseAddress = new Uri(uri);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public AzureHttpClient(string uri, string authToken) : this(uri)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }
        #endregion

        #region Methods

        public async Task<string> RequestWithResult(HttpMethod method, string path, object param = null)
        {
            var cancel = new CancellationTokenSource();
            var message = new HttpRequestMessage(method, path);
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
                else if (resultTask.Result.StatusCode == HttpStatusCode.NoContent)
                {
                    cancel.Cancel(false);
                    throw new DataNotFoundException();
                }
                else if (resultTask.Result.StatusCode == HttpStatusCode.BadRequest)
                {
                    cancel.Cancel(false);
                    throw new BadRequestException();
                }
                cancel.Cancel(false);
                return null;
            }
            cancel.Cancel(false);
            throw new AzureConnectivityException();
        }

        #endregion
    }
}
