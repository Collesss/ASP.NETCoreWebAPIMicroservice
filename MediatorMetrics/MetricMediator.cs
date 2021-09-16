using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorMetrics
{
    public class MetricMediator : IMediator
    {
        private IEnumerable<INotify> _notifies;

        public MetricMediator(IEnumerable<INotify> notifies)
        {
            _notifies = notifies;
        }

        async Task IMediator.Notify()
        {
            await Task.WhenAll(_notifies.Select(n => n.Notify()).ToArray());
        }
    }
}
