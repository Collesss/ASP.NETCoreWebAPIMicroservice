using AutoMapper;
using Entities;
using EntitiesMetricsManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DBMetricsManager
{
    public class RepositryMetricAgents<TEntity> : IRepositoryMetricAgents<TEntity> where TEntity : BaseMetricAgent
    {
        private IRepository<MetricAgent> _repository;
        private IHttpClientFactory _httpClientFactory;
        private IMapper _mapper;

        public RepositryMetricAgents(IRepository<MetricAgent> repository, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        IEnumerable<TEntity> IRepositoryMetricAgents<TEntity>.GetMetricFromAgent(int id, DateTime from, DateTime to)
        {
            MetricAgent agent = _repository.GetAll().SingleOrDefault(agent => agent.Id == id);
            //{\w+(?=Agent)}s
            UriBuilder uriBuilder = new UriBuilder(agent.AddressAgent);
            
            uriBuilder.Path = $"api//from/{from.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFF")}/to{to.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFF")}";
            
            HttpClient client = _httpClientFactory.CreateClient("MetricAgent");

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);

            HttpResponseMessage httpResponse = client.SendAsync(requestMessage).Result;

            return JsonSerializer.DeserializeAsync<IEnumerable<BaseMetricEntity>>(httpResponse.Content.ReadAsStreamAsync().Result, new JsonSerializerOptions(JsonSerializerDefaults.Web)).Result
                .Select(metric => _mapper.Map<BaseMetricEntity, TEntity>(metric, opt => opt.AfterMap((metricBase, metricAgent) => metricAgent.AgentId = id)));

        }

        IEnumerable<TEntity> IRepositoryMetricAgents<TEntity>.GetMetricFromAgents(DateTime from, DateTime to)
        {
            throw new NotSupportedException();
        }
    }
}
