using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Consumers
{
    public interface IPermissionTypeConsumerService
    {
        Task ProcessMessagesAsync(CancellationToken stoppingToken);

    }
}
