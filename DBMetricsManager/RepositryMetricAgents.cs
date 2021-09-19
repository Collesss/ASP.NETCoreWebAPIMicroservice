using AutoMapper;
using Entities;
using EntitiesMetricsManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMetricsManager
{
    public class RepositryMetricAgents<TEntity, VBaseMetricEntity> : IRepositoryMetricAgents<TEntity> where TEntity : BaseMetricAgent where VBaseMetricEntity : BaseMetricEntity
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

        async Task<IEnumerable<TEntity>> IRepositoryMetricAgents<TEntity>.GetMetricFromAgent(int id, DateTime from, DateTime to)
        {
            MetricAgent agent = await _repository.GetAll().SingleOrDefaultAsync(agent => agent.Id == id);
            //{\w+(?=Agent)}s
            if (agent != null)
            {
                UriBuilder uriBuilder = new UriBuilder(agent.AddressAgent);

                RouteEntityAttribute routeEntityAttribute = (RouteEntityAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(RouteEntityAttribute));

                uriBuilder.Path = $"api/{routeEntityAttribute.GetRoute(typeof(TEntity).Name)}/from/{from:yyyy-MM-ddTHH:mm:ss.FFFFFFF}/to/{to:yyyy-MM-ddTHH:mm:ss.FFFFFFF}";

                HttpClient client = _httpClientFactory.CreateClient("MetricAgent");

                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);

                HttpResponseMessage httpResponse = await client.SendAsync(requestMessage);

                return (await JsonSerializer.DeserializeAsync<VBaseMetricEntity[]>(await httpResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                    .Select(metric => _mapper.Map<VBaseMetricEntity, TEntity>(metric, opt => opt.AfterMap((metricBase, metricAgent) => metricAgent.AgentId = id)));
            }

            return new TEntity[0];
        }

        async Task<IEnumerable<TEntity>> IRepositoryMetricAgents<TEntity>.GetMetricFromAgents(DateTime from, DateTime to)
        {
            List<MetricAgent> agents = await _repository.GetAll().ToListAsync();
            //{\w+(?=Agent)}s
            if (agents.Count > 0)
            {
                agents.Select(async agent =>
                {
                    UriBuilder uriBuilder = new UriBuilder(agent.AddressAgent);

                    RouteEntityAttribute routeEntityAttribute = (RouteEntityAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(RouteEntityAttribute));

                    uriBuilder.Path = $"api/{routeEntityAttribute.GetRoute(typeof(TEntity).Name)}/from/{from:yyyy-MM-ddTHH:mm:ss.FFFFFFF}/to/{to:yyyy-MM-ddTHH:mm:ss.FFFFFFF}";

                    HttpClient client = _httpClientFactory.CreateClient("MetricAgent");

                    HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);

                    HttpResponseMessage httpResponse = await client.SendAsync(requestMessage);

                    return (await JsonSerializer.DeserializeAsync<VBaseMetricEntity[]>(await httpResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                            .Select(metric => _mapper.Map<VBaseMetricEntity, TEntity>(metric, opt => opt.AfterMap((metricBase, metricAgent) => metricAgent.AgentId = agent.Id)));
                }).ToArray()
                    .SelectMany(metricsAgent => metricsAgent.Result).ToArray();
            }

            return new TEntity[0];
        }
    }
}
