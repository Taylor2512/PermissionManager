using PermissionManager.Shared.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Shared
{
    public class MessageData<T> where T:class
    {
        public string? OperationType { get; set; } 
        public required T Data { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

}
