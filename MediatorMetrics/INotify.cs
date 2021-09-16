using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediatorMetrics
{
    public interface INotify
    {
        Task Notify();
    }
}
