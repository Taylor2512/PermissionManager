using PermissionManager.Shared.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Shared
{
    public class PermissionEvent<T> where T:class
    {
        public string? OperationType { get; set; } 
        public required T EventData { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

}
