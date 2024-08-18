using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionManager.Shared
{
    public record  ElasticSearchAuthConfig
    {
        [Required]
        public string Url { get; init; }
        public string? CloudID { get; init; }
        public string? ApiKey { get; init; }
        public string? Username { get; init; }
        public string? Password { get; init; }
        public string? Fingerprint { get; init; }
    }
}
