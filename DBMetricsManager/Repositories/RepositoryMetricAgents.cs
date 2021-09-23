using AutoMapper;
using Entities;
using EntitiesMetricsManager;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DBMetricsManager
{
    public abstract class RepositoryMetricAgents<TEntity, VBaseMetricEntity> : IRepositoryMetricAgents<TEntity> where TEntity : BaseMetricAgent where VBaseMetricEntity : BaseMetricEntity
    {
        protected virtual string BaseRoute => "api/";

        private IRepository<MetricAgent> _repository;
        private IHttpClientFactory _httpClientFactory;
        private IMapper _mapper;

        protected RepositoryMetricAgents(IRepository<MetricAgent> repository, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        /*
        private HttpRequestMessage GetRequestMessage(MetricAgent metricAgent, DateTime from, DateTime to)
        {
            UriBuilder uriBuilder = new UriBuilder(metricAgent.AddressAgent);

            RouteEntityAttribute routeEntityAttribute = (RouteEntityAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(RouteEntityAttribute));

            uriBuilder.Path = $"api/{routeEntityAttribute.GetRoute(typeof(TEntity).Name)}/from/{from:yyyy-MM-ddTHH:mm:ss.FFFFFFF}/to/{to:yyyy-MM-ddTHH:mm:ss.FFFFFFF}";

            return new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
        }
        */

        private async Task<HttpResponseMessage> GetHttpResponseMessage([DisallowNull] MetricAgent metricAgent, DateTime from, DateTime to)
        {
            UriBuilder uriBuilder = new UriBuilder(metricAgent.AddressAgent);

            //RouteEntityAttribute routeEntityAttribute = (RouteEntityAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(RouteEntityAttribute));
            //uriBuilder.Path = $"api/{routeEntityAttribute.GetRoute(typeof(TEntity).Name)}/from/{from:yyyy-MM-ddTHH:mm:ss.FFFFFFF}/to/{to:yyyy-MM-ddTHH:mm:ss.FFFFFFF}";

            uriBuilder.Path = $"api/{BaseRoute}/from/{from}/to/{to}";

            return await _httpClientFactory.CreateClient("MetricAgent").SendAsync(new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri));
        }

        async Task<IEnumerable<TEntity>> IRepositoryMetricAgents<TEntity>.GetMetricFromAgent(int id, DateTime from, DateTime to)
        {
            MetricAgent agent = await _repository.GetAll().SingleOrDefaultAsync(agent => agent.Id == id);
            
            HttpResponseMessage httpResponse;

            //{\w+(?=Agent)}s
            if (agent != null && (httpResponse = await GetHttpResponseMessage(agent, from, to)).IsSuccessStatusCode)
            {
                return (await JsonSerializer.DeserializeAsync<VBaseMetricEntity[]>(await httpResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                    .Select(metric => _mapper.Map<VBaseMetricEntity, TEntity>(metric, opt => opt.AfterMap((metricBase, metricAgent) => metricAgent.AgentId = id)));
            }

            return new TEntity[0];
        }

        async Task<IEnumerable<TEntity>> IRepositoryMetricAgents<TEntity>.GetMetricFromAgents(DateTime from, DateTime to)
        {
            MetricAgent[] agents = await _repository.GetAll().ToArrayAsync();
            //{\w+(?=Agent)}s

            return agents.Select(async agent =>
            {
                HttpResponseMessage httpResponse = await GetHttpResponseMessage(agent, from, to);

                return httpResponse.IsSuccessStatusCode ? (await JsonSerializer.DeserializeAsync<VBaseMetricEntity[]>(await httpResponse.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web)))
                        .Select(metric => _mapper.Map<VBaseMetricEntity, TEntity>(metric, opt => opt.AfterMap((metricBase, metricAgent) => metricAgent.AgentId = agent.Id))) : new TEntity[0];
            }).ToArray()
                .SelectMany(metricsAgent => metricsAgent.Result);
        }
    }
}
