using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using QuartzJobMetricAgent.Dto;

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
            string json = JsonSerializer.Serialize(new AgentCreateOrUpdateRequestDto()
            {
                AddressAgent = new Uri("https://localhost:44301")
            },
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, RegisterHost) 
            {
                Content = stringContent
            };

            await _httpClientFactory.CreateClient("RegisterAgent").SendAsync(httpRequest, context.CancellationToken);

            /*
            HttpClient httpClient = _httpClientFactory.CreateClient("RegisterAgent");

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, RegisterHost);

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest, context.CancellationToken);
            */
        }
    }
}
