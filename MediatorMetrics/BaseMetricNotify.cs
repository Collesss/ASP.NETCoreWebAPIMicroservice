using DB;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorMetrics
{
    public abstract class BaseMetricNotify<TEntity> : INotify where TEntity : BaseMetricEntity
    {
        //private IRepository<TEntity> _repository;

        private IServiceProvider _serviceProvider;
        private PerformanceCounter _performanceCounter;
        private IMapper _mapper;
        protected BaseMetricNotify(IServiceProvider serviceProvider, IMapper mapper, PerformanceCounter performanceCounter)
        {
            _performanceCounter = performanceCounter;
            _serviceProvider = serviceProvider;
            //_repository = repository;
            _mapper = mapper;
        }

        async Task INotify.Notify()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IRepository<TEntity> repository = scope.ServiceProvider.GetService<IRepository<TEntity>>();
                
                //await repository.CreateAsync(await Task.Run(() => _mapper.Map<TEntity>(_performanceCounter)));
                await repository.CreateAsync(_mapper.Map<TEntity>(_performanceCounter));
            }
        }
    }
}
