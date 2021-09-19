using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace QuarzJob
{
    public class RegisterAgentJob : IJob
    {
        private Uri RegisterHost { get; }
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterAgentJob(IHttpClientFactory httpClientFactory, Uri registerHost)
        {
            _httpClientFactory = httpClientFactory;
            RegisterHost = registerHost;
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            await _httpClientFactory.CreateClient("RegisterAgent").SendAsync(new HttpRequestMessage(HttpMethod.Post, RegisterHost), context.CancellationToken);

            /*
            HttpClient httpClient = _httpClientFactory.CreateClient("RegisterAgent");

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, RegisterHost);

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest, context.CancellationToken);
            */
        }
    }
}
