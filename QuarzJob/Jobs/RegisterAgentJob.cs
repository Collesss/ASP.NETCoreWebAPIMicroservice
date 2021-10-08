using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using QuartzJobMetricAgent.Dto;
using AutoMapper;
using System.Net.Http.Json;

namespace QuarzJob
{
    public class RegisterAgentJob : IJob
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Uri _registerHost;
        private readonly AgentCreateOrUpdateRequestDto _sendingDto;
        //private readonly IMapper _mapper;

        public RegisterAgentJob(IMapper mapper, IHttpClientFactory httpClientFactory, Uri registerHost, Uri sendingUri)
        {
            _httpClientFactory = httpClientFactory;
            _registerHost = registerHost;
            _sendingDto = mapper.Map<AgentCreateOrUpdateRequestDto>(sendingUri);
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            /*
            string json = JsonSerializer.Serialize(_sendingDto,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, _registerHost) 
            {
                Content = stringContent
            };

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, RegisterHost);

            HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest, context.CancellationToken);
            
            await _httpClientFactory.CreateClient("RegisterAgent").SendAsync(httpRequest, context.CancellationToken);
            */

            await _httpClientFactory.CreateClient("RegisterAgent").PostAsJsonAsync(
                _registerHost,
                _sendingDto,
                new JsonSerializerOptions(JsonSerializerDefaults.Web),
                context.CancellationToken);
        }
    }
}
